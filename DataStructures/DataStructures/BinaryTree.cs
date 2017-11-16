using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinaryTree<T>
    {
        private BinaryTreeNode<T> _headNode;

        private Comparer<T> _comparer;

        public BinaryTree(Comparer<T> comparer)
        {
            _comparer = comparer;
        }

        //public bool CheckConsistencyYourself()
        //{
        //    if (_headNode == null)
        //        return true;


        //}

        //Bad. 
        public bool CheckNode(BinaryTreeNode<T> node)
        {
            if (node == null)
                return true;

            if (node.LeftNode != null && _comparer.Compare(node.LeftNode.Value, node.Value) >= 0)
                return false;
            if (node.RightNode != null && _comparer.Compare(node.RightNode.Value, node.Value) <= 0)
                return false;
            return true;
        }

        public void BreakConsistency(T value)
        {
            _headNode.LeftNode.AddLeftNode(value);
        }

        public bool Add(T value)
        {
            if (_headNode == null)
            {
                _headNode = new BinaryTreeNode<T>(value);
            }

            var result = Add(value, _headNode, null);
            return result;
        }

        private bool Add(T value, BinaryTreeNode<T> node, BinaryTreeNode<T> headNode)
        {
            var result = _comparer.Compare(value, node.Value);
            if (result == 0)
                return false;

            if (result > 0)
            {
                if (node.RightNode == null)
                {
                    node.AddRightNode(value);
                    return true;
                }
                return Add(value, node.RightNode, node);
            }
            else
            {
                if (node.LeftNode == null)
                {
                    node.AddLeftNode(value);
                    return true;
                }
                return Add(value, node.LeftNode, node);
            }
        }

        //public bool DeleteNode(T value)
        //{
        //    var contain = FindValue(value);
        //    if (!contain)
        //        return false;

        //    if

        //}


        public BinaryTreeNode<T> GetNode(T value)
        {
            if (_headNode == null)
                return null;
            return GetNode(new BinaryTreeNode<T>(value), _headNode);
        }

        private BinaryTreeNode<T> GetNode(BinaryTreeNode<T> node, BinaryTreeNode<T> headNode)
        {
            var result = _comparer.Compare(node.Value, headNode.Value);
            if (result == 0)
                return headNode;
            if (result < 0)
                return GetNode(node, headNode.LeftNode);
            else
            {
                return GetNode(node, headNode.RightNode);
            }
        }

        public BinaryTreeNode<T> GetMin()
        {
            if (_headNode == null)
                return null;
            return GetMin(_headNode);
        }

        private BinaryTreeNode<T> GetMin(BinaryTreeNode<T> node)
        {
            if (node.LeftNode == null)
                return node;
            return GetMin(node.LeftNode);
        }

        public BinaryTreeNode<T> GetMax()
        {
            if (_headNode == null)
                return null;
            return GetMax(_headNode);
        }

        private BinaryTreeNode<T> GetMax(BinaryTreeNode<T> node)
        {
            if (node.RightNode == null)
                return node;
            return GetMax(node.RightNode);
        }

        public bool FindValue(T value)
        {
            if (_headNode == null)
                return false;
            return FindValue(value, _headNode);
        }

        private bool FindValue(T value, BinaryTreeNode<T> node)
        {
            var result = _comparer.Compare(value, node.Value);
            if (result == 0)
                return true;
            if (result < 0)
                return FindValue(value, node.LeftNode);
            else
            {
                return FindValue(value, node.RightNode);
            }
        }

        //public IEnumerator<T> GetEnumerator()
        //{

        //}

        public R GoAroundTree<R>(Func<BinaryTreeNode<T>, R> func)
        {
            return VisitNode(_headNode, func);
        }

        private R VisitNode<R>(BinaryTreeNode<T> node, Func<BinaryTreeNode<T>, R> func)
        {
            if (node == null)
                return func(node);

            Console.WriteLine(node.Value);
            VisitNode(node.LeftNode, func);
            VisitNode(node.RightNode, func);
            return func(node);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
