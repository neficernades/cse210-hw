using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"  Comment by {comment.Name}: {comment.Text}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var video1 = new Video("Introduction to C#", "Alice", 300);
        var video2 = new Video("Advanced C# Concepts", "Bob", 450);
        var video3 = new Video("C# for Data Science", "Charlie", 600);

        video1.AddComment(new Comment("John", "Great video!"));
        video1.AddComment(new Comment("Jane", "Very helpful, thanks!"));
        video2.AddComment(new Comment("Alice", "Excellent content."));
        video2.AddComment(new Comment("Bob", "Very informative."));
        video3.AddComment(new Comment("Charlie", "Loved the examples."));

        var videos = new List<Video> { video1, video2, video3 };

        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
            Console.WriteLine();
        }
    }
}
