﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BinaryTreeTests")]

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

        /// <summary>
        /// Is tree in consistency state? 
        /// Childe node must point to their head node,
        /// Value of node must be larger than left childe,
        /// Value of node must be smaller than right childe
        /// </summary>
        /// <returns>true, if all conditions are met</returns>
        public bool CheckConsistency()
        {
            return CheckConsistency(_headNode);
        }

        private bool CheckConsistency(BinaryTreeNode<T> node)
        {
            if (node == null)
                return true;

            var isNodeValid = CheckConsistencyOfChilds(node);
            if (!isNodeValid)
                return false;
            isNodeValid = CheckConsistency(node.LeftNode) && CheckConsistencyOfHead(node.LeftNode, node);
            if (!isNodeValid)
                return false;
            isNodeValid = CheckConsistency(node.RightNode) && CheckConsistencyOfHead(node.RightNode, node);
            if (!isNodeValid)
                return false;
            return true;
        }

        public bool CheckConsistencyOfHead(BinaryTreeNode<T> node, BinaryTreeNode<T> headNode)
        {
            if (node == null)
                return true;
            return node.HeadNode == headNode;
        }

        public bool CheckConsistencyOfChilds(BinaryTreeNode<T> node)
        {
            if (node == null)
                return true;

            if (node.LeftNode != null && _comparer.Compare(node.LeftNode.Value, node.Value) >= 0)
                return false;
            if (node.RightNode != null && _comparer.Compare(node.RightNode.Value, node.Value) <= 0)
                return false;
            return true;
        }
        
        /// <summary>
        /// This method is intended only for using by Unit tests
        /// Replace old value by new value. It is make tree inconsistency and used for tests method CheckConsistency
        /// </summary>
        /// <param name="valueOld">Value, that should be replaced by new value</param>
        /// <param name="valueNew">Value, that will replace old value</param>
        internal void BreakConsistency(T valueOld, T valueNew)
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

        #region Delete

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
                if (rightBranch == null && leftBranch == null)
                {
                    DiscardNodeFromChain(nodeForDelete, null, null);
                    return true;
                }
                if (rightBranch == null)
                {
                    DiscardNodeFromChain(nodeForDelete, leftBranch, null);
                    return true;
                }
                if (leftBranch == null)
                {
                    DiscardNodeFromChain(nodeForDelete, rightBranch, null);
                    return true;
                }

                DiscardNodeFromChain(nodeForDelete, rightBranch, leftBranch);
                return true;
            }
        }

        /// <summary>
        /// Note! This method must be private, otherwise someone use it and break consistensy of this tree
        /// </summary>
        /// <param name="oldNode">Node for delete</param>
        /// <param name="newNode">Node, that will replace deleted node and his childs</param>
        /// <param name="tail">Branhc with other nodes of OldNode. Will be added to the NewNode</param>
        private void DiscardNodeFromChain(BinaryTreeNode<T> oldNode, BinaryTreeNode<T> newNode, BinaryTreeNode<T> tail)
        {
            // If head node of discarded branch have only discarded branch as child
            //  we throw away discaded node, and link head node with residue of branch
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

            // If head node of discarded branch have two childes, we need
            //  find, in which branch place discaded node. And after that append branches.
            if (_comparer.Compare(headNode.LeftNode.Value, oldNode.Value) == 0)
            {
                headNode.SetLeftNode(newNode);
            }
            if (_comparer.Compare(headNode.RightNode.Value, oldNode.Value) == 0)
            {
                headNode.SetRightNode(newNode);
            }

            if(tail == null)
                return;

            var minLeaf = GetMin(newNode);
            minLeaf.SetLeftNode(tail);
        }

        #endregion

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

        /// <summary>
        /// Execute received on each node
        /// </summary>
        /// <param name="func"></param>
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
