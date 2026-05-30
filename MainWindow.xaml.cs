using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CyberSecurityChatbotPart2.Models;
using CyberSecurityChatbotPart2.Services;

namespace CyberSecurityChatbotPart2;

public partial class MainWindow : Window
{
    private readonly ChatBot _chatBot = new();
    private readonly ObservableCollection<ChatMessage> _messages = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        AddBotMessage(_chatBot.GetInitialMessage());
        UpdateMemoryPanel();
    }

    public ObservableCollection<ChatMessage> Messages => _messages;

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        bool played = _chatBot.PlayVoiceGreeting();

        if (!played)
        {
            AddBotMessage("Voice greeting file was not found or could not be played. Make sure Assets/welcome.wav is included in the project and set to copy to the output folder.");
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        SendCurrentText();
    }

    private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && Keyboard.Modifiers != ModifierKeys.Shift)
        {
            e.Handled = true;
            SendCurrentText();
        }
    }

    private void QuickTopicButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not string topic)
        {
            return;
        }

        UserInputTextBox.Text = topic is "another tip" or "explain more"
            ? topic
            : $"Tell me about {topic}";

        SendCurrentText();
    }

    private void SaveNameButton_Click(object sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            AddBotMessage("Please type your name before clicking Save.");
            return;
        }

        AddUserMessage($"My name is {name}");
        AddBotMessage(_chatBot.GetResponse($"My name is {name}"));
        NameTextBox.Clear();
        UpdateMemoryPanel();
    }

    private void PlayVoiceButton_Click(object sender, RoutedEventArgs e)
    {
        bool played = _chatBot.PlayVoiceGreeting();
        AddBotMessage(played
            ? "Voice greeting replayed."
            : "I could not play the voice greeting. Check that Assets/welcome.wav exists and is copied to the output folder.");
    }

    private void CopyTranscriptButton_Click(object sender, RoutedEventArgs e)
    {
        string transcript = string.Join(Environment.NewLine, _messages.Select(message => $"[{message.TimeText}] {message.Sender}: {message.Text}"));

        try
        {
            Clipboard.SetText(transcript);
            AddBotMessage("Transcript copied to the clipboard.");
        }
        catch
        {
            AddBotMessage("The transcript could not be copied, but the chat is still visible on screen.");
        }
    }

    private void ClearChatButton_Click(object sender, RoutedEventArgs e)
    {
        _messages.Clear();
        AddBotMessage("Chat display cleared. I still remember the details you shared during this session.");
        UpdateMemoryPanel();
    }

    private void SendCurrentText()
    {
        string input = UserInputTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            AddBotMessage("Please type a message first. For example: 'Give me a phishing tip.'");
            return;
        }

        UserInputTextBox.Clear();
        AddUserMessage(input);

        string response = _chatBot.GetResponse(input);
        AddBotMessage(response);
        UpdateMemoryPanel();
    }

    private void AddUserMessage(string message)
    {
        AddMessage("You", message, isUser: true);
    }

    private void AddBotMessage(string message)
    {
        AddMessage("Bot", message, isUser: false);
    }

    private void AddMessage(string sender, string text, bool isUser)
    {
        _messages.Add(new ChatMessage
        {
            Sender = sender,
            Text = text,
            IsUser = isUser,
            Time = DateTime.Now
        });

        Dispatcher.InvokeAsync(() => ChatScrollViewer.ScrollToEnd(), DispatcherPriority.Background);
    }

    private void UpdateMemoryPanel()
    {
        MemoryTextBlock.Text = _chatBot.Memory.BuildSnapshot();
    }
}
