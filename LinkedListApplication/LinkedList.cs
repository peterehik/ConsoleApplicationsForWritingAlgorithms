using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApplication
{
    public class LinkedList
    {
        private Node _head;

        public void Reverse()
        {
            _head = Reverse(_head, null);
        }

        private Node Reverse(Node curNode, Node prevNode)
        {
            if (curNode != null)
            {
                var nextNode = curNode.Next;
                curNode.Next = prevNode;
                return Reverse(nextNode, curNode);
            }
            return prevNode;
        }

        public void RemoveDuplicates()
        {
            var curNode = _head;
            var store = new Dictionary<int, int>();
            while(curNode != null)
            {
                if (!store.ContainsKey(curNode.Key))
                {
                    store.Add(curNode.Key, 1);
                }
                else
                {
                    store[curNode.Key] += 1;
                }
                curNode = curNode.Next;
            }
            var duplicates = store.Where(kvp => kvp.Value > 1)
            .Select(kvp => kvp.Key).ToList();

            foreach(var item in duplicates)
            {
                RemoveNode(item);
            }
        }

        private void RemoveNode(int key)
        {
            Node curNode = _head; 
            Node prevNode = null;

            while (curNode != null)
            {
                if (key == curNode.Key)
                {
                    if (prevNode != null)
                    {
                        prevNode.Next = curNode.Next;
                    }
                    else
                    {
                        _head = curNode.Next;
                    }
                    curNode = curNode.Next;
                }
                else
                {
                    prevNode = curNode;
                    curNode = curNode.Next;
                }
            }
        }

        public LinkedList(int head)
        {
            _head = new Node(head);
        }

        public Node FindNode(int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode != null)
            {
                if (curNode.Key == key)
                    return curNode;
                curNode = curNode.Next;
            }

            return curNode;
        }

        public void AppendBefore(Node node, int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode != null)
            {
                if (curNode.Key == node.Key)
                {
                    newNode.Next = curNode;
                    if (curNode.Parent != null)
                    {
                        curNode.Parent.Next = newNode;
                        newNode.Parent = curNode.Parent;
                    }
                    curNode.Parent = newNode;
                    return;
                }

                curNode = curNode.Next;
            }

           
        }

        public void AppendAfter(Node node, int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode.Next != null)
            {
                if (curNode.Key == node.Key)
                {
                    newNode.Next = curNode.Next;
                    curNode.Next.Parent = newNode;
                    curNode.Next = newNode;
                    newNode.Parent = curNode;
                    return;
                }
                
                curNode = curNode.Next;
            }

            if (curNode.Key == node.Key)
            {
                curNode.Next = newNode;
                newNode.Parent = curNode;
            }

            
        }

        public void Append(int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode.Next != null)
            {
                curNode = curNode.Next;
            }

            curNode.Next = newNode;
            newNode.Parent = curNode;
        }

        public void DeleteNode(Node node)
        {
            if (node.Parent != null)
            {
                node.Parent.Next = node.Next;
                
            }
            if (node.Next != null)
            {
                node.Next.Parent = node.Parent;
            }
        }

        public override string ToString()
        {
            string s = _head.Key.ToString();
            Node curNode = _head.Next;
            while (curNode != null)
            {
                s += "->" + curNode.Key;
                curNode = curNode.Next;
            }
            return s;
        }
    }
}
