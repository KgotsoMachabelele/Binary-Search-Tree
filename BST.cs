using System;
using System.Collections.Generic;

namespace Binary_Search_Tree
{
    class BST<T> where T : IComparable
    {
        //Properties
        public T Value { get; private set; }
        public BST<T> Parent { get; private set; }
        public BST<T> Left { get; private set; }
        public BST<T> Right { get; private set; }

        #region Constructors

        //Constructor 1
        public BST(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        } //Constructor 1

        //Constructor 2
        public BST(T value, BST<T> leftChild, BST<T> rightChild)
        {
            this.Value = value;
            this.Left = leftChild;
            this.Right = rightChild;
        } //Constructor 2

        #endregion Constructors

        #region Traversal

        public List<T> VisitInOrder()
        {
            List<T> lstNodes = new List<T>();
            VisitInOrder(lstNodes); //Start the recursion
            return lstNodes;
        } //VisitInOrder

        private void VisitInOrder(List<T> lstNodes) //Left, Root, Right
        {
            // 1. Visit the left child
            if (this.Left != null)
                this.Left.VisitInOrder();

            // 2. Visit the root of this sub-tree
            lstNodes.Add(this.Value);

            // 3. Visit the right child
            if (this.Right != null)
                this.Right.VisitInOrder();
        } //VisitInOrder

        #endregion Traversals

        #region Searching

        private BST<T> Find(T value)
        {
            BST<T> node = this;
            while (node != null)
            {
                int compareTo = value.CompareTo(node.Value);
                if (compareTo < 0)
                    node = node.Left;
                else
                    if (compareTo > 0)
                    node = node.Right;
                else //values are equal - node is found, break out of loop
                    break;
            } //while

            return node;
        } //Find

        public bool Contains(T value)
        {
            bool found = this.Find(value) != null;
            return found;
        } //Contains

        #endregion Searching

        #region Insert
        public void Add(T value)
        {
            //Add without ability to remove nodes
            //Add(value, this); //value, node

            //Add with ability to remove nodes
            Add(value, null, this); //value, parent, node

        } //Insert

        //Use this if we do not want to enable the removal of parent nodes - easier to follow the logic of insertion
        private BST<T> Add(T value, BST<T> child)
        {
            if (child == null) //Occurs when we reach the bottom of the tree
            {
                child = new BST<T>(value); //The pointer, which was previously null, now becomes something. Thus, parent.Left/Right becomes something.
                //child.Parent = parent; //Only needed for removal
            }
            else //Still not at the bottom - decide which way to go 
            {
                int compareTo = value.CompareTo(child.Value);
                if (compareTo < 0)
                    child.Left = Add(value, child.Left); //value, node
                else
                    if (compareTo > 0)
                    child.Right = Add(value, child.Right); //value, node
                else //values are equal - don't insert
                {
                    int dummy = 0; //Dummy to enable breakpoint
                }
            }
            return child;
        } //Add

        private BST<T> Add(T value, BST<T> parent, BST<T> child)
        {
            if (child == null) //Occurs when we reach the bottom of the tree
            {
                child = new BST<T>(value); //The pointer, which was previously null, now becomes something. Thus, parent.Left/Right becomes something.
                child.Parent = parent; //Only needed for removal
            }
            else //Still not at the bottom - decide which way to go 
            {
                int compareTo = value.CompareTo(child.Value);
                if (compareTo < 0)
                    child.Left = Add(value, child, child.Left); //value, parent, node
                else
                    if (compareTo > 0)
                    child.Right = Add(value, child, child.Right); //value, parent, node
                else //values are equal - don't insert
                {
                    int dummy = 0; //Dummy to enable breakpoint
                }
            }
            return child;
        } //Add

        #endregion Add

        //Not for tests or exam
        #region Removing an element

        public void Remove(T value)
        {
            BST<T> nodeToDelete = Find(value);
            if (nodeToDelete != null)
                Remove(nodeToDelete);
        } //Remove

        private void Remove(BST<T> node)
        {
            // Case 3: If the node has two children.
            // Note that if we get here at the end, the node will be with at most one child
            if (node.Left != null && node.Right != null)
            {
                BST<T> replacement = node.Left; //Or go left and then right, right, right, ... until we reach the bottom
                while (replacement.Right != null)
                    replacement = replacement.Right;
                node.Value = replacement.Value; //Dont' swap the nodes, just replace the value
                node = replacement;
            }

            // Case 1 and 2: If the node has at most one child
            BST<T> theChild = node.Left != null ? node.Left : node.Right;

            // If the element to be deleted has one child
            if (theChild != null)
            {
                theChild.Parent = node.Parent;

                // Handle the case when the node is the root
                if (node.Parent == null)
                    node = theChild;
                else
                {
                    // Replace the node with its child sub-tree
                    if (node.Parent.Left == node)
                        node.Parent.Left = theChild;
                    else
                        node.Parent.Right = theChild;
                }
            }
            else
            {
                // Handle the case when the node is the root
                if (node.Parent == null)
                    node = null;
                else
                {
                    // Remove the node - it is a leaf
                    if (node.Parent.Left == node)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right = null;
                }
            }
        } //Remove

        #endregion Remove

        //Not for tests or exam
        #region PrintVisual

        private int nSpaces;
        public void PrintVisual()
        {
            int depth = GetDepth(this) - 1;
            dynamic prev = null;
            nSpaces = 10 + (int)(Math.Pow(2, depth));
            Queue<BST<T>> Q = new Queue<BST<T>>();
            Q.Enqueue(this);
            while (Q.Count > 0)
            {
                BST<T> current = Q.Dequeue();

                //Start new line if necessary
                if (current.Value < prev)
                {
                    Console.WriteLine(); Console.WriteLine();
                    depth--;
                }
                if (current.nSpaces >= 0 && current.nSpaces < Console.BufferWidth)
                {
                    Console.SetCursorPosition(current.nSpaces, Console.CursorTop);
                    Console.Write(current.Value.ToString().PadLeft(2));
                }
                prev = current.Value;

                if (current.Left != null)
                {
                    current.Left.nSpaces = current.nSpaces - (int)Math.Pow(2, depth);
                    Q.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    current.Right.nSpaces = current.nSpaces + (int)Math.Pow(2, depth);
                    Q.Enqueue(current.Right);
                }
            } //while

            Console.WriteLine();
        } //PrintVisual

        private int GetDepth(BST<T> node)
        {
            int depthR = 0, depthL = 0;
            if (node.Right != null) depthR = GetDepth(node.Right);
            if (node.Left != null) depthL = GetDepth(node.Left);
            return Math.Max(depthR, depthL) + 1;
        } //GetDepth

        private void PrintVisual(BST<T> node)
        {
            if (node == null)
                return;
            Console.Write(node.Value);
            PrintVisual(node.Left);
            PrintVisual(node.Right);
        } //PrintVisual

        #endregion PrintVisual

    } //class BST
} //nmespace
