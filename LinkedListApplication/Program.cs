using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var linkedList = new LinkedList(5);
            linkedList.Append(10);
            linkedList.Append(15);
            linkedList.Append(20);
            linkedList.Append(30);
            Console.WriteLine(linkedList);
            linkedList.AppendAfter(linkedList.FindNode(15), 17);
            Console.WriteLine(linkedList);
            linkedList.AppendBefore(linkedList.FindNode(20), 18);
            Console.WriteLine(linkedList);
            linkedList.DeleteNode(linkedList.FindNode(20));
            linkedList.Append(30);
            linkedList.Append(10);
            linkedList.Append(10);
            linkedList.Append(5);
            Console.WriteLine(linkedList);
            Console.WriteLine("Reversing");
            linkedList.Reverse();
            Console.WriteLine(linkedList);
            Console.WriteLine("Removing Duplicates");
            linkedList.RemoveDuplicates();
            Console.WriteLine(linkedList);
            Console.ReadLine();
        }
    }
}
