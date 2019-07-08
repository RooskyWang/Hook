using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoaderMgr : Sington<DataLoaderMgr>
{
	public BuildInfoLoader buildInfo;
	public ResourcesInfoLoader resInfo;

	public void LoadAllData()
	{
		buildInfo = new BuildInfoLoader();
		resInfo = new ResourcesInfoLoader();
	}
}
