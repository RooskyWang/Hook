using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThriftStruct;

public class ResourcesInfoLoader : ThriftDataTypeMgr<ResourcesInfo>
{
	protected override string GetDataFilePath()
	{
		return "Bytes/ResourcesInfo";
	}

	public string GetResNameById(int id)
	{
		foreach (var item in dataList)
		{
			if (item.ID == id)
				return item.ResPath;
		}
		return null;
	}

}
