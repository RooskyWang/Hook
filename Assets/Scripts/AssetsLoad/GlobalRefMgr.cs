using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefMgr : Sington<GlobalRefMgr>
{
	public AAssetsLoader AssetsLoader;

	public void Init()
	{
		AssetsLoader = new AssetsLoader_Resources();
	}

}