using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWallInfo
{
	public GameObject crossObj;
	public Vector3 crossPos;
}

public class WallMgr : Sington<WallMgr>
{
	private GameObject rootObj;
	private List<GameObject> allWall;

	public void Init()
	{
		rootObj = GameObject.Find("Wall");
		allWall = new List<GameObject>();

		foreach (Transform item in rootObj.transform)
		{
			allWall.Add(item.gameObject);
		}
	}

	public CrossWallInfo GetCrossWall(Vector3 prePos, Vector3 startPos, Vector3 linePos, Vector3 lineDir)
	{
		Vector3 crossPos;
		foreach (var item in allWall)
		{
			if (MathHelper.GetLineAndPanelCrossPos(item.transform.position, item.transform.forward, linePos, lineDir, out crossPos))
			{
				// 求出线和面的交叉点
				if (Vector3.Distance(startPos, crossPos) > 0.05f)
				{
					// 排除起点就在墙内，移动时还在墙内的情况
					Bounds bd = item.GetComponent<BoxCollider>().bounds;
					if (bd.Contains(crossPos))
					{
						if (Vector3.Distance(crossPos, linePos) < 0.05f)
						{
							// 判断交叉点和链子的头部是否在墙面内
							CrossWallInfo cwi = new CrossWallInfo();
							cwi.crossObj = item;
							cwi.crossPos = crossPos;
							return cwi;
						}
					}
				}
			}
		}
		return null;
	}

}
