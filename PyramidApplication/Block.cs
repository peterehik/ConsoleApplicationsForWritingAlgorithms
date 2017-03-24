using System.Diagnostics;

namespace PyramidApplication
{
    public class Block
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Block(int id, int parentId)
        {
            Id = id;
            Value = id * 10;
            ParentId = parentId;
        }
        public static Block operator +(Block a, Block b)
        {
            return new Block();
        }
        public static bool operator ==(Block a, Block b)
        {
            return ReferenceEquals(a,b);
        }

        public static bool operator !=(Block a, Block b)
        {
            return !(a == b);
        }

        public int this[int i]
        {
            get { return Id; }
            set { Id = value; }
        }

        public Block() { }

    }
}