using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build
{
	public GameObject obj;
	public int buildType = 0;
	public Vector3 buildPos;
}

public class BuildingMgr : Sington<BuildingMgr>
{
	public bool isBuildingMode = false;
	public GameObject buildObj = null;
	public int buildType = 0;

	public Build baseBuild = null;
	private List<Build> allBuilds = new List<Build>();

	public void AddBaseBuild(GameObject baseBuild)
	{
		Build b = new Build();
		b.obj = baseBuild;
		b.buildType = 3;
		b.buildPos = baseBuild.transform.position;

		allBuilds.Add(b);

		this.baseBuild = b;
	}

	public void AddBuild()
	{
		Build b = new Build();
		b.obj = buildObj;
		b.buildType = buildType;
		b.buildPos = buildObj.transform.position;

		allBuilds.Add(b);

		isBuildingMode = false;
		EventMessage.Instance.DispatchEvent<EventCls_BuildClick>(new EventCls_BuildClick());
	}

	public bool CheckIsBuildContainsPos(int x, int y)
	{
		foreach (var build in allBuilds)
		{
			int bX = (int)build.buildPos.x;
			int bY = (int)build.buildPos.z;
			if (x <= bX && x >= bX - 1)
			{
				if (y <= bY && y >= bY - 1)
				{
					return true;
				}
			}
		}
		return false;
	}
}
