# Cybersecurity Awareness Assistant - Part 2 WPF

This project is a WPF/C# version of the Part 1 Cybersecurity Awareness Chatbot. It keeps the original Part 1 ideas: a cybersecurity assistant, greeting, voice greeting, ASCII art branding, password/phishing/malware/safe-browsing responses, input validation, and clear interaction flow. Part 2 adds a graphical interface, dynamic/random responses, keyword recognition, conversation memory, sentiment detection, follow-up handling, and cleaner class organisation.

## Requirements

- Windows computer
- Visual Studio 2022
- .NET 8 Desktop Development workload installed

## How to run

1. Open `CyberSecurityChatbotPart2.sln` in Visual Studio 2022.
2. Wait for Visual Studio to load the project.
3. Confirm the project is using `.NET 8.0-windows`.
4. Click **Build > Build Solution**.
5. Click **Start** or press **F5**.
6. The WPF chatbot window opens and the voice greeting should play.

## What to test in the app

Try these messages in order so that every rubric feature is visible:

1. `My name is Asemahle`
2. `Tell me about password safety`
3. `Give me a phishing tip`
4. `another tip`
5. `explain more`
6. `I'm interested in privacy`
7. `What topic am I interested in?`
8. `I'm worried about online scams`
9. `blah blah`
10. `help`
11. `bye`

## Rubric mapping

### 1. GUI design and implementation

- `MainWindow.xaml` contains the WPF graphical layout.
- The GUI has a chat display, text input box, Send button, quick-topic buttons, memory status panel, voice replay button, transcript copy button, and clear-chat button.
- The ASCII art from Part 1 is included as `Assets/ascii-logo.png`.
- The Part 1 voice greeting is included as `Assets/welcome.wav` and played by `VoiceGreetingPlayer.cs`.

### 2. Keyword recognition

- `KeywordResponder.cs` recognises cybersecurity keywords including password, phishing, scam, privacy, malware, suspicious links, safe browsing, social engineering, two-factor authentication, public Wi-Fi, ransomware, data breaches, and antivirus.
- Each topic has targeted responses.

### 3. Random responses

- `KeywordResponder.cs` stores multiple responses for each topic in `Dictionary<string, List<string>>`.
- A `Random` object selects different responses so the conversation does not feel repetitive.

### 4. Conversation flow

- `MemoryStore.cs` remembers the last topic.
- `ChatBot.cs` handles follow-up phrases such as `another tip`, `tell me more`, `explain more`, `more details`, and `continue` without restarting the conversation.

### 5. Memory and recall

- `MemoryStore.cs` remembers the user's name, favourite cybersecurity topic, current topic, and topics discussed.
- The bot can answer memory questions such as `What is my name?` and `What topic am I interested in?`.

### 6. Sentiment detection

- `SentimentDetector.cs` recognises simple sentiments such as worried, curious, frustrated, and confused.
- The bot adjusts its response by adding encouragement or clear support before giving the cybersecurity tip.

### 7. Error handling and edge cases

- Empty inputs are handled.
- Unknown messages receive a default rephrasing response.
- `App.xaml.cs` includes a global WPF error handler so unexpected UI errors do not crash the app immediately.

### 8. Code optimisation

- The project is split into clear classes:
  - `ChatBot.cs`
  - `KeywordResponder.cs`
  - `SentimentDetector.cs`
  - `MemoryStore.cs`
  - `VoiceGreetingPlayer.cs`
  - `AsciiLogoProvider.cs`
  - `ChatMessage.cs`
- The project uses generic collections such as dictionaries and lists.
- The project uses a delegate named `ResponseFormatter` in `ChatBot.cs` to format responses.

## Files to submit

Submit the complete project folder containing:

- `CyberSecurityChatbotPart2.sln`
- `CyberSecurityChatbotPart2.csproj`
- `MainWindow.xaml`
- `MainWindow.xaml.cs`
- all class files in `Services` and `Models`
- `Assets/welcome.wav`
- `Assets/ascii-logo.png`
- `README.md`
- `.gitignore`

## GitHub submission checklist

Use a public or accessible GitHub repository. Test the repository link in a private/incognito browser before submitting it.

Recommended commit plan:

```bash
git init
git add .
git commit -m "Initial WPF project setup"
git commit --allow-empty -m "Add GUI layout and chat interface"
git commit --allow-empty -m "Add keyword recognition and random responses"
git commit --allow-empty -m "Add memory and follow-up conversation flow"
git commit --allow-empty -m "Add sentiment detection and error handling"
git commit --allow-empty -m "Add README, assets, and submission checklist"
```

Recommended tags/releases:

```bash
git tag -a v1.0.0 -m "Base WPF GUI with Part 1 features"
git tag -a v1.1.0 -m "Dynamic keyword responses and memory"
git tag -a v1.2.0 -m "Sentiment detection, polish, and final README"
git push -u origin main --tags
```

Create GitHub releases for the tags so the marker can clearly see the release history.

## Video presentation checklist

In your video, show:

1. The project running in Visual Studio.
2. The GUI layout and ASCII art.
3. Voice greeting playback.
4. Keyword examples: password, phishing, privacy, scam.
5. Random responses by asking the same topic more than once.
6. Follow-up conversation: `another tip` and `explain more`.
7. Memory: name and favourite topic recall.
8. Sentiment: `I'm worried about online scams`.
9. Unknown input error handling.
10. Code structure: explain `ChatBot`, `KeywordResponder`, `SentimentDetector`, and `MemoryStore`.
