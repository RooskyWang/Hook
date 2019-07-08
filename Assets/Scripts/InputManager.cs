using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{

    public static InputManager instance = null;

    /// <summary>
    /// 当前选中格子的X
    /// </summary>
    [HideInInspector]
    public int curSelectPosX;
    /// <summary>
    /// 当前选中格子的Y
    /// </summary>
    [HideInInspector]
    public int curSelectPosY;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }


    private void OnGUI()
    {
        GUILayout.Label("当前选择的位置:" + curSelectPosX + ":" + curSelectPosY);
    }

	void Update()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (!Physics.Raycast(ray, out hit, 500))
		{
			return;
		}
		curSelectPosX = (int)hit.point.x;
		curSelectPosY = (int)hit.point.z;

		if (Input.GetMouseButtonDown(0))
		{
			if (BuildingMgr.Instance.isBuildingMode)
			{
				BuildingMgr.Instance.AddBuild();
			}
			else
			{
				//不在建筑模式下，控制玩家
				List<Vector3> path = UnityAStar.Instance.FindPath(PlayerMgr.Instance.MainPlayer.Position, new Vector3(hit.point.x, 0, hit.point.z), true);

				if (path != null)
				{
					PlayerMgr.Instance.MainPlayer.Move(path);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayerMgr.Instance.MainPlayer.FireHook();
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			EnemiesMgr.Instance.CreateFormation(new Vector3(14.5f, 0, 13.5f), 0);
		}
	}

}