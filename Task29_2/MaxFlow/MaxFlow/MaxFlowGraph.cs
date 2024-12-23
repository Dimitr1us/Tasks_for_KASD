using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFlowGraphLib
{
    internal class MaxFlowGraph
    {
        private int[,] capacity;
        private int numberOfVertices;

        public MaxFlowGraph(int vertices)
        {
            numberOfVertices = vertices;
            capacity = new int[vertices, vertices];
        }

        public void AddEdge(int from, int to, int cap)
        {
            capacity[from, to] += cap;
        }

        public int EdmondsKarp(int source, int sink)
        {
            int maxFlow = 0;
            int[] parent = new int[numberOfVertices];

            while (BFS(source, sink, parent))
            {
                int pathFlow = int.MaxValue;

                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, capacity[u, v]);
                }

                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    capacity[u, v] -= pathFlow;
                    capacity[v, u] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        private bool BFS(int source, int sink, int[] parent)
        {
            bool[] visited = new bool[numberOfVertices];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < numberOfVertices; v++)
                {
                    if (!visited[v] && capacity[u, v] > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parent[v] = u;

                        if (v == sink)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
