using UnityEngine;
using System.Collections.Generic;

public class AStarMoveCtroller
{
    private Coroutine moveCor;

    private List<Vector3> findPath;
    private List<Vector3> movePath = new List<Vector3>();
	private System.Action endCallBack;
	private Transform cacheTran;

	public void Init(Transform tran)
	{
		cacheTran = tran;
	}

	public void Update()
    {
		//if (findPath != null)
		//{
		//	Vector3 stPos = Vector3.zero;
		//	Vector3 tarPos = Vector3.zero;

		//	for (int i = 0; i < findPath.Count; i++)
		//	{
		//		if (i + 1 < findPath.Count)
		//		{
		//			stPos = findPath[i];
		//			tarPos = findPath[i + 1];
		//		}
		//		Debug.DrawLine(stPos, tarPos, Color.blue);
		//	}
		//}

        //有路径
        if (movePath != null && movePath.Count > 0)
        {
            Vector3 targetPos = movePath[0];
            if (Vector3.Distance(cacheTran.position, targetPos) > 0.002f)
            {
				//证明未到达此点
				cacheTran.rotation = Quaternion.Lerp(cacheTran.rotation, Quaternion.LookRotation(targetPos - cacheTran.position), 4 * Time.deltaTime);

				cacheTran.position = Vector3.MoveTowards(cacheTran.position, targetPos, 5 * Time.deltaTime);
            }
            else
            {
                //证明到达了这个点
                movePath.RemoveAt(0);

				//判断结束
				if (movePath.Count == 0)
				{
					if (endCallBack != null)
					{
						endCallBack();
						endCallBack = null;
					}
				}
            }
        }
    }

	public void SetPath(List<Vector3> path, System.Action endCallBack)
	{
		findPath = path;
		movePath.Clear();
		this.endCallBack = endCallBack;

		if (findPath == null)
		{
			if (endCallBack != null)
			{
				endCallBack();
				endCallBack = null;
			}
			return;
		}

		foreach (var item in findPath)
		{
			movePath.Add(item);
		}
		findPath.Insert(0, cacheTran.position);
	}

}