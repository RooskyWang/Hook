using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInfoLoader : ThriftDataTypeMgr<ThriftStruct.BuildInfo>
{
	protected override string GetDataFilePath()
	{
		return "Bytes/BuildInfo";
	}

	public ThriftStruct.BuildInfo GetBuildInfoById(int id)
	{
		foreach (var item in dataList)
		{
			if (item.ID == id)
				return item;
		}
		return null;
	}
}
