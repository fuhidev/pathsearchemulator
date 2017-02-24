using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public class Node
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Point ViTri { get; set; }
        public List<Node> hangXom { get; set; }

        public Node()
        {
            hangXom = new List<Node>();
        }
        public bool DaXet { get; set; }
        public int GiaTri { get; set; }

        public Node nodeCha { get; set; }

        public override bool Equals(object obj)
        {
            var n = obj as Node;
            return this.ViTri.Equals(n.ViTri);
        }

        public override string ToString()
        {
            return "Node: " + ViTri + String.Format("HangXom={0} DaXet={1}", hangXom.Count, DaXet);
        }

    }
}
