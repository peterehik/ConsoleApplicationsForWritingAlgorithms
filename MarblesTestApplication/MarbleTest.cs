using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickTest
{
    public class Marbles
    {
        private static readonly Random mRandom = new Random(); // use to generate random color marble

        public const int RED_MARBLE = 1;
        public const int BLUE_MARBLE = 2;
        public const int GREEN_MARBLE = 3;
        public const int ORANGE_MARBLE = 4;

        static void Main(string[] args)
        {
            int red, green, blue, orange;
            string keepPlaying;

            do
            {
                red = PromptInt("Ratio red marbles?");
                green = PromptInt("Ratio green marbles?");
                blue = PromptInt("Ratio blue marbles?");
                orange = PromptInt("Ratio orange marbles?");

                int[] results = Solve(red, green, blue, orange, 1000);
                WriteOutStats(results);

                Console.Write("Enter 'Y' to keep playing or anything else to quit: ");
                keepPlaying = Console.ReadLine();

            } while (keepPlaying == "Y");
        }

        public static int PromptInt(string message)
        {
            int ret = -1;
            while (true)
            {
                Console.WriteLine(message);
                string str = Console.ReadLine();
                if (Int32.TryParse(str, out ret))
                    break;
                else
                    Console.WriteLine("'{0}' is invalid", str);
            }
            return ret;
        }

        public static int[] Solve(int red, int green, int blue, int orange, int count)
        {
            // TOOD: Return an array of integers of length [count]
            // each element in the array should contain a value from 1 to 4
            // the value represents a marble color (see constants above)
            // using the passed in values, you need to infer the probablility of each colored marble.
            // You should then *randomly* generate [count] number of marbles based on that probability

            // (i.e. if you were passed the values 10, 5, 5, 1 for the red, green, blue and orange parameters respectively
            // you should have approximately 10 red marbles for every 5 green and for every five blue marbles, and
            // there should 10 red marbles for approximately every orange marble you get)

            var result = new int[count];

            for (int i = 0; i < count; i++)
                result[i] = GetValue(red, green, blue, orange);

            return result;
        }

        public static void WriteOutStats(int[] results)
        {
            var red = results.Count(r => r == RED_MARBLE);
            var green = results.Count(r => r == GREEN_MARBLE);
            var blue = results.Count(r => r == BLUE_MARBLE);
            var orange = results.Count(r => r == ORANGE_MARBLE);

            Console.WriteLine("Red: {0}", red);
            Console.WriteLine("Green: {0}", green);
            Console.WriteLine("Blue: {0}", blue);
            Console.WriteLine("Orange: {0}", orange);

            // TODO: output the total number of red, green, blue and orange marbles based on the array of results passed into you.
            // This array is the same array you generated in the Solve function above.

        }

        private static int GetValue(double red, double green, double blue, double orange)
        {
            var total = red + green + blue + orange;

            var randomFloat = mRandom.NextDouble(); 
            //returns values between 0 and 1 included meaning we can implement probability by using ranges i.e 1/2 probability is comparable to getting a random value between 0 and 0.5

            if (randomFloat >= 0 && randomFloat < red/total)
                return RED_MARBLE;

            if (randomFloat >= red/total && randomFloat < (red + green)/total)
                return GREEN_MARBLE;

            if (randomFloat >= (red + green)/total && randomFloat < (red + green + blue)/total)
                return BLUE_MARBLE;

            return ORANGE_MARBLE;
        }
       
    }
}