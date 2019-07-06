using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader_Resources : AAssetsLoader
{
	public override void AsyncLoad_Object(string path, System.Action<Object> callBack)
	{
		Object obj = Resources.Load(path);
		callBack(obj);
	}

	public override T SyncLoad_Object<T>(string path)
	{
		return Resources.Load<T>(path);
	}

}
