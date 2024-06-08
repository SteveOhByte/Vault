using System.Windows;

namespace Vault;

public static class MessageBox
{
    public static void Show(string title, string content, bool shutdownAfter)
    {
        Wpf.Ui.Controls.MessageBox box = new() { Title = title, Content = content };
        box.ShowDialogAsync();
        if (shutdownAfter) Application.Current.Shutdown();
    }
}