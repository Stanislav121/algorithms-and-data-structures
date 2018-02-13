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

            var result = Add(value, _headNode);
            return result;
        }

        private bool Add(T value, BinaryTreeNode<T> node)
        {
            var result = _comparer.Compare(value, node.Value);
            if (result == 0)
                return false;

            if (result > 0)
            {
                if (node.RightNode == null)
                {
                    node.SetRightNode(value);
                    return true;
                }
                return Add(value, node.RightNode);
            }
            else
            {
                if (node.LeftNode == null)
                {
                    node.SetLeftNode(value);
                    return true;
                }
                return Add(value, node.LeftNode);
            }
        }

        #endregion

        public bool DeleteNode(T value)
        {
            var contain = ContainsValue(value);
            if (!contain)
                return false;

            var nodeForDelete = GetNode(value);
            var leftBranch = nodeForDelete.LeftNode;
            var rightBranch = nodeForDelete.RightNode;
            if (nodeForDelete.HeadNode == null)
            {
                if (rightBranch == null && leftBranch == null)
                {
                    _headNode = null;
                    return true;
                }
                if (rightBranch == null)
                {
                    _headNode = leftBranch;
                    _headNode.DiscardHeadtNode();
                    return true;
                }
                if (leftBranch == null)
                {
                    _headNode = rightBranch;
                    _headNode.DiscardHeadtNode();
                    return true;
                }

                _headNode = rightBranch;
                _headNode.DiscardHeadtNode();
                var minLeaf = GetMin();
                minLeaf.SetLeftNode(leftBranch);
                return true;
            }
            else
            {
                var newHeadNode = nodeForDelete.HeadNode;
                if (rightBranch == null && leftBranch == null)
                {
                    UpdateNode(nodeForDelete, null);
                    return true;
                }
                if (rightBranch == null)
                {
                    UpdateNode(nodeForDelete, leftBranch);
                    nodeForDelete.AddHeadtNode(newHeadNode);
                    return true;
                }
                if (leftBranch == null)
                {
                    UpdateNode(nodeForDelete, rightBranch);
                    nodeForDelete.AddHeadtNode(newHeadNode);
                    return true;
                }

                UpdateNode(nodeForDelete, rightBranch);
                nodeForDelete.AddHeadtNode(newHeadNode);
                var minLeaf = GetMin(rightBranch);
                minLeaf.SetLeftNode(leftBranch);
                return true;
            }
        }

        /// <summary>
        /// Note! This method must be private, otherwise someone use it and break consistensy of this tree
        /// </summary>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        public void UpdateNode(BinaryTreeNode<T> oldNode, BinaryTreeNode<T> newNode)
        {
            var headNode = oldNode.HeadNode;
            if (headNode.LeftNode == null)
            {
                headNode.SetRightNode(newNode);
                return;
            }
            if (headNode.RightNode == null)
            {
                headNode.SetLeftNode(newNode);
                return;
            }

            if (_comparer.Compare(headNode.LeftNode.Value, oldNode.Value) == 0)
            {
                headNode.SetLeftNode(newNode);
                return;
            }
            if (_comparer.Compare(headNode.RightNode.Value, oldNode.Value) == 0)
            {
                headNode.SetRightNode(newNode);
                return;
            }
        }

        //private bool Displace(ref BinaryTreeNode<T> displacedNode, BinaryTreeNode<T> rightBranch, BinaryTreeNode<T> leftBranch)
        //{
            
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
