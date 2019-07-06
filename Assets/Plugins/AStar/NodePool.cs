using System.Collections.Generic;

namespace AStar
{
    internal class NodePool
    {
        private LinkedList<Node> pools = null;

        public NodePool()
        {
            pools = new LinkedList<Node>();
        }

        public Node Pop()
        {
            if (pools.Count <= 0)
            {
                return new Node();
            }
            else
            {
                Node node = pools.First.Value;
                pools.RemoveFirst();
                return node;
            }
        }

        public void Push(Node node)
        {
            if (node == null)
                return;

            node.Reset();
            pools.AddFirst(node);
        }
    }
}