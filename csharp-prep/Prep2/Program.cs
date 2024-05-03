using System;
class Program
{   
    static void Main (string [] args)
    {
        Console.Write("What is your grade percentage? ");
        double percentage = Convert.ToDouble(Console.ReadLine());
        char letter;
        if (percentage >= 90)
        {
            letter = 'A';
        }
        else if (percentage >= 80)
        {
            letter = 'B';
        }
        else if (percentage >= 70)
        {
            letter = 'C';
        }
        else if (percentage >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }
        int lastDigit = (int)percentage % 10;
        char sign = ' ';

        if (lastDigit >= 7)
        {
            sign = '+';
        }
        else if (lastDigit < 3 && letter != 'F')
        {
            sign = '-';
        }
        if (letter == 'A' && sign == '-')
        {
            letter = 'B';
            sign = ' ';
        }
        else if (letter == 'F')
        {
            sign = ' ';
        }
        Console.WriteLine($"Your percentage in letters is : {letter}{sign}");
        if (percentage >= 70)
        {
            Console.WriteLine("Congratulations, you have passed!");

        }
        else
        {
            Console.WriteLine("Try harder next time, I know you can!");
        }
    }
}
