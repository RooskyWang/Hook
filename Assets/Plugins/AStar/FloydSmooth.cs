using Mehroz;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class FloydSmooth
{
	/// <summary>
	/// 弗洛伊德平滑算法
	/// </summary>
	public static List<Point2> SmoothnessPath(List<Point2> nodeList, AStar.AStar.CheckIsBlock m_CheckBlockHandle)
	{
		if (nodeList == null || nodeList.Count == 0)
			return nodeList;

		Point2 turnNode = nodeList[0];

		int index = 0;
		while (index < nodeList.Count - 1)
		{
			turnNode = nodeList[index];
			for (int i = Mathf.Min(index + 20, nodeList.Count - 1); i > index; i--)
			{
				//if (i >= nodeList.Count)
				//    i = nodeList.Count - 1;

				if (CheckCanMove(turnNode, nodeList[i], m_CheckBlockHandle))
				{
					//连通，开始删除点
					nodeList.RemoveRange(index + 1, i - index - 1);
					index++;
					break;
				}

				if (i == index + 1)
				{
					index++;
					break;
				}
			}
		}

		return nodeList;
	}

	/// <summary>
	/// 检测两点之间是否可直线移动
	/// </summary>
	private static bool CheckCanMove(Point2 startPoint, Point2 endPoint, AStar.AStar.CheckIsBlock m_CheckBlockHandle)
	{
		int startX = (int)Math.Floor(startPoint.x);
		int startY = (int)Math.Floor(startPoint.y);
		int endX = (int)Math.Floor(endPoint.x);
		int endY = (int)Math.Floor(endPoint.y);

		int minX = Mathf.Min(startX, endX);
		int maxX = Mathf.Max(startX, endX);

		int minY = Mathf.Min(startY, endY);
		int maxY = Mathf.Max(startY, endY);

		if (startX == endX)
		{
			//横向相同
			for (int y = minY; y <= maxY; y++)
			{
				if (m_CheckBlockHandle(startX, y))
					return false;
			}
			return true;
		}
		else if (startY == endY)
		{
			//纵向相同
			for (int x = minX; x <= maxX; x++)
			{
				if (m_CheckBlockHandle(x, startY))
					return false;
			}
			return true;
		}
		else
		{
			//斜向
			//直线公式y=kx+b
			long dx = (long)(startPoint.x * 10 - endPoint.x * 10);
			long dy = (long)(startPoint.y * 10 - endPoint.y * 10);
			Fraction k = new Fraction(dy, dx);
			Fraction b = new Fraction(new Fraction((long)(startPoint.y * 10000), 10000) - k * new Fraction((long)(startPoint.x * 10000), 10000));

			//遍历横向
			for (int x = minX; x <= maxX; x++)
			{
				float yyy = (k * new Fraction(x) + b).ToFloat();
				int y = Mathf.FloorToInt(yyy);      //负数也要向上取整

				//x强转为int导致计算出来的（x,y）是在线段的延长线上
				if (y < minY || y > maxY)
					continue;

				//检测该点
				if (m_CheckBlockHandle(x, y))
					return false;

				//检测该点左边的点
				//因为是横向遍历，直线与y轴交叉，所以还要检测左边（x从小到大，故而只检测左边）的格子。此情况是为了防止直线因为x值的增加直接跳到斜格子
				if (x - 1 >= minX)
				{
					if (m_CheckBlockHandle(x - 1, y))
						return false;
				}

				//如果刚好是格子的顶点，还要检测格子下方的点
				if (y * 1000 == (int)(yyy * 1000) && y - 1 >= minY)
				{
					if (m_CheckBlockHandle(x - 1, y - 1))
						return false;
				}
			}

			//遍历纵向
			k = new Fraction(dx, dy);
			for (int y = minY; y <= maxY; y++)
			{
				float xxx = ((new Fraction(y) - b) * k).ToFloat();
				int x = Mathf.FloorToInt(xxx);

				//y强转为int导致计算出来的（x,y）是在线段的延长线上
				if (x < minX || x > maxX)
					continue;

				//检测该点
				if (m_CheckBlockHandle(x, y))
					return false;

				//检测该点下边的点
				//因为是纵向遍历，直线与x轴交叉，所以还要检测下边（y从小到大，故而只检测下边）的格子。此情况是为了防止直线因为y值的增加直接跳到斜格子
				if (y - 1 >= minY)
				{
					if (m_CheckBlockHandle(x, y - 1))
						return false;
				}

				//如果刚好是格子的顶点，还要检测格子下方的点
				if (x * 1000 == (int)(xxx * 1000) && x - 1 >= minX)
				{
					if (m_CheckBlockHandle(x - 1, y - 1))
						return false;
				}
			}

			return true;
		}
	}

}
