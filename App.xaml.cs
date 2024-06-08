using System.IO;
using System.Security.Principal;
using System.Windows;

namespace Vault;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static string ActiveFolderPath { get; private set; }
    
    private static bool IsAdministrator()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
    
    private void ApplicationStartup(object sender, StartupEventArgs e)
    {
        if (!IsAdministrator())
        {
            MessageBox.Show("Error",
                "This application requires administrator privileges. Please run the application as an administrator.",
                true);
            return;
        }
        
        List<string> args = e.Args.ToList();
        
        if (args.Count < 3)
        {
            MessageBox.Show("Error", "Not enough arguments provided. Exiting...", true);
            return;
        }
        
        if (args.Contains("-path"))
        {
            int index = args.IndexOf("-path") + 1;
            ActiveFolderPath = args[index];
            ValidateFolderPath();
        }

        ProcessArguments(args);
    }

    private void ValidateFolderPath()
    {
        if (!string.IsNullOrWhiteSpace(ActiveFolderPath) && !Directory.Exists(ActiveFolderPath))
            MessageBox.Show("Error", "The specified path does not exist. Exiting...", true);
    }

    private void ProcessArguments(List<string> args)
    {
        bool hasConfig = LockManager.FolderHasConfig(ActiveFolderPath);
        bool isLocked = LockManager.IsFolderLocked(ActiveFolderPath);

        if (args.Contains("-create"))
        {
            if (hasConfig && !isLocked)
            {
                LockManager.ApplyLock(ActiveFolderPath);
                MessageBox.Show("Success", "Folder locked successfully.", true);
                return;
            }
            else if (isLocked)
            {
                MessageBox.Show("Notice", "The folder is already locked.", true);
                return;
            }
            StartupUri = new("/CreateLockWindow.xaml", UriKind.Relative);
        }
        else if (args.Contains("-unlock"))
        {
            if (!hasConfig || !isLocked)
            {
                string message = !hasConfig ? "The folder does not have a configuration." : "The folder is already unlocked.";
                MessageBox.Show("Notice", message, true);
                return;
            }
            StartupUri = new("/UnlockWindow.xaml", UriKind.Relative);
        }
        else if (args.Contains("-edit"))
        {
            if (!hasConfig)
            {
                MessageBox.Show("Error", "The folder does not have a configuration.", true);
                return;
            }
            StartupUri = new("/EditLockWindow.xaml", UriKind.Relative);
        }
        else
            MessageBox.Show("Error", "Invalid arguments provided. Exiting...", true);
    }
}