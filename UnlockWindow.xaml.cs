using System.Windows;
using System.Windows.Input;

namespace Vault;

public partial class UnlockWindow
{
    public UnlockWindow()
    {
        InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        PasswordBox.Focus();
    }

    private void OnPasswordBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            OnUnlockButtonClicked(sender, e);
    }
    
    private void OnUnlockButtonClicked(object sender, RoutedEventArgs e)
    {
        string folderPath = App.ActiveFolderPath;

        if (string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            MessageBox.Show("Error", "Password cannot be empty.", false);
            return;
        }

        if (LockManager.ValidatePassword(PasswordBox.Password, folderPath))
        {
            LockManager.UnlockFolder(folderPath);
            MessageBox.Show("Success", "Folder unlocked successfully.", true);
            Close();
        }
        else
        {
            MessageBox.Show("Error", "Incorrect password.", false);
        }
    }
}