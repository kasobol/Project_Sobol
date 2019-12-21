using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static void Main()
        {
            //Create Graph
            Graph graph = new Graph();

            Vertex v1 = new Vertex("v1", 1);
            Vertex v2 = new Vertex("v2", 2);
            Vertex v3 = new Vertex("v3", 3);
            Vertex v4 = new Vertex("v4", 4);
            Vertex v5 = new Vertex("v5", 5);
            Vertex v6 = new Vertex("v6", 6);
            Vertex v7 = new Vertex("v7", 7);

            graph.Add_Vertex(v1);
            graph.Add_Vertex(v2);
            graph.Add_Vertex(v3);
            graph.Add_Vertex(v4);
            graph.Add_Vertex(v5);
            graph.Add_Vertex(v6);
            graph.Add_Vertex(v7);

            int[] Range = { 1, 2, 3, 4, 5, 6 };

            graph.Add_Edge(v1, v5, 12);
            graph.Add_Edge(v1, v4, 7);
            graph.Add_Edge(v1, v3, 9);
            graph.Add_Edge(v1, v2, 1);
            graph.Add_Edge(v2, v6, 3);
            graph.Add_Edge(v2, v4, 2);
            graph.Add_Edge(v2, v3, 4);
            graph.Add_Edge(v3, v4, 4);
            graph.Add_Edge(v4, v7, 2);
            graph.Add_Edge(v4, v6, 6);
            graph.Add_Edge(v4, v5, 7);
            graph.Add_Edge(v4, v1, 7);
            graph.Add_Edge(v5, v7, 3);
            graph.Add_Edge(v6, v2, 3);
            graph.Add_Edge(v7, v4, 2);

            graph.Add_OutEdge();

            Display(graph);

            //Search_Deep
            Console.WriteLine();
            var GoDeep = graph.Go_Deep(v5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Go_Deep: ");
            Console.ResetColor();

            Show(GoDeep);
            Console.WriteLine("Hard: " + Graph.count_deep);

            Console.WriteLine();
            var GoDeep_Way = graph.Search_Way(v5, v3, GoDeep);
            Console.WriteLine("Go_Deep_Way: v5 - v3: ");
            Show(GoDeep_Way);
            Console.WriteLine($"Leanth_Way: {graph.Sum(GoDeep_Way)}");

            //Search_Width
            Console.WriteLine();
            var GoWidth = graph.Go_Width(v5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Go_Width: ");
            Console.ResetColor();

            Show(GoWidth);
            Console.WriteLine("Hard: " + Graph.count_width);

            Console.WriteLine();
            var GoWidth_Way = graph.Search_Way(v5, v3, GoWidth);
            Console.WriteLine("Go_Width_Way: v5 - v3: ");
            Show(GoWidth_Way);
            Console.WriteLine($"Leanth_Way: {graph.Sum(GoWidth_Way)}");

            //Search_Dijkstra_Just
            Console.WriteLine();
            var GoDijkstraJust = graph.Go_Dijkstra_Just(v5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Go_Dijkstra_Just: ");
            Console.ResetColor();

            Show(GoDijkstraJust);
            Console.WriteLine("Hard: " + Graph.count_just);

            Console.WriteLine();
            var GoDijkstraJust_Way = graph.Search_Way(v5, v3, GoDijkstraJust);
            Console.WriteLine("Go_Dijstra_Just_Way: v5 - v3: ");
            Show(GoDijkstraJust_Way);
            Console.WriteLine($"Leanth_Way: {graph.Sum(GoDijkstraJust_Way)}");

            //Search_Dijkstra_BinaryHeap
            Console.WriteLine();
            var GoDijkstraBinaryHeap = graph.Go_Dijkstra_Binaryheap(v5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Go_Dijkstra_Binaryheap: ");
            Console.ResetColor();

            Show(GoDijkstraBinaryHeap);
            Console.WriteLine("Hard: " + Graph.count_binary);

            Console.WriteLine();
            var GoDijkstraBinaryHeap_Way = graph.Search_Way(v5, v3, GoDijkstraBinaryHeap);
            Console.WriteLine("Go_Dijstra_Binaryheap_Way: v5 - v3: ");
            Show(GoDijkstraBinaryHeap_Way);
            Console.WriteLine($"Leanth_Way: {graph.Sum(GoDijkstraBinaryHeap_Way)}");

            //Go_Yen
            Console.WriteLine();
            var Go_Yen = graph.Go_Yen(v5, v3);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Go_Yen: v5 - v3: ");
            Console.ResetColor();

            foreach (var list in Go_Yen)
            {
                Console.WriteLine();
                Show(list);
                Console.WriteLine("Sum: " + graph.Sum(list));
            }
            Console.WriteLine("HARD YEN: " + Graph.count_yen);
        }

        static void Display(Graph graph)
        {
            double[,] mas = graph.Get_Graph_Matrix();
            Console.WriteLine();
            Console.Write("\t");

            for (int i = 0; i < graph.Vertex_Count; i++)
            {
                Console.Write($"\t{i + 1}");
            }
            Console.WriteLine();
            Console.Write("\t");
            for (int i = 0; i < graph.Vertex_Count; i++)
            {
                Console.Write($"\t#");
            }
            Console.WriteLine();
            for (int i = 0; i < graph.Vertex_Count; i++)
            {
                Console.Write($"{i + 1}\t#\t");
                for (int j = 0; j < graph.Vertex_Count; j++)
                {
                    Console.Write($"{mas[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
        
        static void Show(List<Edge> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine($"{item.ToString()}");
            }
        }
    }
}
