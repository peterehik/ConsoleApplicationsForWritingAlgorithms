using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApplication
{
    public class Node
    {
        public int Key { get; set; }
        public Node Parent { get; set; }
        public Node Next { get; set; }

        public Node(int key)
        {
            Key = key;
        }

        public override string ToString()
        {
            if(Parent != null)
                return Key + "{" + Parent.Key + "}";
            return Key.ToString();
        }
    }
}
