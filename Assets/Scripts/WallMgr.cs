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

	public CrossWallInfo GetCrossWall(Vector3 startPos, Vector3 linePos, Vector3 lineDir)
	{
		Vector3 crossPos;
		foreach (var wall in allWall)
		{
			//求出线和面的交叉点
			if (MathHelper.GetLineAndPanelCrossPos(wall.transform.position, wall.transform.forward, linePos, lineDir, out crossPos))
			{
				//检测交叉点是否在平面内
				Vector3 crossPosLocalPosForWall = wall.transform.InverseTransformPoint(crossPos);
				if (Mathf.Abs(crossPosLocalPosForWall.x) <= 0.5f && Mathf.Abs(crossPosLocalPosForWall.y) <= 0.5f)
				{
					//将钩子头的点映射到面的坐标空间下
					Vector3 localPosForWall = wall.transform.InverseTransformPoint(linePos);
					float worldDisZ = localPosForWall.z * (wall.transform.lossyScale.z / wall.transform.localScale.z);

					//处理穿透墙面的问题
					//判断当前点到墙面的向量和线的前进方向是否一致，不一致（反向），则为穿透
					Vector3 startDir = crossPos - startPos;
					Vector3 curDir = crossPos - linePos;
					float angle = Vector3.Dot(startDir.normalized, curDir.normalized);
					bool diffDir = angle <= -0.98f;

					if (wall.transform.name.Equals("Cube"))
					{
						Debug.LogError(crossPos);
						Debug.LogError(angle);
					}

					if (Mathf.Abs(worldDisZ) < 0.05f || diffDir)
					{
						//距离特别近，或者穿透了
						CrossWallInfo cwi = new CrossWallInfo();
						cwi.crossObj = wall;
						cwi.crossPos = crossPos;
						return cwi;
					}
				}
			}
		}
		return null;
	}

}
