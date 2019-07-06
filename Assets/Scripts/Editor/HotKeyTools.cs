using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HotKeyTools : Editor
{
	[MenuItem("Tools/Set child active")]
	public static void SetChildActive()
	{
		GameObject obj = Selection.activeGameObject;
		if (obj == null)
			return;

		obj.SetActiveRecursively(true);
	}

}
