using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefMgr : Sington<GlobalRefMgr>
{
	public AAssetsLoader AssetsLoader;
	public Camera mainCamera;

	public void Init(Camera mainCamera)
	{
		AssetsLoader = new AssetsLoader_Resources();
		this.mainCamera = mainCamera;
	}

}