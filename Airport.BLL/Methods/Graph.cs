using System.Collections.Generic;

namespace Airport.BLL.Methods
{
    class Graph<T>
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
                foreach (var item in Last) item.Next = newNode;
                Last = newNode;
            }
        }
    }

    class Node<T>
    {
        public List<Node<T>> Next { get; set; }
        public T Current { get; private set; }
        public Node(T current) => Current = current;
    }
}
