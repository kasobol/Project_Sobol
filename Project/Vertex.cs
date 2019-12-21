using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Vertex
    {
        public bool Visited_Heap { get; set; }

        public bool Visited { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public double Distance { get; set; }

        public Edge Previous { get; set; }

        public List<Edge> OutEdge = new List<Edge>();

        public Vertex(string Name, int Number)
        {
            this.Name = Name;
            this.Number = Number;
            Visited = false;
            Visited_Heap = false;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
