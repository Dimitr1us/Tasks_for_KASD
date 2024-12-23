using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaryanLib
{
    internal class Taryan
    {
        private Dictionary<int, List<int>> adjacencyList;

        public Taryan()
        {
            adjacencyList = new Dictionary<int, List<int>>();
        }

        public void AddVertex(int vertex)
        {
            if (!adjacencyList.ContainsKey(vertex))
            {
                adjacencyList[vertex] = new List<int>();
            }
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);
            adjacencyList[vertex1].Add(vertex2);
        }

        public List<int> TopologicalSort()
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>();

            foreach (var vertex in adjacencyList.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    TopologicalSortUtil(vertex, visited, stack);
                }
            }

            var sortedList = new List<int>();
            while (stack.Count > 0)
            {
                sortedList.Add(stack.Pop());
            }

            return sortedList;
        }

        private void TopologicalSortUtil(int vertex, HashSet<int> visited, Stack<int> stack)
        {
            visited.Add(vertex);

            foreach (var adjacent in adjacencyList[vertex])
            {
                if (!visited.Contains(adjacent))
                {
                    TopologicalSortUtil(adjacent, visited, stack);
                }
            }

            stack.Push(vertex);
        }
    }
}
