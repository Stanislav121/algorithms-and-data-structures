using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using NUnit.Framework;

namespace BinaryTreeTests
{
    [TestFixture]
    public class BinaryTreeBaseTests
    {
        private BinaryTree<int> _tree;
        private BinaryTree<int> _treeWithOneNode; 
        private BinaryTree<int> _emptyTree;

        [SetUp]
        public void FillTree()
        {
            _tree = new BinaryTree<int>(Comparer<int>.Default);
            _tree.Add(8);
            _tree.Add(17);
            _tree.Add(3);
            _tree.Add(5);
            _tree.Add(11);
            _tree.Add(47);
            _tree.Add(51);
            _tree.Add(63);
            _tree.Add(18);
            _tree.Add(15);
            _tree.Add(9);

            _treeWithOneNode = new BinaryTree<int>(Comparer<int>.Default);
            _treeWithOneNode.Add(42);

            _emptyTree = new BinaryTree<int>(Comparer<int>.Default);
        }

        [Test]
        public void TestAdd()
        {
            var expectedList = new List<int> { 8, 3, 5, 17, 11, 9, 15, 47, 18, 51, 63 };
            var listFromTree = new List<int>();
            foreach (var value in _tree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
        }

        [Test]
        public void TestConsistencyOfTreeValidTree()
        {
            Assert.IsTrue(_tree.CheckConsistency());
            Assert.IsTrue(_tree.CheckConsistency());
            Assert.IsTrue(_tree.CheckConsistency());
        }

        [Test]
        public void TestConsistencyOfTreeInvalidTreeLeft()
        {
            _tree.BreakConsistency(63, 12);
            Assert.IsFalse(_tree.CheckConsistency());
        }

        [Test]
        public void TestConsistencyOfTreeInvalidTreeMiddle()
        {
            _tree.BreakConsistency(47, 12);
            Assert.IsFalse(_tree.CheckConsistency());
        }

        [Test]
        public void TestBreakConsistency()
        {
            _tree.BreakConsistency(47, 12);
            var expectedList = new List<int> { 8, 3, 5, 17, 11, 9, 15, 12, 18, 51, 63 };
            var listFromTree = new List<int>();
            foreach (var value in _tree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
        }

        [Test]
        public void TestGetMin()
        {
            Assert.AreEqual(_tree.GetMin().Value, 3);
            Assert.AreEqual(_treeWithOneNode.GetMin().Value, 42);
            Assert.AreEqual(_emptyTree.GetMin(), null);
        }

        [Test]
        public void TestGetMax()
        {
            Assert.AreEqual(_tree.GetMax().Value, 63);
            Assert.AreEqual(_treeWithOneNode.GetMax().Value, 42);
            Assert.AreEqual(_emptyTree.GetMax(), null);
        }

        [Test]
        public void TestGetNode()
        {
            Assert.AreEqual(_tree.GetNode(47).Value, 47);
            Assert.AreEqual(_treeWithOneNode.GetNode(42).Value, 42);
            Assert.AreEqual(_emptyTree.GetNode(42), null);
        }

        [Test]
        public void TestContainsValue()
        {
            Assert.IsTrue(_tree.ContainsValue(11));
            Assert.IsTrue(_treeWithOneNode.ContainsValue(42));

            Assert.IsFalse(_tree.ContainsValue(12));
            Assert.IsFalse(_treeWithOneNode.ContainsValue(12));
            Assert.IsFalse(_emptyTree.ContainsValue(12));
        }

        [Test]
        public void TestUpdateNode()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void TestDeleteHead()
        {
            _tree.DeleteNode(8);
            var expectedList = new List<int> { 17, 11, 9, 3, 5, 15, 47, 18, 51, 63 };
            var listFromTree = new List<int>();
            foreach (var value in _tree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
            Assert.IsTrue(_tree.CheckConsistency());
        }

        [Test]
        public void TestDelete()
        {
            _tree.DeleteNode(17);
            var expectedList = new List<int> { 8, 3, 5, 47, 18, 11, 9, 15, 51, 63 };
            var listFromTree = new List<int>();
            foreach (var value in _tree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
            Assert.IsTrue(_tree.CheckConsistency());
        }

        [Test]
        public void TestGoAroundTree()
        {
            _tree.GoAroundTree(SomeAction);
            Assert.IsTrue(false);
        }

        private void SomeAction<T>(BinaryTreeNode<T> node)
        {
            
        }
    }
}
