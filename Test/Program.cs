using Airport.Models;
using Airport.Models.DataStructures;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph<Station>();
            graph.AddNode(new Station() { StationName = "One" });
            graph.AddNode(new Station() { StationName = "Two" });

            foreach (var item in graph)
            {
                Console.WriteLine(item);
            }
        }
    }
}
