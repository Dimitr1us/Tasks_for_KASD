using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BronKerboschLib
{
    internal class BronKerboschGraph
    {
        private HashSet<int> vertices;
        private Dictionary<int, HashSet<int>> adjacencyList;

        public BronKerboschGraph()
        {
            vertices = new HashSet<int>();
            adjacencyList = new Dictionary<int, HashSet<int>>();
        }

        public void AddVertex(int vertex)
        {
            if (!vertices.Contains(vertex))
            {
                vertices.Add(vertex);
                adjacencyList[vertex] = new HashSet<int>();
            }
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            if (vertices.Contains(vertex1) && vertices.Contains(vertex2))
            {
                adjacencyList[vertex1].Add(vertex2);
                adjacencyList[vertex2].Add(vertex1);
            }
        }


        public HashSet<int> FindMaxClique()
        {
            HashSet<int> maxClique = new HashSet<int>();
            HashSet<int> p = new HashSet<int>(vertices);
            HashSet<int> r = new HashSet<int>();
            HashSet<int> x = new HashSet<int>();

            FindMaxCliqueUtil(r, p, x, ref maxClique);
            return maxClique;
        }

        private void FindMaxCliqueUtil(HashSet<int> r, HashSet<int> p, HashSet<int> x, ref HashSet<int> maxClique)
        {
            if (p.Count == 0 && x.Count == 0)
            {
                if (r.Count > maxClique.Count)
                {
                    maxClique = new HashSet<int>(r);
                }
                return;
            }

            foreach (var vertex in new HashSet<int>(p))
            {
                HashSet<int> newR = new HashSet<int>(r) { vertex };
                HashSet<int> newP = new HashSet<int>(Intersect(p, adjacencyList[vertex]));
                HashSet<int> newX = new HashSet<int>(Intersect(x, adjacencyList[vertex]));

                FindMaxCliqueUtil(newR, newP, newX, ref maxClique);

                p.Remove(vertex);
                x.Add(vertex);
            }
        }

        private HashSet<int> Intersect(HashSet<int> set1, HashSet<int> set2)
        {
            HashSet<int> intersection = new HashSet<int>();
            foreach (var item in set1)
            {
                if (set2.Contains(item))
                {
                    intersection.Add(item);
                }
            }
            return intersection;
        }
    }
}
