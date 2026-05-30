using System.Text.RegularExpressions;

namespace CyberSecurityChatbotPart2.Services;

public sealed class KeywordResponder
{
    private readonly Random _random = new();

    private readonly Dictionary<string, List<string>> _topicKeywords = new(StringComparer.OrdinalIgnoreCase)
    {
        ["password"] = new List<string> { "password", "passwords", "passcode", "credential", "credentials", "login code" },
        ["phishing"] = new List<string> { "phishing", "phish", "fake email", "email scam" },
        ["scam"] = new List<string> { "scam", "scams", "fraud", "fraudster", "online scam", "banking scam" },
        ["privacy"] = new List<string> { "privacy", "private", "personal information", "personal data", "data privacy" },
        ["safe browsing"] = new List<string> { "safe browsing", "browsing", "browser", "https", "website", "websites", "downloads", "pop-up", "popup" },
        ["suspicious links"] = new List<string> { "suspicious link", "suspicious links", "link", "links", "shortened link", "url" },
        ["malware"] = new List<string> { "malware", "virus", "viruses", "trojan", "spyware", "adware" },
        ["social engineering"] = new List<string> { "social engineering", "manipulate", "manipulation", "impersonation", "pretend to be" },
        ["two-factor authentication"] = new List<string> { "2fa", "mfa", "two factor", "two-factor", "multi factor", "authenticator" },
        ["public wi-fi"] = new List<string> { "public wifi", "public wi-fi", "free wifi", "free wi-fi", "hotspot" },
        ["ransomware"] = new List<string> { "ransomware", "encrypted files", "file ransom" },
        ["data breach"] = new List<string> { "data breach", "breach", "leaked data", "data leak" },
        ["antivirus"] = new List<string> { "antivirus", "anti-virus", "security software" }
    };

    private readonly Dictionary<string, List<string>> _responses = new(StringComparer.OrdinalIgnoreCase)
    {
        ["password"] = new List<string>
        {
            "Use strong, unique passwords for each account. A good password is long, avoids personal details, and mixes letters, numbers, and symbols.",
            "Never reuse the same password on many accounts. If one site is hacked, criminals may try that same password elsewhere.",
            "A password manager can help you create and store strong passwords safely instead of writing them down or using easy ones."
        },
        ["phishing"] = new List<string>
        {
            "Be cautious of emails or messages asking for passwords, banking details, or personal information. Scammers often pretend to be trusted organisations.",
            "Before you click, check the sender address, spelling, links, and urgency. Phishing messages often pressure you to act quickly.",
            "If a message says your account will be closed, do not use the link in the message. Open the official website or app yourself."
        },
        ["scam"] = new List<string>
        {
            "Online scams often use fear, prizes, romance, fake jobs, or urgent payment requests. Pause and verify before sending money or information.",
            "If someone asks for gift cards, instant payments, or banking OTPs, treat it as a serious warning sign and stop the conversation.",
            "Scammers can sound professional. Verify requests through an official phone number or website, not through the contact details in the suspicious message."
        },
        ["privacy"] = new List<string>
        {
            "Protect your privacy by sharing less personal information online and reviewing who can see your posts, photos, and profile details.",
            "Check app permissions regularly. Remove access to your camera, microphone, contacts, or location when an app does not need it.",
            "Use privacy settings on social media so only trusted people can see personal posts, contact details, or location information."
        },
        ["safe browsing"] = new List<string>
        {
            "Safe browsing means visiting trusted websites, checking for HTTPS, avoiding risky downloads, and not clicking suspicious pop-ups.",
            "Keep your browser updated because updates often fix security weaknesses that criminals can attack.",
            "Avoid downloading files from unknown websites. If a download looks unexpected or too good to be true, leave the site."
        },
        ["suspicious links"] = new List<string>
        {
            "Before clicking a link, hover over it or preview it to check whether the address looks real. Avoid strange shortened links from unknown senders.",
            "Do not click links that arrive with urgent threats or prizes. Go to the official website manually instead.",
            "A suspicious link may contain misspelled company names, extra numbers, or unusual domains. When in doubt, do not click."
        },
        ["malware"] = new List<string>
        {
            "Malware is harmful software that can damage your device or steal information. Avoid unknown downloads and keep your antivirus software updated.",
            "Do not open unexpected attachments, especially files ending in .exe, .scr, .bat, or compressed folders from people you do not trust.",
            "If your device suddenly becomes slow, shows strange pop-ups, or opens apps by itself, scan it with trusted security software."
        },
        ["social engineering"] = new List<string>
        {
            "Social engineering is when attackers manipulate people into revealing sensitive information. They often use urgency, fear, or fake authority.",
            "If someone pressures you to act immediately, slow down. Real organisations should allow you time to verify the request.",
            "Never share passwords or OTP codes with anyone, even if they claim to be from your bank, school, or support team."
        },
        ["two-factor authentication"] = new List<string>
        {
            "Two-factor authentication adds an extra security step after your password, such as a code or authenticator app approval.",
            "Use an authenticator app where possible because it is usually safer than receiving codes by SMS.",
            "Never approve a two-factor login request that you did not start. That may mean someone already knows your password."
        },
        ["public wi-fi"] = new List<string>
        {
            "Avoid logging into banking or sensitive accounts on public Wi-Fi. Use mobile data or a trusted VPN for sensitive tasks.",
            "On public Wi-Fi, turn off file sharing and avoid sending private information unless the website uses HTTPS.",
            "Fake public Wi-Fi networks can copy the names of real ones. Confirm the official network name before connecting."
        },
        ["ransomware"] = new List<string>
        {
            "Ransomware locks or encrypts files and demands payment. Regular backups are one of the best protections.",
            "Do not open unexpected attachments because ransomware often arrives through fake invoices, delivery notices, or urgent documents.",
            "Keep your operating system and apps updated to reduce ransomware risks from known security weaknesses."
        },
        ["data breach"] = new List<string>
        {
            "After a data breach, change the affected password immediately and change it anywhere else you reused it.",
            "Watch for suspicious emails after a breach because criminals may use leaked information to make scams look personal.",
            "Use unique passwords and two-factor authentication so one leaked password does not open all your accounts."
        },
        ["antivirus"] = new List<string>
        {
            "Antivirus software helps detect harmful files, but it works best with safe habits like avoiding suspicious downloads.",
            "Keep your antivirus and operating system updated so they can recognise newer threats.",
            "Run a scan if your device behaves strangely, but do not install random 'cleaner' apps from pop-up adverts."
        }
    };

    private readonly Dictionary<string, string> _detailedExplanations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["password"] = "Password safety matters because passwords protect your identity, messages, photos, school accounts, banking apps, and social media. Attackers often guess weak passwords or reuse leaked passwords from old breaches.",
        ["phishing"] = "Phishing works by pretending to be a trusted person or organisation. The attacker wants you to click a link, open an attachment, or share private information before you think carefully.",
        ["scam"] = "Scams are designed to make you react emotionally. They may use fear, greed, romance, fake emergencies, or fake authority to push you into a risky action.",
        ["privacy"] = "Privacy is about controlling what information others can see or collect about you. Less exposed information makes it harder for criminals to impersonate or target you.",
        ["safe browsing"] = "Safe browsing reduces the chance of visiting dangerous sites, downloading harmful files, or entering details into fake pages.",
        ["suspicious links"] = "Suspicious links can send you to fake login pages or download malware. Always inspect links carefully before opening them.",
        ["malware"] = "Malware includes viruses, spyware, trojans, ransomware, and other harmful programs. It can steal data, damage files, or spy on your activity.",
        ["social engineering"] = "Social engineering attacks the person instead of the computer. The best defence is to slow down, verify, and refuse to share sensitive information.",
        ["two-factor authentication"] = "Two-factor authentication protects you even when a password is stolen because the attacker still needs the second approval step.",
        ["public wi-fi"] = "Public Wi-Fi is risky because other people may be on the same network and fake hotspots can be created to trick users.",
        ["ransomware"] = "Ransomware is dangerous because it blocks access to your own files. Backups and careful email habits reduce the impact.",
        ["data breach"] = "A data breach means information was exposed from a service or organisation. The safest response is to change passwords and watch for follow-up scams.",
        ["antivirus"] = "Antivirus tools detect many threats, but they cannot replace careful browsing, updates, strong passwords, and good judgement."
    };

    public string? DetectTopic(string input)
    {
        foreach (KeyValuePair<string, List<string>> entry in _topicKeywords)
        {
            if (entry.Value.Any(keyword => ContainsKeyword(input, keyword)))
            {
                return entry.Key;
            }
        }

        return null;
    }

    public bool IsKnownTopic(string topic) => _responses.ContainsKey(topic);

    public string GetRandomResponse(string topic)
    {
        if (!_responses.TryGetValue(topic, out List<string>? possibleResponses) || possibleResponses.Count == 0)
        {
            return "I know this is related to cybersecurity, but I do not have a detailed tip for it yet.";
        }

        int selectedIndex = _random.Next(possibleResponses.Count);
        return possibleResponses[selectedIndex];
    }

    public string GetDetailedResponse(string topic)
    {
        if (!_detailedExplanations.TryGetValue(topic, out string? explanation))
        {
            return GetRandomResponse(topic);
        }

        return explanation + Environment.NewLine + Environment.NewLine + "Practical tip: " + GetRandomResponse(topic);
    }

    public string GetTopicsList()
    {
        return string.Join(", ", _responses.Keys.OrderBy(topic => topic));
    }

    private static bool ContainsKeyword(string input, string keyword)
    {
        string pattern = $@"(?<![A-Za-z0-9]){Regex.Escape(keyword)}(?![A-Za-z0-9])";
        return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    }
}
