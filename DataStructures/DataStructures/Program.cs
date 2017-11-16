using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinaryTree<int>(Comparer<int>.Default);
            tree.Add(8);
            tree.Add(17);
            tree.Add(3);
            tree.Add(5);
            tree.Add(11);
            tree.Add(47);
            tree.Add(51);
            tree.Add(63);
            tree.Add(18);
            tree.Add(15);
            tree.Add(9);

            tree.BreakConsistency(12);

            if(!tree.GoAroundTree(tree.CheckNode))
                Console.WriteLine("Tree is incorrect"); ;
            Console.WriteLine("Min {0}", tree.GetMin().Value);
            Console.WriteLine("Max {0}", tree.GetMax().Value);

            Console.WriteLine("Find {0}", tree.FindValue(11));

            Console.WriteLine("Get {0}", tree.GetNode(47).Value);

            Console.ReadLine();
        }
    }
}
