using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateApplication
{
    class Program
    {
        private delegate int Del(string name);

        private static int ConvertToInt32(string name)
        {
            Console.WriteLine(Convert.ToInt32(name));
            return Convert.ToInt32(name);
        }

        private static int DoNothing(string name)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            var x = new List<string>().ToDictionary(GetKey, GetKey);
            int number;
            string name = args.Length > 0 ? args[0] : "28";
            Del afunctionPointer = ConvertToInt32;

            if (!int.TryParse(name, out number))
                afunctionPointer = DoNothing;

            afunctionPointer = afunctionPointer + (s => {Console.WriteLine(4);
                                                            return 4;
            });

            afunctionPointer += (s => ConvertToInt32(s) + 12);

            afunctionPointer(name);

            DoStuff(4, s =>
            {
                Console.WriteLine("failed");
                return 1;
            });

            Console.ReadLine();
        }

        private static string GetKey(string s)
        {
            return s;
        }

        private static void DoStuff(int a, Del callback)
        {
            Console.WriteLine(a);
            callback(a.ToString());
        }
    }
}
