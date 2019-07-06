using System.Collections.Generic;

namespace AStar
{
    public class Node
    {
        public int x;
        public int y;

        /// <summary>
        /// 从初始点到N点的实际消耗
        /// </summary>
        public int g;

        /// <summary>
        /// 从N点到终点的预估消耗
        /// </summary>
        public int h;

        /// <summary>
        /// 从初始点经过N点到终点的预估消耗 F = G + H
        /// </summary>
        public int f;

        public Node prev;

        private int key = -1;

        public void Reset()
        {
            x = 0;
            y = 0;
            g = 0;
            h = 0;
            f = 0;
            key = -1;
            prev = null;
        }

        public int Key
        {
            get { return key == -1 ? key = x + (y << 16) : key; }
        }

        public static int CalculateKey(int x, int y)
        {
            return x + (y << 16);
        }
    }

    internal class NodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x == null && y == null)
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            return x.f.CompareTo(y.f);
        }
    }

}
