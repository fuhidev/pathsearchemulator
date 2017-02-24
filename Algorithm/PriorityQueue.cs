using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public class PriorityQueue : IEnumerable
    {
        List<Node> items;
        List<int> priorities;

        public PriorityQueue()
        {
            items = new List<Node>();
            priorities = new List<int>();
        }

        public IEnumerator GetEnumerator() { return items.GetEnumerator(); }
        public int Count { get { return items.Count; } }

        public bool Any()
        {
            return items.Any();
        }

        /// <returns>Index of new element</returns>
        public int Enqueue(Node item, int priority)
        {
            for (int i = 0; i < priorities.Count; i++) //go through all elements...
            {
                if (priorities[i] > priority) //...as long as they have a lower priority.    low priority = low index
                {
                    items.Insert(i, item);
                    priorities.Insert(i, priority);
                    return i;
                }
            }

            items.Add(item);
            priorities.Add(priority);
            return items.Count - 1;
        }

        public Node Dequeue()
        {
            Node item = items[0];
            priorities.RemoveAt(0);
            items.RemoveAt(0);
            return item;
        }

        public int PeekPriority()
        {
            return priorities[0];
        }
    }
}
