using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Graph
    {
        public static int count_deep, count_width;
        public static int count_just, count_binary;
        public static int count_yen;

        public List<Vertex> Vertexes = new List<Vertex>();
        public List<Edge> Edges = new List<Edge>();

        public int Vertex_Count => Vertexes.Count;
        public int Edge_Count => Edges.Count;

        public void Add_Vertex(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }

        public void Add_Edge(Vertex From, Vertex To, double Weight = 0)
        {
            var edge = new Edge(From, To, Weight);
            if (Edges.Contains(edge) == false)
            {
                Edges.Add(edge);
            }
        }

        public void Add_OutEdge()
        {
            foreach(var edge in Edges)
            {
                edge.From.OutEdge.Add(edge);
            }
        }

        public double[,] Get_Graph_Matrix()
        {
            var matrix = new double[Vertex_Count, Vertex_Count];
            
            foreach(var edge in Edges)
            {
                matrix[edge.From.Number - 1, edge.To.Number - 1] = edge.Weight;
            }

            return matrix;
        }

        public void Reset()
        {
            foreach(var vertex in Vertexes)
            {
                vertex.Distance = int.MaxValue;
                vertex.Visited = false;
                vertex.Visited_Heap = false;
                vertex.Previous = null;
            }
        }

        public List<Edge> Copy(List<Edge> list)
        {
            var result = new List<Edge>();
            foreach(var item in list)
            {
                result.Add(item);
            }
            return result;
        }

        public List<Edge> Concat(List<Edge> list1, List<Edge> list2)
        {
            if(list2 == null)
            {
                return null;
            }
            var res = new List<Edge>();
            res = Copy(list1);
            foreach(var edge in list2)
            {
                res.Add(edge);
            }
            return res;
        }

        public bool Contains(List<List<Edge>> list1, List<Edge> list2)
        {
            if(list1.Count == 0)
            {
                return false;
            }
            bool check;

            foreach(var item in list1)
            {
                check = true;
                foreach(var item1 in item)
                {
                    if(check == false)
                    {
                        break;
                    }
                    foreach(var item2 in list2)
                    {
                        if(item2 == item1)
                        {
                            check = true;
                            break;
                        }
                        check = false;
                    }
                }
                if (check)
                {
                    return true;
                }
            }
            return false;
        }

        public double Sum(List<Edge> list)
        {
            double sum = 0;
            foreach (var edge in list)
            {
                sum += edge.Weight;
            }
            return sum;
        }

        public List<Edge> Reverse_List(List<Edge> list)
        {
            list.Reverse();
            return list;
        }

        /// <summary>
        /// Search way after algoritm's work
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Edge> Search_Way(Vertex start, Vertex end, List<Edge> list)
        {
            var way = new List<Edge>();
            var pointer = end;

            while (true)
            { 
                if(pointer == start)
                {
                    return Reverse_List(way);
                }
                foreach(var item in list)
                {
                    if(pointer == item.To)
                    {
                        pointer = item.From;
                        way.Add(item);
                    }
                }
                if(pointer == end)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Go Deep Algoritm - Hard: O(E + V)
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<Edge> Go_Deep(Vertex start)
        {
            Reset();

            start.Visited = true;

            bool check;
            var pointer = start;
            var recouvery = new List<Vertex>() { start };
            var result = new List<Edge>();

            while (true)
            {
                check = true;
                foreach (var outedge in pointer.OutEdge)
                {
                    count_deep++;
                    if (outedge.To.Visited == false)
                    {
                        check = false;

                        outedge.To.Visited = true;
                        recouvery.Add(outedge.To);
                        result.Add(outedge);

                        pointer = outedge.To;
                        break;
                    }
                }
                if (check)
                {
                    recouvery.Remove(recouvery.Last());
                    if (recouvery.Count == 0)
                    {
                        return result;
                    }
                    pointer = recouvery.Last();
                }
            }
        }

        /// <summary>
        /// Go Width Algoritm - Hard: O(E + V)
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<Edge> Go_Width(Vertex start)
        {
            Reset();

            start.Visited = true;

            var pointer = start;
            var recovery = new List<Vertex>() { start };
            var result = new List<Edge>();

            while (true)
            {
                foreach(var outedge in pointer.OutEdge)
                {
                    count_width++;
                    if(outedge.To.Visited == false)
                    {
                        outedge.To.Visited = true;
                        recovery.Add(outedge.To);
                        result.Add(outedge);
                    }
                }
                recovery.Remove(recovery.First());
                if(recovery.Count == 0)
                {
                    return result;
                }
                pointer = recovery.First();
            }
        }

        /// <summary>
        /// Go Dijkstra_Just Algoritm - Hard: O(V^2 + E)
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<Edge> Go_Dijkstra_Just(Vertex start)
        {
            Reset();

            start.Visited = true;
            start.Distance = 0;

            var pointer = start;
            var minimum = start.Previous;
            double min;
            var result = new List<Edge>();

            while (true)
            {
                min = int.MaxValue;
                foreach (var outedge in pointer.OutEdge)
                {
                    count_just++;
                    if (outedge.To.Visited == false)
                    {
                        if (pointer.Distance + outedge.Weight < outedge.To.Distance)
                        {
                            outedge.To.Distance = pointer.Distance + outedge.Weight;
                            outedge.To.Previous = outedge;
                        }
                    }
                }
                foreach (var vertex in Vertexes)
                {
                    count_just++;
                    if (vertex.Distance < min)
                    {
                        if (vertex.Visited == false)
                        {
                            min = vertex.Distance;
                            minimum = vertex.Previous;
                        }
                    }
                }
                if (minimum == null || pointer == minimum.To)
                {
                    break;
                }
                minimum.To.Visited = true;
                result.Add(minimum);
                pointer = minimum.To;
            }
            return result;
        }

        /// <summary>
        /// Go Dijcstra_BinaryHeap Algoritm - Hard: O((V + E)*log(V))
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public List<Edge> Go_Dijkstra_Binaryheap(Vertex start)
        {
            Reset();

            start.Visited = true;
            start.Distance = 0;

            var heap = new BinaryHeap();
            var pointer = start;
            var minimum = start.Previous;
            var result = new List<Edge>();

            for (int i = 0; true; i++)
            {
                foreach (var outedge in pointer.OutEdge)
                {
                    count_binary++;
                    count_yen++;
                    if (outedge.To.Visited == false)
                    {
                        if (pointer.Distance + outedge.Weight < outedge.To.Distance)
                        {
                            outedge.To.Distance = pointer.Distance + outedge.Weight;
                            outedge.To.Previous = outedge;
                            heap.Add_Heap(outedge.To);
                        }
                    }
                }

                if (heap.Vertexes_Sort.Count == 0)
                {
                    break;
                }

                minimum = heap.Get_Min().Previous;

                minimum.To.Visited = true;
                result.Add(minimum);
                heap.Delete_Min();
                pointer = minimum.To;
            }
            return result;
        }
        /// <summary>
        /// Go Yen Algoritm - Hard: O(k*V(E + V*log(V)))
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<List<Edge>> Go_Yen(Vertex start, Vertex end)
        {
            Reset();

            var help = new List<Edge>();
            var result = new List<List<Edge>>();
            var pointer_vertex = start;
            var visited = new List<List<Edge>>();
            var additive = new List<Edge>();
            var minimum = new List<Edge>();

            var way = Search_Way(start, end, Go_Dijkstra_Just(start));
            result.Add(way);
            var pointer_list = result.Last();

            while (true)
            {
                pointer_list = result.Last();
                pointer_vertex = start;
                additive = new List<Edge>();
                foreach (var edge in pointer_list)
                {
                    foreach (var list in result)
                    {
                        foreach (var edge_list in list)
                        {
                            count_yen++;
                            if (edge.From == edge_list.From)
                            {
                                edge.From.OutEdge.Remove(edge_list);
                                help.Add(edge_list);
                            }
                        }
                    }
                    var k = Go_Dijkstra_Binaryheap(pointer_vertex);

                    way = Search_Way(pointer_vertex, end, k);
                    way = Concat(additive, way);

                    if (way != null && Contains(visited, way) == false)
                    {
                        visited.Add(way);   
                    }
                    additive.Add(edge);

                    pointer_vertex = edge.To;

                    edge.From.OutEdge.AddRange(help);
                    help = new List<Edge>();
                }
                minimum = visited.First();
                foreach (var road in visited)
                {
                    count_yen++;
                    if (Sum(road) < Sum(minimum))
                    {
                        minimum = road;
                    }
                }
                visited.Remove(minimum);
                result.Add(minimum);
                
                if (visited.Count == 0)
                {
                    return result;
                }
            }

        }

        //public List<List<Edge>> Go_Yen(Vertex start, Vertex end)
        //{
        //    bool check = true;

        //    List<Edge> helper = new List<Edge>();
        //    foreach(var edge in Edges)
        //    {
        //        helper.Add(edge);
        //    }
        //    var minimum = new List<Edge>();
        //    var result = new List<List<Edge>>();
        //    var visited = new List<List<Edge>>();
        //    var visited_helper = new List<List<Edge>>();
        //    var pointer = new List<Edge>();
        //    var adder = new List<Edge>();

        //    var way = new List<Edge>();

        //    pointer = Search_Way(start, end, Go_Dijkstra_Just(start));
        //    result.Add(pointer);
        //    var visited_pointer = pointer.First();

        //    while (true)
        //    {
        //        foreach(var edge_pointer in pointer)
        //        {
        //            foreach(var list_edge in result)
        //            {
        //                foreach(var edge in list_edge)
        //                {
        //                    if(edge_pointer.From == edge.From)
        //                    {
        //                        if (edge_pointer != edge)
        //                        {
        //                            if (adder.Contains(edge) == false && adder.Contains(edge_pointer) == false)
        //                            {
        //                                adder.Add(edge_pointer);
        //                                adder.Add(edge);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if(adder.Contains(edge_pointer) == false)
        //                            {
        //                                adder.Add(edge_pointer);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            foreach(var edge in helper)
        //            {
        //                check = false;
        //                foreach(var e in adder)
        //                {
        //                    if(e.From == edge.From && e.To == edge.To)
        //                    {
        //                        check = true;
        //                    }
        //                }
        //                if (check)
        //                {
        //                    Edges.Remove(edge);
        //                }
        //            }

        //            way = Search_Way(start, end, Go_Dijkstra_Just(start));
        //            if(way != null)
        //            {
        //                if(visited_helper.Contains(way) == false)
        //                {
        //                    visited_helper.Add(way);
        //                    visited.Add(way);
        //                }
        //            }
        //            else
        //            {
        //                break;
        //            }
        //            foreach(var edge in helper)
        //            {
        //                if(edge_pointer.From == edge.From)
        //                {
        //                    Edges.Remove(edge);
        //                }
        //            }
        //            adder = new List<Edge>();
        //            Edges.Add(edge_pointer);
        //        }
        //        minimum = visited.First();
        //        foreach(var list in visited)
        //        {
        //            if(Sum(minimum) > Sum(list))
        //            {
        //                minimum = list;
        //            }
        //        }
        //        pointer = minimum;
        //        visited.Remove(minimum);
        //        result.Add(minimum);
        //        Edges = new List<Edge>();
        //        foreach(var edge in helper)
        //        {
        //            Edges.Add(edge);
        //        }
        //        if(visited.Count == 1)
        //        {
        //            result.Add(visited.First());
        //            break;
        //        }
        //    }
        //    return result;
        //}
    }
}
