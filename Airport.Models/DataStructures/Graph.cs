using System.Collections;
using System.Collections.Generic;

namespace Airport.Models.DataStructures
{
    public class Graph<T>
    {
        public List<Node<T>> First;
        public List<Node<T>> Last;

        public void AddNode(params T[] list)
        {
            var newNode = new List<Node<T>>();
            foreach (var item in list) newNode.Add(new Node<T>(item));

            if (First == default) First = newNode;
            if (Last == default) Last = newNode;
            else
            {
                foreach (var node in Last) node.Next = newNode;
                Last = newNode;
            }
        }
    }

    public class Node<T>
    {
        public List<Node<T>> Next { get; set; }
        public T Current { get; private set; }
        public Node(T current) => Current = current;
    }

}
