using Mehroz;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStar
    {
        public delegate bool CheckIsBlock(float x, float y);

        private bool isUseCube = false;
        private const float RED_CUBE_SCALE = 0.3f;          //红色方块的大小
        private const float BLACK_CUBE_SCALE = 0.5f;        //黑色方块的大小
        private GameObject cubeObjRoot;                     //方块的挂载点

        private PriorityQueue<Node> m_OpenQueue = null;
        private Dictionary<int, Node> m_CloseDic = null;
        private NodePool m_NodePool = null;
        private CheckIsBlock m_CheckBlockHandle = null;

        private int m_StraightMoveValue = 10;
        private int m_SlantingMoveValue = 14;

        private static int[,] DirectionValue = new int[,]
        {
            {-1,-1},        //左上
            {-1,1},         //左下
            {1,-1},         //右上
            {1,1},          //右下
            {0,-1},         //上
            {0,1},          //下
            {-1,0},         //左
            {1,0},          //右
        };

        public AStar(CheckIsBlock checkBlockHandle)
        {
            this.m_CheckBlockHandle = checkBlockHandle;
            m_NodePool = new NodePool();
            m_OpenQueue = new PriorityQueue<Node>(50, new NodeComparer());
            m_CloseDic = new Dictionary<int, Node>();
        }

        /// <summary>
        /// 寻路函数
        /// </summary>
        /// <param name="startPos">起始点</param>
        /// <param name="targetPos">目标点</param>
        /// <param name="smoothPath">是否需要平滑移动</param>
        /// <returns>所经过的路径点</returns>
        public List<Point2> FindPath(Vector3 startPos, Vector3 targetPos, bool smoothPath)
        {
            Node node = null;
            bool isFindPath = FindPath((int)startPos.x, (int)startPos.z, (int)targetPos.x, (int)targetPos.z, ref node);

            if (isFindPath)
            {
                List<Point2> pathNodeList = GetAllNodeByFirst(node);
				if (smoothPath)
				{
					pathNodeList = FloydSmooth.SmoothnessPath(pathNodeList, m_CheckBlockHandle);
				}

                //第一个是起点
                pathNodeList.RemoveAt(0);

				lastPath = pathNodeList;

				return pathNodeList;
            }
            return null;
        }

        /// <summary>
        /// 寻路算法
        /// </summary>
        private bool FindPath(int startX, int startY, int endX, int endY, ref Node resultNode)
        {
            #region 条件检测

            if (m_CheckBlockHandle == null)
            {
                Debug.LogError("Erro: AStar check block handle is null!");
                return false;
            }

            //1：检测起始点和终点是否是障碍点，如果是则寻路失败
            if (m_CheckBlockHandle(startX, startY) || m_CheckBlockHandle(endX, endY))
            {
                return false;
            }

            //2：检测终点是否是起始点，如果是，则不需要寻路
            if (startX == endX && startY == endY)
            {
                return false;
            }

            #endregion

            #region 数据准备

            foreach (var item in m_OpenQueue)
            {
                m_NodePool.Push(item);
            }
            m_OpenQueue.Clear();

            foreach (var item in m_CloseDic.Values)
            {
                m_NodePool.Push(item);
            }
            m_CloseDic.Clear();

            #endregion

            //3：将起始点放入Open集合，开始寻路
            Node startNode = m_NodePool.Pop();
            startNode.x = startX;
            startNode.y = startY;
            startNode.g = 0;
            startNode.h = (Math.Abs(endX - startX) + Math.Abs(endY - startY)) * m_StraightMoveValue;
            startNode.f = startNode.g + startNode.h;
            m_OpenQueue.Push(startNode);

            //4：开始寻路
            while (m_OpenQueue.Count > 0)
            {
                //从Open表中取出，放入Close表中
                Node tempNode = m_OpenQueue.Pop();
                AddToCloseDic(tempNode);

                //判断是否到达
                if (tempNode.x == endX && tempNode.y == endY)
                {
                    resultNode = tempNode;
					CreateRedCube(resultNode);
					return true;
                }

                //遍历周围的点
                for (int i = 0; i < DirectionValue.GetLength(0); i++)
                {
                    int aroundX = tempNode.x + DirectionValue[i, 0];
                    int aroundY = tempNode.y + DirectionValue[i, 1];

                    //检测此点是否是障碍点
                    if (m_CheckBlockHandle(aroundX, aroundY))
                        continue;

                    //检测此点是否在Close表中
                    if (m_CloseDic.ContainsKey(Node.CalculateKey(aroundX, aroundY)))
                        continue;

                    //是否是斜向
                    bool isSlant = Math.Abs(DirectionValue[i, 0]) + Math.Abs(DirectionValue[i, 1]) > 1;

                    //检测斜向是否可通过（斜着经过的两个点都不为障碍点，则可斜向通行）
                    if (isSlant && m_CheckBlockHandle(aroundX, tempNode.y) || m_CheckBlockHandle(tempNode.x, aroundY))
                        continue;

                    //生成这个点
                    Node aroundNode = m_NodePool.Pop();
                    aroundNode.x = aroundX;
                    aroundNode.y = aroundY;
                    aroundNode.g = tempNode.g + (isSlant ? m_SlantingMoveValue : m_StraightMoveValue);
                    aroundNode.h = (Math.Abs(endX - aroundNode.x) + Math.Abs(endY - aroundNode.y)) * m_StraightMoveValue;
                    aroundNode.f = aroundNode.g + aroundNode.h;

                    //检测此点是否在Open表中
                    Node inOpenNode = null;
                    if ((inOpenNode = m_OpenQueue.Find(n => n != null && n.x == aroundNode.x && n.y == aroundNode.y)) != null)
                    {
                        if (inOpenNode.f > aroundNode.f)
                        {
                            inOpenNode.f = aroundNode.f;
                            inOpenNode.g = aroundNode.g;
                            inOpenNode.prev = tempNode;
                        }
                        m_NodePool.Push(aroundNode);
                        continue;
                    }

                    //把这个点当成一个新的点，加入到Open表中
                    aroundNode.prev = tempNode;
                    m_OpenQueue.Push(aroundNode);
                }
            }
            return false;
        }

        /// <summary>
        /// 将一个点添加到关闭列表
        /// </summary>
        private void AddToCloseDic(Node node)
        {
            if (!m_CloseDic.ContainsKey(node.Key))
            {
                m_CloseDic.Add(node.Key, node);
            }
        }

        /// <summary>
        /// 根据数据头，获取所有的节点数据
        /// </summary>
        private List<Point2> GetAllNodeByFirst(Node first)
        {
            if (first == null)
                return new List<Point2>(0);

            //找到节点的数量
            int count = 0;
            Node itNode = first;
            while (itNode != null)
            {
                count++;
                itNode = itNode.prev;
            }

            //生成结果集
            List<Point2> result = new List<Point2>(count);
            itNode = first;
            while (itNode != null)
            {
                result.Insert(0, new Point2(itNode.x + 0.5f, itNode.y + 0.5f));
                itNode = itNode.prev;
            }
            return result;
        }

        /// <summary>
        /// 创建黑色方块（曾经探索果的区域）
        /// </summary>
        private void CreateBlackCube()
        {
            if (!isUseCube)
                return;

            foreach (var item in m_CloseDic.Values)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = cubeObjRoot.transform;
                cube.transform.localPosition = new Vector3(item.x + 0.5f, 0, item.y + 0.5f);
                cube.transform.localScale = new Vector3(BLACK_CUBE_SCALE, BLACK_CUBE_SCALE, BLACK_CUBE_SCALE);
                cube.GetComponent<Renderer>().material.color = Color.black;
            }
        }

        /// <summary>
        /// 创建红色方块（行走路径）
        /// </summary>
        private void CreateRedCube(Node node)
        {
            if (!isUseCube)
                return;

			if (cubeObjRoot != null)
			{
				GameObject.DestroyImmediate(cubeObjRoot);
			}
			cubeObjRoot = new GameObject("cube root");

			while (node != null)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = cubeObjRoot.transform;
                cube.transform.localPosition = new Vector3(node.x + 0.5f, 0.5f, node.y + 0.5f);
                cube.transform.localScale = new Vector3(RED_CUBE_SCALE, RED_CUBE_SCALE, RED_CUBE_SCALE);
                cube.GetComponent<Renderer>().material.color = Color.red;
                cube.name = "Red_G-" + node.g + " H-" + node.h + " F-" + node.f;
                node = node.prev;
            }
        }

		private List<Point2> lastPath = null;

		public void DrawLine()
		{
			if (lastPath == null || lastPath.Count <= 1)
				return;

			for (int i = 1; i < lastPath.Count - 1; i++)
			{
				Debug.DrawLine(new Vector3(lastPath[i].x, 2, lastPath[i].y), new Vector3(lastPath[i + 1].x, 2, lastPath[i].y), Color.blue);
			}
		}

    }
}