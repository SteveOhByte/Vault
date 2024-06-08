using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Vault;

public static class LockManager
{
    private static readonly string DataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Vault");
    private static readonly string ConfigFilePath = Path.Combine(DataFolderPath, "config.json");

    public static bool IsFolderLocked(string folderPath)
    {
        Dictionary<string, Dictionary<string, object>>? config = ReadConfig();
        if (config == null || !config.ContainsKey(folderPath)) return false;

        IEnumerable<FileSystemAccessRule> rules = GetAccessRules(folderPath);
        return rules.Any(rule => (rule.FileSystemRights & FileSystemRights.Write) != 0 && rule.AccessControlType == AccessControlType.Deny);
    }

    public static void LockFolder(string folderPath, Dictionary<string, object> lockConfig)
    {
        UpdateOrStoreConfiguration(folderPath, lockConfig);
        ApplySecurityChanges(folderPath, AccessControlType.Deny);
    }

    public static void ApplyLock(string folderPath)
    {
        ApplySecurityChanges(folderPath, AccessControlType.Deny);
    }
    
    public static void UnlockFolder(string folderPath)
    {
        ApplySecurityChanges(folderPath, AccessControlType.Allow);
    }

    private static void ApplySecurityChanges(string folderPath, AccessControlType controlType)
    {
        DirectoryInfo info = new(folderPath);
        DirectorySecurity security = info.GetAccessControl();
        SecurityIdentifier everyone = new(WellKnownSidType.WorldSid, null);

        switch (controlType)
        {
            case AccessControlType.Allow:
                security.RemoveAccessRule(new(everyone, FileSystemRights.Read | FileSystemRights.Write, AccessControlType.Deny));
                break;
            case AccessControlType.Deny:
                security.AddAccessRule(new(everyone, FileSystemRights.Read | FileSystemRights.Write, AccessControlType.Deny));
                break;
        }
        
        info.SetAccessControl(security);
    }

    public static bool ValidatePassword(string inputPassword, string folderPath)
    {
        Dictionary<string, Dictionary<string, object>>? config = ReadConfig();
        if (config != null && config.TryGetValue(folderPath, out Dictionary<string, object>? value))
        {
            string? storedHash = value["passwordHash"].ToString();
            byte[] salt = Convert.FromBase64String(value["salt"].ToString());
            string hashedInput = HashPassword(inputPassword, salt);
            return hashedInput == storedHash;
        }
        return false;
    }

    public static bool FolderHasConfig(string folderPath)
    {
        Dictionary<string, Dictionary<string, object>>? config = ReadConfig();
        return config != null && config.ContainsKey(folderPath);
    }
    
    public static void RemoveConfiguration(string folderPath)
    {
        Dictionary<string, Dictionary<string, object>>? config = ReadConfig();
        if (config == null || !config.ContainsKey(folderPath)) return;
        
        config.Remove(folderPath);
        WriteConfig(config);
    }
    
    public static void UpdateConfiguration(string newPassword, string folderPath)
    {
        byte[] salt = GenerateSalt();
        string hashedPassword = HashPassword(newPassword, salt);
        Dictionary<string, object> lockConfig = new()
        {
            { "passwordHash", hashedPassword },
            { "salt", Convert.ToBase64String(salt) }
        };
        UpdateOrStoreConfiguration(folderPath, lockConfig);
    }

    private static void UpdateOrStoreConfiguration(string folderPath, Dictionary<string, object> lockConfig)
    {
        Dictionary<string, Dictionary<string, object>> config = ReadConfig() ?? new();
        config[folderPath] = lockConfig;
        WriteConfig(config);
    }

    private static Dictionary<string, Dictionary<string, object>>? ReadConfig()
    {
        if (!File.Exists(ConfigFilePath))
            return null;
        
        string json = File.ReadAllText(ConfigFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(json);
    }

    private static void WriteConfig(Dictionary<string, Dictionary<string, object>> config)
    {
        File.WriteAllText(ConfigFilePath, JsonSerializer.Serialize(config));
    }

    private static IEnumerable<FileSystemAccessRule> GetAccessRules(string folderPath)
    {
        DirectoryInfo info = new(folderPath);
        DirectorySecurity security = info.GetAccessControl();
        AuthorizationRuleCollection rules = security.GetAccessRules(true, true, typeof(SecurityIdentifier));
        return rules.OfType<FileSystemAccessRule>();
    }

    public static byte[] GenerateSalt()
    {
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[16];
        rng.GetBytes(salt);
        return salt;
    }

    public static string HashPassword(string password, IEnumerable<byte> salt)
    {
        byte[] saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
        return Convert.ToBase64String(SHA256.HashData(saltedPassword));
    }
}