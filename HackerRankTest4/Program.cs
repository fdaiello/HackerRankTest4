using System;
using System.Collections.Generic;

namespace HackerRankTest4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestComponentsInGraph();
        }

        static void TestCutTheTree()
        {
            List<int> nodes = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<List<int>> edges = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 1, 3 },
                new List<int> { 2, 6 },
                new List<int> { 3, 4 },
                new List<int> { 3, 5 },
            };

            Console.WriteLine(Result.cutTheTree(nodes, edges));
            Console.WriteLine("Expected: 3");
        }

        static void TestComponentsInGraph()
        {
            List<List<int>> array = new List<List<int>>
            { 
                new List<int> { 1, 5 },
                new List<int> { 1, 6 },
                new List<int> { 2, 4 }
            };

            Console.WriteLine(String.Join(",", Result.componentsInGraph(array)));
            Console.WriteLine("Expected: 2, 3");

            array = new List<List<int>> { 
                new List<int> { 1, 5 },
                new List<int> { 1, 6 },
                new List<int> { 5, 7 },
                new List<int> { 2, 4 }
            };

            Console.WriteLine(String.Join(",", Result.componentsInGraph(array)));
            Console.WriteLine("Expected: 2, 4");

            array = new List<List<int>> {
                new List<int> { 1, 6 },
                new List<int> { 2, 7 },
                new List<int> { 3, 8 },
                new List<int> { 4, 9 },
                new List<int> { 6, 2 },
                new List<int> { 9, 4 }
            };

            Console.WriteLine(String.Join(",", Result.componentsInGraph(array)));
            Console.WriteLine("Expected: 2, 4");


            array = new List<List<int>> {
                new List<int> { 1, 6 },
                new List<int> { 2, 7 },
                new List<int> { 3, 8 },
            };

            Console.WriteLine(String.Join(",", Result.componentsInGraph(array)));
            Console.WriteLine("Expected: 2, 2");

        }

        static void TestBFS()
        {
            int n = 9;
            int m = 4;
            int s = 1;

            List<List<int>> edges = new()
            {
                new() { 1, 2 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 1, 3 },
                new() { 3,6},
                new() { 3,7},
                new() { 7,8},
                new() { 7,9}
            };

            Console.WriteLine(String.Join(", ", Result.Bfs(n, m, edges, s)));
            Console.WriteLine("Expected:\n6, 6, 12, -1, 12, 12, 18, 18\n");


            edges = new()
            {
                new() { 1, 2 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 3, 6 },
                new() { 3, 7 },
                new() { 7, 8 },
                new() { 7, 9 }
            };

            Console.WriteLine(String.Join(", ", Result.Bfs(n, m, edges, s)));
            Console.WriteLine("Expected:\n6, 12, 12, -1, 18, 18, 24, 24\n");



            edges = new()
            {
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 3, 4 },
                new() { 4, 5 },
                new() { 5, 6 },
                new() { 6, 4 },
            };

            Console.WriteLine(String.Join(", ", Result.Bfs(6, 5, edges, 2)));
            Console.WriteLine("Expected:\n-1, 6, 6, 12, 12\n");
        }
    }
}
