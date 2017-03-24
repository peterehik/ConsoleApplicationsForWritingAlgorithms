namespace BinarySearchTreeApplication
{
    public class Node
    {
        public int Key { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node() { }
        public Node(int key, Node parent)
        {
            Key = key;
            Parent = parent;
        }

        public override string ToString()
        {
            return Key.ToString() + (Parent != null ? " {" + Parent.Key + "}" : "");
        }
    }
}