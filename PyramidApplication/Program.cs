using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidApplication
{
    public class Program
    {
        public List<Block> Blocks { get; set; }

        public Program()
        {
            Blocks = new List<Block>();

            int id = 1;

            var head = new Block() { Id = 1, Value = 1, Name = "1" };

            Blocks.Add(head);

            for (int i = 2; i < 5; i++)
            {
                Blocks.Add(new Block() { Id = id * i, Value = id, Name = id.ToString(), ParentId = head.Id });
                for (int j = 1; j < 2; j++)
                {
                    int tempId = (i * 10) + j;
                    Blocks.Add(new Block(tempId, i));
                    for (int k = 6; k < 8; k++)
                    {
                        Blocks.Add(new Block(i * 100 + j * 10 + k, tempId));
                    }
                }
            }



        }
        public static void Main()
        {
            var program = new Program();
            var blocks = program.Blocks;
            Console.WriteLine("Pyramid");
            Console.WriteLine(string.Join(Environment.NewLine, blocks.Select(blk => blk.Id + " {" + blk.ParentId + "}")));

            Console.WriteLine("Recursive Sum: ");
            Console.WriteLine(program.GetTotalRecursively(2));
            Console.WriteLine("Non-Recursive Sub tree:");
            Console.WriteLine(string.Join(", ", program.GetChildrenNonRecursively(2)));
            Console.ReadLine();

        }

        //recursive function to get sum based on id.
        public double GetTotalRecursively(int id)
        {
            var childBlocks = Blocks.Where(blk => blk.ParentId == id).ToList();

            var sum = childBlocks.Sum(r => r.Value);

            foreach (var block in childBlocks)
            {
                sum += GetTotalRecursively(block.Id);
            }

            return sum;


        }

        //recursive function to return id subtree
        public List<int> GetChildrenRecursively(int id)
        {
            var childBlocks = Blocks.Where(blk => blk.ParentId == id).ToList();

            var sum = childBlocks.Select(s => s.Id).ToList();

            foreach (var block in childBlocks)
            {
                sum.AddRange(GetChildrenRecursively(block.Id));
            }

            return sum;


        }

        //non recursive function to return id subtree
        public List<int> GetChildrenNonRecursively(int id)
        {
            List<int> ids = new List<int>();

            List<int> childBlockIds = Blocks.Where(blk => blk.ParentId == id).Select(r => r.Id).ToList();
            ids.AddRange(childBlockIds);
            do
            {
                childBlockIds = Blocks.Where(blk => blk.ParentId.HasValue && childBlockIds.Contains(blk.ParentId.Value)).Select(r => r.Id).ToList();
                ids.AddRange(childBlockIds);

            }
            while (childBlockIds.Any());

            return ids;

        }

    }
}
