using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        private BinaryTreeNode<T> _headNode;

        private readonly Comparer<T> _comparer;


        public BinaryTree(Comparer<T> comparer)
        {
            _comparer = comparer;
        }

        #region Consistency

        public bool CheckConsistency()
        {
            return CheckConsistency(_headNode);
        }

        private bool CheckConsistency(BinaryTreeNode<T> node)
        {
            if (node == null)
                return true;

            var isNodeValid = CheckConsistencyOfNode(node);
            if (!isNodeValid)
                return false;
            isNodeValid = CheckConsistency(node.LeftNode);
            if (!isNodeValid)
                return false;
            isNodeValid = CheckConsistency(node.RightNode);
            if (!isNodeValid)
                return false;
            return true;
        }

        public bool CheckConsistencyOfNode(BinaryTreeNode<T> node)
        {
            if (node == null)
                return true;

            if (node.LeftNode != null && _comparer.Compare(node.LeftNode.Value, node.Value) >= 0)
                return false;
            if (node.RightNode != null && _comparer.Compare(node.RightNode.Value, node.Value) <= 0)
                return false;
            return true;
        }

        public void BreakConsistency(T valueOld, T valueNew)
        {
            var oldNode = GetNode(valueOld);
            if (oldNode.HeadNode.LeftNode != null && _comparer.Compare(oldNode.HeadNode.LeftNode.Value, valueOld) == 0)
            {
                oldNode.HeadNode.LeftNode.Value = valueNew;
            }
            else
            {
                oldNode.HeadNode.RightNode.Value = valueNew;
            }
        }
        
        #endregion

        #region Add

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

        #endregion

        //public bool DeleteNode(T value)
        //{
        //    var contain = ContainsValue(value);
        //    if (!contain)
        //        return false;

        //    var nodeForDelete = GetNode(value);
        //    // deleted node is head of tree
        //    if (nodeForDelete.HeadNode == null)
        //    {
        //        _headNode = null;
        //        return true;
        //    }
            
        //    if (nodeForDelete.LeftNode == null)
        //    {
        //        var deletedNodeHead = nodeForDelete.HeadNode;
        //        if (_comparer.Compare(deletedNodeHead.LeftNode.Value, value) == 0)
        //        {
        //            deletedNodeHead.AddLeftNode(nodeForDelete.RightNode);
        //            return true;
        //        }
        //        if (_comparer.Compare(deletedNodeHead.RightNode.Value, value) == 0)
        //        {
        //            deletedNodeHead.AddRightNode(nodeForDelete.RightNode);
        //            return true;
        //        }
        //        return false;
        //    }
        //    if (nodeForDelete.RightNode == null)
        //    {
        //        var deletedNodeHead = nodeForDelete.HeadNode;
        //        if (_comparer.Compare(deletedNodeHead.LeftNode.Value, value) == 0)
        //        {
        //            deletedNodeHead.AddLeftNode(nodeForDelete.LeftNode);
        //            return true;
        //        }
        //        if (_comparer.Compare(deletedNodeHead.RightNode.Value, value) == 0)
        //        {
        //            deletedNodeHead.AddRightNode(nodeForDelete.LeftNode);
        //            return true;
        //        }
        //        return false;
        //    }

        //    // If both are zero?
        //    return false;
        //}

        #region GetNode

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
 
        #endregion

        #region GetMin

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

        #endregion

        #region GetMax

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

        #endregion

        #region ContainsValue

        public bool ContainsValue(T value)
        {
            if (_headNode == null)
                return false;
            return ContainsValue(value, _headNode);
        }

        private bool ContainsValue(T value, BinaryTreeNode<T> node)
        {
            if (node == null)
                return false;
            var result = _comparer.Compare(value, node.Value);
            if (result == 0)
                return true;
            if (result < 0)
                return ContainsValue(value, node.LeftNode);
            else
            {
                return ContainsValue(value, node.RightNode);
            }
        }
        
        #endregion

        public void GoAroundTree(Action<BinaryTreeNode<T>> func)
        {
            VisitNode(_headNode, func);
        }

        private void VisitNode(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> func)
        {
            if (node == null)
                return;

            func(node);
            Console.WriteLine(node.Value);
            VisitNode(node.LeftNode, func);
            VisitNode(node.RightNode, func);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>();
            ToList(_headNode, list);
            return list.GetEnumerator();
        }

        private void ToList(BinaryTreeNode<T> node, IList<T> list)
        {
            if (node == null)
                return;

            list.Add(node.Value);
            ToList(node.LeftNode, list);
            ToList(node.RightNode, list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
