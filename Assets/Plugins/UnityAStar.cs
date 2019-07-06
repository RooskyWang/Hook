using System.Collections.Generic;
using UnityEngine;

public class UnityAStar : Sington<UnityAStar>
{
	private AStar.AStar astar;

	public void InitAStar(AStar.AStar.CheckIsBlock cib)
	{
		astar = new AStar.AStar(cib);
	}

	public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos, bool bSmooth)
	{
		List<Point2> allNode = astar.FindPath(startPos, endPos, bSmooth);

		if (allNode != null)
		{
			List<Vector3> path = new List<Vector3>(allNode.Count);

			foreach (var item in allNode)
			{
				path.Add(new Vector3(item.x, 0, item.y));
			}

			return path;
		}
		return null;
	}
}
