using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Random random = new Random();
        int[] secret = Enumerable.Range(0, 4).Select(_ => random.Next(1, 7)).ToArray();
        int maxAttempts = 10;

        Console.WriteLine("Welcome to Mastermind!");
        Console.WriteLine("Guess the 4-digit number (digits between 1 and 6). You have 10 attempts.");

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.Write($"Attempt {attempt}: ");
            string guess = Console.ReadLine();

            if (guess.Length != 4 || !guess.All(char.IsDigit) || guess.Any(c => c < '1' || c > '6'))
            {
                Console.WriteLine("Invalid guess. Please enter four digits between 1 and 6.");
                attempt--;
                continue;
            }

            int[] guessDigits = guess.Select(c => int.Parse(c.ToString())).ToArray();

            bool[] secretMatched = new bool[4];
            bool[] guessMatched = new bool[4];

            List<char> plusSigns = new List<char>();
            List<char> minusSigns = new List<char>();

            // Check for correct digits in the correct position
            for (int i = 0; i < 4; i++)
            {
                if (guessDigits[i] == secret[i])
                {
                    plusSigns.Add('+');
                    secretMatched[i] = true;
                    guessMatched[i] = true;
                }
            }

            // Check for correct digits in the wrong position
            for (int i = 0; i < 4; i++)
            {
                if (!guessMatched[i])
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (!secretMatched[j] && guessDigits[i] == secret[j])
                        {
                            minusSigns.Add('-');
                            secretMatched[j] = true;
                            break;
                        }
                    }
                }
            }

            // Combine in correct order: all plus signs first, then minus signs
            Console.WriteLine("Hint: " + new string(plusSigns.Concat(minusSigns).ToArray()));

            if (plusSigns.Count == 4)
            {
                Console.WriteLine("Congratulations! You guessed the number.");
                return;
            }
        }

        Console.WriteLine("Sorry, you've used all your attempts.");
        Console.WriteLine("The correct number was: " + string.Join("", secret));
    }
}
