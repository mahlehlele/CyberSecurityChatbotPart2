using System.Windows;
using System.Windows.Threading;

namespace CyberSecurityChatbotPart2;

public partial class App : Application
{
    public App()
    {
        DispatcherUnhandledException += OnDispatcherUnhandledException;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(
            "The chatbot found an unexpected problem, but the app will keep running. Details: " + e.Exception.Message,
            "Cybersecurity Chatbot",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);

        e.Handled = true;
    }
}
