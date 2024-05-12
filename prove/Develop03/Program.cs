using System;
using System.Collections.Generic;
using System.IO;

class Scripture
{
    public string Reference { get; }
    private List<string> words;

    public Scripture(string reference, string text)
    {
        Reference = reference;
        words = new List<string>(text.Split());
    }

    public string HideWords(int numWords)
    {
        Random random = new Random();
        HashSet<int> hiddenIndices = new HashSet<int>();
        while (hiddenIndices.Count < numWords)
        {
            hiddenIndices.Add(random.Next(words.Count));
        }

        for (int i = 0; i < words.Count; i++)
        {
            if (hiddenIndices.Contains(i))
            {
                words[i] = "_";
            }
        }

        return string.Join(" ", words);
    }

    public bool AllWordsHidden()
    {
        return words.TrueForAll(word => word == "_");
    }
}

class Bible
{
    private List<Scripture> scriptures;

    public Bible(string filename)
    {
        scriptures = new List<Scripture>();
        LoadScriptures(filename);
    }

    private void LoadScriptures(string filename)
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                scriptures.Add(new Scripture(parts[0], parts[1]));
            }
        }
    }

    public Scripture GetRandomScripture()
    {
        Random random = new Random();
        return scriptures[random.Next(scriptures.Count)];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }

    private Bible bible;

    public Program()
    {
        bible = new Bible("scriptures.txt");
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Scripture scripture = bible.GetRandomScripture();
            Console.WriteLine(scripture.Reference);
            Console.WriteLine(string.Join(" ", scripture.HideWords(new Random().Next(1, scripture.Reference.Split().Length + 1))));
            Console.WriteLine("\nPress Enter to reveal more words or type 'exit' to quit.");
            string input = Console.ReadLine().ToLower();
            if (input == "exit")
            {
                break;
            }
            else if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words are hidden. Exiting...");
                break;
            }
        }
    }
}
