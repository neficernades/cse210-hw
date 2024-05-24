using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool Completed { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        Completed = false;
    }

    public abstract int Complete();
    public override string ToString()
    {
        return $"{(Completed ? "[X]" : "[ ]")} {Name}";
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int Complete()
    {
        Completed = true;
        return Points;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int Complete()
    {
        return Points;
    }
}

class ChecklistGoal : Goal
{
    public int CurrentCount { get; set; }
    public int TargetCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) 
        : base(name, points)
    {
        CurrentCount = 0;
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
    }

    public override int Complete()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount)
        {
            Completed = true;
            return Points + BonusPoints;
        }
        return Points;
    }

    public override string ToString()
    {
        return $"{(Completed ? "[X]" : "[ ]")} {Name} (Completed {CurrentCount}/{TargetCount} times)";
    }
}
class User
{
    public string Name { get; set; }
    public List<Goal> Goals { get; set; }
    public int Score { get; set; }

    public User(string name)
    {
        Name = name;
        Goals = new List<Goal>();
        Score = 0;
    }

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public int RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < Goals.Count)
        {
            int points = Goals[goalIndex].Complete();
            Score += points;
            return points;
        }
        return 0;
    }

    public void ShowGoals()
    {
        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Goals[i]}");
        }
    }

    public void SaveToFile(string filename)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filename, data);
    }

    public static User LoadFromFile(string filename)
    {
        var data = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<User>(data);
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Eternal Quest!");
        Console.Write("Enter your name: ");
        string userName = Console.ReadLine();
        User user = new User(userName);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. Show goals");
            Console.WriteLine("4. Show score");
            Console.WriteLine("5. Save progress");
            Console.WriteLine("6. Load progress");
            Console.WriteLine("7. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal type (simple, eternal, checklist): ");
                    string goalType = Console.ReadLine().Trim().ToLower();
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine().Trim();
                    Console.Write("Enter goal points: ");
                    int goalPoints = int.Parse(Console.ReadLine().Trim());

                    switch (goalType)
                    {
                        case "simple":
                            user.AddGoal(new SimpleGoal(goalName, goalPoints));
                            break;
                        case "eternal":
                            user.AddGoal(new EternalGoal(goalName, goalPoints));
                            break;
                        case "checklist":
                            Console.Write("Enter target count: ");
                            int targetCount = int.Parse(Console.ReadLine().Trim());
                            Console.Write("Enter bonus points: ");
                            int bonusPoints = int.Parse(Console.ReadLine().Trim());
                            user.AddGoal(new ChecklistGoal(goalName, goalPoints, targetCount, bonusPoints));
                            break;
                        default:
                            Console.WriteLine("Invalid goal type.");
                            break;
                    }
                    Console.WriteLine("Goal added!");
                    break;
                
                case "2":
                    user.ShowGoals();
                    Console.Write("Enter goal number to record: ");
                    int goalIndex = int.Parse(Console.ReadLine().Trim()) - 1;
                    int points = user.RecordEvent(goalIndex);
                    if (points > 0)
                    {
                        Console.WriteLine($"Recorded! You earned {points} points.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal number.");
                    }
                    break;
                
                case "3":
                    user.ShowGoals();
                    break;
                
                case "4":
                    Console.WriteLine($"Your score: {user.Score}");
                    break;
                
                case "5":
                    Console.Write("Enter filename to save progress: ");
                    string saveFilename = Console.ReadLine().Trim();
                    user.SaveToFile(saveFilename);
                    Console.WriteLine("Progress saved!");
                    break;
                
                case "6":
                    Console.Write("Enter filename to load progress: ");
                    string loadFilename = Console.ReadLine().Trim();
                    user = User.LoadFromFile(loadFilename);
                    Console.WriteLine("Progress loaded!");
                    break;
                
                case "7":
                    Console.WriteLine("Goodbye!");
                    return;
                
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
