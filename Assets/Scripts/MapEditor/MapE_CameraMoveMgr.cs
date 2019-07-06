using UnityEngine;

public class MapE_CameraMoveMgr : MonoBehaviour
{
	private Transform cacheTran;

	public float moveSpeed = 10;
	public float zoomSpeed = 300;

    void Start()
    {
		cacheTran = transform;
	}

    void Update()
    {
		int forward = 0;
		int left = 0;
		float zoom = Input.GetAxis("Mouse ScrollWheel");

		#region 获取输入的方向
		if (Input.GetKey(KeyCode.W))
		{
			forward = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			forward = -1;
		}
		if (Input.GetKey(KeyCode.A))
		{
			left = 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			left = -1;
		}
		#endregion

		//计算摄像机的前后移动
		if (forward != 0)
		{
			Vector3 forwardDir = cacheTran.up;
			forwardDir.y = 0;
			cacheTran.position += forwardDir.normalized * forward * Time.deltaTime * moveSpeed;
		}

		//计算摄像机的左右移动
		if (left != 0)
		{
			Vector3 leftDir = -cacheTran.right;
			leftDir.y = 0;
			cacheTran.position += leftDir.normalized * left * Time.deltaTime * moveSpeed;
		}

		//计算摄像机的上下移动
		if (zoom != 0)
		{
			Vector3 downDir = cacheTran.forward;
			cacheTran.position += downDir.normalized * zoom * Time.deltaTime * zoomSpeed;
		}
	}
}
