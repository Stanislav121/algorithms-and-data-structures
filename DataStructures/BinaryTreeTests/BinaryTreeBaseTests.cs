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
            _tree = new BinaryTree<int>(Comparer<int>.Default) {8, 17, 3, 5, 11, 47, 51, 63, 18, 15, 9, 16, 2, 1};

            _treeWithOneNode = new BinaryTree<int>(Comparer<int>.Default) {42};

            _emptyTree = new BinaryTree<int>(Comparer<int>.Default);
        }

        [Test]
        public void TestAdd()
        {
            var expectedList = new List<int> { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 47, 18, 51, 63 };
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
            var expectedList = new List<int> { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 12, 18, 51, 63 };
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
            Assert.AreEqual(_tree.GetMin().Value, 1);
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

        [Test, Ignore("Need implement")]
        public void TestUpdateNode()
        {
            Assert.IsTrue(false);
        }

        #region DeleteNode

        [TestCase(47, ExpectedResult = new[] { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 51, 18, 63 })]
        [TestCase(63, ExpectedResult = new[] { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 47, 18, 51 })]
        [TestCase(3, ExpectedResult = new[] { 8, 5, 2, 1, 17, 11, 9, 15, 16, 47, 18, 51, 63 })]
        [TestCase(17, ExpectedResult = new[] { 8, 3, 2, 1, 5, 47, 18, 11, 9, 15, 16, 51, 63 })]
        [TestCase(16, ExpectedResult = new[] { 8, 3, 2, 1, 5, 17, 11, 9, 15, 47, 18, 51, 63 })] // delete node without branchs
        [TestCase(5, ExpectedResult = new[] { 8, 3, 2, 1, 17, 11, 9, 15, 16, 47, 18, 51, 63 })] // delete node without branchs
        [TestCase(51, ExpectedResult = new[] { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 47, 18, 63 })] // delete node with only right branch
        [TestCase(2, ExpectedResult = new[] { 8, 3, 1, 5, 17, 11, 9, 15, 16, 47, 18, 51, 63 })] // delete node with only left branch
        [TestCase(8, ExpectedResult = new[] { 17, 11, 9, 3, 2, 1, 5, 15, 16, 47, 18, 51, 63 })] // delete head
        [TestCase(0, ExpectedResult = new[] { 8, 3, 2, 1, 5, 17, 11, 9, 15, 16, 47, 18, 51, 63 })] // delete not existed node
        public int[] TestDelete(int deletedValue)
        {
            _tree.DeleteNode(deletedValue);
            
            var listFromTree = new List<int>();
            foreach (var value in _tree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(_tree.CheckConsistency());
            return listFromTree.ToArray();
        }

        [Test]
        public void TestDeleteTreeWithOneNode()
        {
            _treeWithOneNode.DeleteNode(42);
            var expectedList = new List<int> ();
            var listFromTree = new List<int>();
            foreach (var value in _treeWithOneNode)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(_treeWithOneNode.CheckConsistency());
            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
        }

        [Test]
        public void TestDeleteEmptyTree()
        {
            var isDeleted = _emptyTree.DeleteNode(42);
            var expectedList = new List<int>();
            var listFromTree = new List<int>();
            foreach (var value in _emptyTree)
            {
                listFromTree.Add(value);
            }

            Assert.IsTrue(_emptyTree.CheckConsistency());
            Assert.IsTrue(expectedList.SequenceEqual(listFromTree));
            Assert.IsFalse(isDeleted);
        }

        #endregion

        [Test, Ignore("Need implement")]
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
