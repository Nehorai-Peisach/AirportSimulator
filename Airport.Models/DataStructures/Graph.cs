using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Airport.Models.DataStructures
{
    public class Graph<T>
    {
        public GraphType type { get; private set; }
        public Graph(GraphType type) => this.type = type;

        public List<Node<T>> First { get; private set; }
        public List<Node<T>> Last { get; private set; }

        public void AddNode(params T[] list)
        {
            var newNode = new List<Node<T>>();
            foreach (var item in list) newNode.Add(new Node<T>(item,type));

            if (First == default) First = newNode;
            if (Last == default) Last = newNode;
            else
            {
                newNode.ForEach(n => n.Previous = Last);
                Last.ForEach(n => n.Next = newNode);

                Last = newNode;
            }
        }
    }

    public class Node<T>
    {

        public T Value { get; private set; }
        public GraphType type { get; private set; }
        public Node(T value, GraphType type)
        {
            Value = value;
            this.type = type;
        }

        public List<Node<T>> Next { get; set; }
        public List<Node<T>> Previous { get; set; }
    }

}
