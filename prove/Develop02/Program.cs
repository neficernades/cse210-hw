using System;
using System.Collections.Generic;
using System.IO;

class Entry {
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date) {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString() {
        return $"{Date}: {Prompt}\n{Response}\n";
    }
}

class Journal {
    private List<Entry> entries = new List<Entry>();

    public void WriteNewEntry(string prompt) {
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("MM/dd/yyyy");
        Entry entry = new Entry(prompt, response, date);
        entries.Add(entry);
    }

    public void ShowJournal() {
        foreach (Entry entry in entries) {
            Console.WriteLine(entry);
        }
    }

    public void SaveJournal(string filename) {
        using (StreamWriter writer = new StreamWriter(filename)) {
            foreach (Entry entry in entries) {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadJournal(string filename) {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename)) {
            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] parts = line.Split('|');
                Entry entry = new Entry(parts[1], parts[2], parts[0]);
                entries.Add(entry);
            }
        }
        Console.WriteLine("Journal loaded successfully!");
    }
}

class Program {
    static void Main(string[] args) {
        Journal journal = new Journal();
        bool running = true;

        while (running) {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Show journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice) {
                case "1":
                    Console.WriteLine("Writing a new entry...");
                    journal.WriteNewEntry(GetRandomPrompt());
                    break;
                case "2":
                    Console.WriteLine("Showing journal...");
                    journal.ShowJournal();
                    break;
                case "3":
                    Console.Write("Enter filename to save journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournal(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter filename to load journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournal(loadFilename);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static string GetRandomPrompt() {
        List<string> prompts = new List<string> {
            "Who was the most interesting person you interacted with today?",
            "What was the best part of your day?",
            "How did you see the hand of the Lord in your life today?",
            "What was the strongest emotion you felt today?",
            "If you could do anything today, what would it be?"
        };
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}
