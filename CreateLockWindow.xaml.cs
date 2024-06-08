using System.Windows;
using System.Windows.Input;

namespace Vault;

/// <summary>
/// Interaction logic for CreateLockWindow.xaml
/// </summary>
public partial class CreateLockWindow
{
    public CreateLockWindow()
    {
        InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        PasswordBox.Focus();
    }

    private void OnConfirmPasswordBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            OnLockButtonClicked(sender, e);
    }
    
    private void OnLockButtonClicked(object sender, RoutedEventArgs e)
    {
        string folderPath = App.ActiveFolderPath;

        if (PasswordBox.Password != ConfirmPasswordBox.Password)
        {
            MessageBox.Show("Error", "Passwords do not match.", true);
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            MessageBox.Show("Error", "Password cannot be empty.", true);
            return;
        }

        if (LockManager.IsFolderLocked(folderPath))
        {
            MessageBox.Show("Notice", "The folder is already locked or has an existing configuration.", true);
            return;
        }

        byte[] salt = LockManager.GenerateSalt();
        string hashedPassword = LockManager.HashPassword(PasswordBox.Password, salt);
        Dictionary<string, object> lockConfig = new()
        {
            { "passwordHash", hashedPassword },
            { "salt", Convert.ToBase64String(salt) }
        };

        LockManager.LockFolder(folderPath, lockConfig);

        MessageBox.Show("Success", "Folder locked successfully.", true);
        Close();
    }
}