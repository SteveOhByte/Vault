using System.Windows;
using System.Windows.Input;

namespace Vault;

public partial class EditLockWindow
{
    public EditLockWindow()
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
            OnRemoveButtonClicked(sender, e);
    }
    
    private void OnRemoveButtonClicked(object sender, RoutedEventArgs e)
    {
        string folderPath = App.ActiveFolderPath;
        if (PasswordBox.Password != ConfirmPasswordBox.Password)
        {
            MessageBox.Show("Error", "Passwords do not match.", false);
            return;
        }

        if (string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            MessageBox.Show("Error", "Password cannot be empty.", false);
            return;
        }
        
        if (LockManager.ValidatePassword(PasswordBox.Password, folderPath))
        {
            LockManager.UnlockFolder(folderPath);
            LockManager.RemoveConfiguration(folderPath);
            MessageBox.Show("Success", "Password removed and folder unlocked successfully.", true);
            Close();
        }
        else
        {
            MessageBox.Show("Error", "Incorrect password.", false);
        }
    }
    
    private void OnNewPasswordBoxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            OnChangeButtonClicked(sender, e);
    }
    
    private void OnChangeButtonClicked(object sender, RoutedEventArgs e)
    {
        string folderPath = App.ActiveFolderPath;
        if (OldPasswordBox.Password == NewPasswordBox.Password)
        {
            MessageBox.Show("Error", "New password must not match the old password.", false);
            return;
        }

        if (string.IsNullOrWhiteSpace(NewPasswordBox.Password))
        {
            MessageBox.Show("Error", "New password cannot be empty.", false);
            return;
        }

        if (LockManager.ValidatePassword(OldPasswordBox.Password, folderPath))
        {
            LockManager.UpdateConfiguration(NewPasswordBox.Password, folderPath);
            MessageBox.Show("Success", "Password changed successfully.", true);
            Close();
        }
        else
        {
            MessageBox.Show("Error", "Incorrect old password.", false);
        }
    }
}