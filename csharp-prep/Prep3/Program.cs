using System;
class Program
{   
    static void Main (string [] args)
    {
       Random random = new Random();
        do
        {
            int magicNumber = random.Next(1, 101);
            int guess;
            int guessesCount = 0;

            Console.WriteLine("Guess My Number Game!");
            Console.WriteLine("I have selected a magic number between 1 and 100. Try to guess it!");
            do
            {
                Console.Write("Enter your guess: ");
                guess = Convert.ToInt32(Console.ReadLine());
                guessesCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            } while (guess != magicNumber);

            Console.WriteLine($"Number of guesses: {guessesCount}");

            Console.Write("Do you want to play again? (yes/no): ");
        } while (Console.ReadLine().ToLower() == "yes");
    }
}
