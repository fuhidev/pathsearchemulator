using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm.Greedy
{
    public class Node: Algorithm.Node
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public int ChiPhi { get; set; }
    }
}
