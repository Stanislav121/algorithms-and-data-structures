using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine(tree.Count());
            Console.ReadLine();
            //Second first commit on new machine
        }
    }
}
