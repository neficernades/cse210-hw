using System;
using System.Collections.Generic;
using System.Threading;

// Namespace para todas las actividades de mindfulness
namespace MindfulnessApp
{
    // Clase base para actividades de mindfulness
    public abstract class MindfulnessActivity
    {
        protected int duration;

        public MindfulnessActivity(int duration)
        {
            this.duration = duration;
        }

        public void DisplayStartMessage(string activityName, string description)
        {
            Console.WriteLine($"Starting {activityName} Activity");
            Console.WriteLine(description);
            Console.WriteLine($"Duration: {duration} seconds");
            Console.WriteLine("Prepare to begin...");
            DisplayAnimation(3);
        }

        public void DisplayEndMessage(string activityName)
        {
            Console.WriteLine($"Good job! You've completed the {activityName} activity for {duration} seconds.");
            DisplayAnimation(3);
        }

        public void DisplayAnimation(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"{i}... ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        public abstract void RunActivity();
    }

    // Clase para la actividad de respiración
    public class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity(int duration) : base(duration) { }

        public override void RunActivity()
        {
            DisplayStartMessage("Breathing", 
                "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");

            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                Console.WriteLine("Breathe in...");
                DisplayAnimation(3);
                Console.WriteLine("Breathe out...");
                DisplayAnimation(3);
            }

            DisplayEndMessage("Breathing");
        }
    }

    // Clase para la actividad de reflexión
    public class ReflectionActivity : MindfulnessActivity
    {
        private static readonly string[] Prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private static readonly string[] Questions = {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity(int duration) : base(duration) { }

        public override void RunActivity()
        {
            DisplayStartMessage("Reflection", 
                "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");

            var random = new Random();
            string prompt = Prompts[random.Next(Prompts.Length)];
            Console.WriteLine(prompt);

            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                string question = Questions[random.Next(Questions.Length)];
                Console.WriteLine(question);
                DisplayAnimation(5);
            }

            DisplayEndMessage("Reflection");
        }
    }

    // Clase para la actividad de listado
    public class ListingActivity : MindfulnessActivity
    {
        private static readonly string[] Prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        private List<string> items;

        public ListingActivity(int duration) : base(duration)
        {
            items = new List<string>();
        }

        public override void RunActivity()
        {
            DisplayStartMessage("Listing", 
                "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

            var random = new Random();
            string prompt = Prompts[random.Next(Prompts.Length)];
            Console.WriteLine(prompt);

            Console.WriteLine("You have a few seconds to think...");
            DisplayAnimation(5);

            Console.WriteLine("Start listing:");
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                string item = Console.ReadLine();
                if (!string.IsNullOrEmpty(item))
                {
                    items.Add(item);
                }
            }

            Console.WriteLine($"You have listed {items.Count} items.");
            DisplayEndMessage("Listing");
        }
    }

public class GratitudeActivity : MindfulnessActivity
{
    public GratitudeActivity(int duration) : base(duration) {}

    public override void RunActivity()
    {
        DisplayStartMessage("Gratitude", 
            "This activity will help you reflect on the things you are grateful for in your life.");

        Console.WriteLine("List things you are grateful for:");
        DateTime startTime = DateTime.Now;
        List<string> gratitudeList = new List<string>();

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                gratitudeList.Add(item);
            }
        }

        Console.WriteLine($"You have listed {gratitudeList.Count} items.");
        DisplayEndMessage("Gratitude");
    }
}

   class Program
{
    private static Dictionary<string, int> activityCounts = new Dictionary<string, int>();

    static void Main(string[] args)
    {
        var activities = new Dictionary<string, Func<int, MindfulnessActivity>>()
        {
            { "1", duration => new BreathingActivity(duration) },
            { "2", duration => new ReflectionActivity(duration) },
            { "3", duration => new ListingActivity(duration) },
            { "4", duration => new GratitudeActivity(duration) }
        };

        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Gratitude");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            if (choice == "5")
            {
                SaveLog();
                break;
            }

            if (activities.ContainsKey(choice))
            {
                Console.Write("Enter the duration of the activity in seconds: ");
                if (int.TryParse(Console.ReadLine(), out int duration))
                {
                    var activity = activities[choice](duration);
                    activity.RunActivity();
                    if (activityCounts.ContainsKey(choice))
                    {
                        activityCounts[choice]++;
                    }
                    else
                    {
                        activityCounts[choice] = 1;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid duration. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private static void SaveLog()
    {
        using (var writer = new System.IO.StreamWriter("activity_log.txt"))
        {
            foreach (var entry in activityCounts)
            {
                writer.WriteLine($"Activity {entry.Key} performed {entry.Value} times.");
            }
        }
    }
}
}



