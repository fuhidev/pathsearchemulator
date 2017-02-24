using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm.AStar
{
    public class Node: Dijkstra.Node
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
