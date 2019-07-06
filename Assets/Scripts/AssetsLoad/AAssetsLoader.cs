using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAssetsLoader
{
	public abstract T SyncLoad_Object<T>(string path) where T :Object;

	public abstract void AsyncLoad_Object(string path, System.Action<Object> callBack);
}
