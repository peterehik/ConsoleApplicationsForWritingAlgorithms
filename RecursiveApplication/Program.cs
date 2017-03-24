using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveApplication
{
    class Program
    {
        public static int Factorial(int num)
        {
            if (num == 0)
                return 1;

            return num * Factorial(num-1);
        }

        public static int Fibonacci(int num)
        {
            if (num <= 1)
                return num;

            return Fibonacci(num - 1) + Fibonacci(num - 2);

        }

        static void Main(string[] args)
        {
            Console.WriteLine(Factorial(6));
            Console.WriteLine(Fibonacci(7));
            Console.WriteLine(IsCircular("GLGLGLG"));
            Console.WriteLine(IsCircular("GLLG"));
            Console.WriteLine(IsCircular("GRRG"));
            Console.WriteLine(IsCircular("GRGLG"));
            Console.WriteLine(CountSquares(2,3));
            Console.WriteLine(CountSquares(4, 25));

            Console.ReadKey();

        }

        public static char GetDirection(char currentDir, char leftOrRight)
        {
            if (currentDir == 'N')
            {
                if (leftOrRight == 'L')
                    return 'W';
                return 'E';
            }
            if (currentDir == 'S')
            {
                if (leftOrRight == 'L')
                    return 'E';
                return 'W';
            }
            if (currentDir == 'W')
            {
                if (leftOrRight == 'L')
                    return 'S';
                return 'N';
            }
            if (currentDir == 'E')
            {
                if (leftOrRight == 'L')
                    return 'N';
                return 'S';
            }

            throw new NotImplementedException(currentDir.ToString());
        }

        public static bool IsCircular(string commands)
        {
            int x = 0, y = 0;
            char currentDir = 'N';

            for (int i = 0; i < commands.Length; i++)
            {
                char command = commands[i];
                if (command == 'L' || command == 'R')
                {
                    currentDir = GetDirection(currentDir, command);
                }
                else if (command == 'G')
                {
                    if (currentDir == 'N')
                        y++;
                    else if (currentDir == 'S')
                        y--;
                    else if (currentDir == 'E')
                        x++;
                    else
                    {
                        x--;
                    }
                }
            }

            return x == 0 && y == 0;
        }

        public static int CountSquares(int a, int b)
        {
            return (int) Math.Floor(Math.Sqrt(b)) - (int) Math.Ceiling(Math.Sqrt(a)) + 1;
        }
    }
}
