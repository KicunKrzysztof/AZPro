using System;
using System.Text;

namespace LogoIntersectionFinder.Helpers
{
    public class AVL<U> where U : IComparable<U>
    {
        public Node<U> root;
        public bool IsEmpty()
        {
            return root == null ? true : false;
        }
        public U GetMin()
        {
            if (root == null)
                return default(U);
            Node<U> n = root;
            while (n.left != null)
                n = n.left;
            return n.data;
        }
        public U FindNext(U data)
        {
            Node<U> founded = RecursiveFindNext(root, data);
            return founded != null ? founded.data : default(U);
        }
        private Node<U> RecursiveFindNext(Node<U> current, U data)
        {
            if (current == null)
                return null;
            if (data.CompareTo(current.data) < 0)//go down in left subtree
            {
                Node<U> foundInLeftSubtree = RecursiveFindNext(current.left, data);
                return foundInLeftSubtree == null ? current : foundInLeftSubtree;
            }
            else if (data.CompareTo(current.data) > 0)//go down in right subtree
            {
                return RecursiveFindNext(current.right, data);
            }
            else //found exact value
            {
                if (current.right != null)
                {
                    Node<U> tmp = current.right;
                    while (tmp.left != null)
                        tmp = tmp.left;
                    return tmp;
                }
                else
                    return null;
            }   
        }
        public U FindPrev(U data)
        {
            Node<U> founded = RecursiveFindPrev(root, data);
            return founded != null ? founded.data : default(U);
        }
        private Node<U> RecursiveFindPrev(Node<U> current, U data)
        {
            if (current == null)
                return null;
            if (data.CompareTo(current.data) < 0)//go down in left subtree
            {
                return RecursiveFindPrev(current.left, data);
            }
            else if (data.CompareTo(current.data) > 0)//go down in right subtree
            {
                Node<U> foundInRightSubtree = RecursiveFindPrev(current.right, data);
                return foundInRightSubtree == null ? current : foundInRightSubtree;
            }
            else //found exact value
            {
                if (current.left != null)
                {
                    Node<U> tmp = current.left;
                    while (tmp.right != null)
                        tmp = tmp.right;
                    return tmp;
                }
                else
                    return null;
            }

        }
        public U Find(U data)
        {
            Node<U> findedNode = RecursiveFind(root, data);
            return findedNode == null ? default(U) : findedNode.data;
        }
        private Node<U> RecursiveFind(Node<U> current, U data)
        {
            if (current == null)
                return null;
            else if (data.CompareTo(current.data) < 0)
                return RecursiveFind(current.left, data);
            else if (data.CompareTo(current.data) > 0)
                return RecursiveFind(current.right, data);
            else
                return current;
        }
        public void Add(U data)
        {
            Node<U> newNode = new Node<U>(data);
            bool uselessHeightChangeParam = false;
            RecursiveInsert(ref root, newNode, ref uselessHeightChangeParam);
        }
        private void RecursiveInsert(ref Node<U> current, Node<U> newNode, ref bool heightChange)
        {
            if (current == null)
            {
                current = newNode;
                heightChange = true;
                return;
            }
            else if(newNode.data.CompareTo(current.data) < 0)//left subtree
            {
                RecursiveInsert(ref current.left, newNode, ref heightChange);
                if (heightChange)
                {
                    switch (current.balanceFactor)
                    {
                        case 1:
                            if (current.left.balanceFactor == 1 || current.left.balanceFactor == 0)
                                RotateLL(ref current);
                            else
                                RotateLR(ref current);
                            heightChange = false;
                            break;
                        case 0:
                            current.balanceFactor = 1;
                            break;
                        case -1:
                            current.balanceFactor = 0;
                            heightChange = false;
                            break;
                    }
                }
                return;
            }
            else if(newNode.data.CompareTo(current.data) > 0)//right subtree
            {
                RecursiveInsert(ref current.right, newNode, ref heightChange);
                if (heightChange)
                {
                    switch (current.balanceFactor)
                    {
                        case 1:
                            current.balanceFactor = 0;
                            heightChange = false;
                            break;
                        case 0:
                            current.balanceFactor = -1;
                            break;
                        case -1:
                            if (current.right.balanceFactor == -1 || current.right.balanceFactor == 0)
                                RotateRR(ref current);
                            else
                                RotateRL(ref current);
                            heightChange = false;
                            break;
                    }
                }
                return;
            }
        }

        public void Delete(U data)
        {
            bool uselessHeightChangeParam = false;
            RecursiveDelete(ref root, data, ref uselessHeightChangeParam);
        }
        private void RecursiveDelete(ref Node<U> current, U deleteData, ref bool heightChange)
        {
            bool done = false;
            if (current == null)
            {
                heightChange = false;
                done = true;
            }
            else if (deleteData.CompareTo(current.data) < 0)//delete from left subtree
            {
                RecursiveDelete(ref current.left, deleteData, ref heightChange);
                if (heightChange)
                {
                    if (current.balanceFactor >= 0)
                    {
                        current.balanceFactor--;
                        if (current.balanceFactor == -1)
                            heightChange = false;
                    }
                    else
                    {
                        if (current.right.balanceFactor == -1)
                            RotateRR(ref current);
                        else
                            RotateRL(ref current);
                    }
                    done = true;
                }
            }
            else if (deleteData.CompareTo(current.data) == 0)//delete from current node
            {
                if (current.left == null || current.right == null)
                {
                    //Node<U> tmp = current.left == null ? current.right : current.left;
                    current = current.left == null ? current.right : current.left;
                    heightChange = true;
                    done = true;
                }
                else
                {
                    Node<U> tmp = current.right;
                    while (tmp.left != null)
                        tmp = tmp.left;
                    current.data = tmp.data;
                    deleteData = current.data;
                }
            }
            if (!done && deleteData.CompareTo(current.data) >= 0) //delete from right subtree
            {
                RecursiveDelete(ref current.right, deleteData, ref heightChange);
                if (heightChange)
                {
                    if (current.balanceFactor <= 0)
                    {
                        current.balanceFactor++;
                        if (current.balanceFactor == 1)
                            heightChange = false;
                    }
                    else
                    {
                        if (current.left.balanceFactor == 1)
                            RotateLL(ref current);
                        else
                            RotateLR(ref current);
                    }
                }
            }
        }

        private void RotateRR(ref Node<U> current)
        {
            Node<U> tmp = current.right;
            current.right = tmp.left;
            tmp.left = current;
            current.balanceFactor = tmp.balanceFactor == -1 ? 0 : -1;
            tmp.balanceFactor = tmp.balanceFactor == 0 ? 1 : 0;
            current = tmp;
        }
        private void RotateLL(ref Node<U> current)
        {
            Node<U> tmp = current.left;
            current.left = tmp.right;
            tmp.right = current;
            current.balanceFactor = tmp.balanceFactor == 1 ? 0 : 1;
            tmp.balanceFactor = tmp.balanceFactor == 0 ? -1 : 0;
            current = tmp;
        }
        private void RotateLR(ref Node<U> current)
        {
            Node<U> tmp1 = current.left;
            Node<U> tmp2 = tmp1.right;
            tmp1.right = tmp2.left;
            tmp2.left = tmp1;
            current.left = tmp2.right;
            tmp2.right = current;
            current.balanceFactor = tmp2.balanceFactor == 1 ? -1 : 0;
            tmp1.balanceFactor = tmp2.balanceFactor == -1 ? 1 : 0;
            tmp2.balanceFactor = 0;
            current = tmp2;
        }
        private void RotateRL(ref Node<U> current)
        {
            Node<U> tmp1 = current.right;
            Node<U> tmp2 = tmp1.left;
            tmp1.left = tmp2.right;
            tmp2.right = tmp1;
            current.right = tmp2.left;
            tmp2.left = current;
            current.balanceFactor = tmp2.balanceFactor == -1 ? 1 : 0;
            tmp1.balanceFactor = tmp2.balanceFactor == 1 ? -1 : 0;
            tmp2.balanceFactor = 0;
            current = tmp2;
        }

        public static StringBuilder Print(Node<U> current, ref int numb, bool left)
        {
            StringBuilder sb = new StringBuilder();
            if (current != null)
            {
                sb.Append(numb.ToString());
                if (left)
                {
                    sb.Append(" L----");
                }
                else
                {
                    sb.Append(" R----");
                }
                sb.Append(current.data.ToString());
                sb.Append(" |bf: ");
                sb.Append(current.balanceFactor.ToString());
                sb.Append("\n");
                int ldeep = numb + 1;
                int rdeep = numb + 1;
                sb.Append(Print(current.left, ref ldeep, true).ToString());
                sb.Append(Print(current.right, ref rdeep, false).ToString());
            }
            return sb;
        }

        public class Node<T> where T: IComparable<T>
        {
            public T data;
            public Node<T> left;
            public Node<T> right;
            public int balanceFactor;
            public Node(T d)
            {
                data = d;
                balanceFactor = 0;
            }
        }

    }
}
