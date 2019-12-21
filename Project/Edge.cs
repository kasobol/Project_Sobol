using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }

        public double Weight { get; set; }

        public Edge(Vertex From, Vertex To, double Weight)
        {
            this.From = From;
            this.To = To;
            this.Weight = Weight;

        }

        public override string ToString()
        {
            return $"{From.Name} -> {To.Name} : {Weight}";
        }
    }
}
