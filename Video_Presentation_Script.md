# Video Presentation Script

## Opening

Good day. This is my Part 2 Cybersecurity Awareness Assistant. In Part 1 the chatbot was a console application. For Part 2 I created a WPF graphical user interface and moved the chatbot logic into separate classes so the code is easier to maintain and expand.

## GUI demonstration

This is the main WPF window. It includes a chat display area, a text input box, a Send button, quick topic buttons, a memory status panel, the ASCII art branding from Part 1, and a button to replay the voice greeting.

## Voice greeting and ASCII art

When the application starts, the voice greeting plays from the `Assets/welcome.wav` file. The ASCII art from Part 1 has been translated into the GUI as an image called `ascii-logo.png`.

## Keyword recognition

The chatbot recognises cybersecurity keywords such as password, phishing, scam, privacy, malware, suspicious links, and safe browsing. For example, if I type `Tell me about password safety`, the chatbot recognises the word password and gives a password safety response.

## Random responses

For each topic, I stored multiple responses in lists. The chatbot randomly selects a response so the interaction is not repetitive. I can ask for a phishing tip more than once and receive different tips.

## Conversation flow

The chatbot remembers the current topic. If I ask `another tip` or `explain more`, it continues with the previous topic instead of restarting the conversation.

## Memory and recall

The chatbot stores information provided by the user. If I type `My name is Asemahle`, the chatbot remembers my name. If I type `I'm interested in privacy`, it remembers privacy as my favourite cybersecurity topic. Later I can ask what it remembers.

## Sentiment detection

The chatbot detects simple sentiments. If I type `I'm worried about online scams`, it recognises that I am worried and gives an encouraging response before giving a scam safety tip.

## Error handling

If I type something unknown, the chatbot does not crash. It gives a default response asking me to rephrase and suggests cybersecurity topics.

## Code structure

The main classes are: `ChatBot.cs` for controlling the conversation, `KeywordResponder.cs` for keyword responses and random tips, `SentimentDetector.cs` for tone detection, `MemoryStore.cs` for memory, and `VoiceGreetingPlayer.cs` for the voice greeting. The GUI code is in `MainWindow.xaml` and `MainWindow.xaml.cs`. This structure uses dictionaries, lists, classes, methods, and a delegate called `ResponseFormatter`.

## Closing

This completes the Part 2 requirements: WPF GUI, dynamic responses, keyword recognition, random responses, conversation flow, memory recall, sentiment detection, error handling, and organised code.
