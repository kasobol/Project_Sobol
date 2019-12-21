using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class BinaryHeap
    {
        public List<Vertex> Vertexes_Sort = new List<Vertex>();

        public Vertex Get_Min()
        {
            return Vertexes_Sort.First();
        }

        public void Add_Heap(Vertex vertex_add)
        {
            int index_prev;
            int index_pointer;
            if(Vertexes_Sort.Count == 0)
            {
                Vertexes_Sort.Add(vertex_add);
                return;
            }

            if(vertex_add.Visited_Heap == false)
            {
                Vertexes_Sort.Add(vertex_add);
            }
            vertex_add.Visited_Heap = true;
            var pointer = vertex_add;

            while (true)
            {
                Graph.count_binary++;
                Graph.count_yen++;
                index_prev = (Vertexes_Sort.IndexOf(pointer) - 1) / 2;
                index_pointer = Vertexes_Sort.IndexOf(pointer);
                if(pointer.Distance < Vertexes_Sort[index_prev].Distance)
                {
                    Vertexes_Sort[index_pointer] = Vertexes_Sort[index_prev];
                    Vertexes_Sort[index_prev] = pointer;
                }
                else
                {
                    break;
                }
            }
        }

        public void Delete_Min()
        {
            int index_pointer;
            int index_Pos_Left;
            int index_Pos_Right;
            int index_minimum;
            if(Vertexes_Sort.Count == 1 || Vertexes_Sort.Count == 2)
            {
                Vertexes_Sort.Remove(Vertexes_Sort.First());
                return;
            }
            Vertexes_Sort[0] = Vertexes_Sort.Last();
            Vertexes_Sort.Remove(Vertexes_Sort.Last());

            var pointer = Vertexes_Sort[0];
            var minimum = pointer;
            while (true)
            {
                Graph.count_binary++;
                Graph.count_yen++;
                index_pointer = Vertexes_Sort.IndexOf(pointer);
                index_Pos_Left = (index_pointer + 1) * 2 - 1;
                index_Pos_Right = (index_pointer + 1) * 2;

                if (index_Pos_Left > Vertexes_Sort.Count - 1)
                {
                    break;
                }
                if(index_Pos_Right > Vertexes_Sort.Count - 1)
                {
                    if(pointer != Min_Of_Two(pointer, Vertexes_Sort[index_Pos_Left]))
                    {
                        Vertexes_Sort[index_pointer] = Vertexes_Sort[index_Pos_Left];
                        Vertexes_Sort[index_Pos_Left] = pointer;
                    }
                    break;
                }

                minimum = Min_Of_Three(pointer, Vertexes_Sort[index_Pos_Left], Vertexes_Sort[index_Pos_Right]);
                index_minimum = Vertexes_Sort.IndexOf(minimum);  
                if(pointer != minimum)
                {
                    Vertexes_Sort[index_pointer] = Vertexes_Sort[index_minimum];
                    Vertexes_Sort[index_minimum] = pointer;
                }
                else
                {
                    break;
                }
            }
        }

        public Vertex Min_Of_Three(Vertex v1, Vertex v2, Vertex v3)
        {
            return v1.Distance < v2.Distance ? (v1.Distance < v3.Distance ? v1 : v3) : (v2.Distance < v3.Distance ? v2 : v3);
        }

        public Vertex Min_Of_Two(Vertex v1, Vertex v2)
        {
            return v1.Distance < v2.Distance ? v1 : v2;
        }
    }
}
