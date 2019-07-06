using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour
{

    public static CameraFollowTarget instance = null;

    private Vector3 direction;

    #region 目标信息
    /// <summary>
    /// 追踪的目标
    /// </summary>
    private GameObject targetObj;

    /// <summary>
    /// 与目标保持的距离
    /// </summary>
    public float distance = 10;

    /// <summary>
    /// 目标位置
    /// </summary>
    private Vector3 targetPos;

    #endregion

    public float rotateSpeed = 80;

    public float moveSpeed = 3;

    public void Init(GameObject targetObj)
    {
		this.targetObj = targetObj;
		direction = -transform.forward;
        targetPos = targetObj.transform.position + direction * distance;
        instance = this;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(targetObj.transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
            direction = -transform.forward;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(targetObj.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
            direction = -transform.forward;
        }
        targetPos = targetObj.transform.position + direction * distance;
    }
    private void LateUpdate()
    {
        if (Vector3.Distance(this.transform.position, targetPos) > 0.05f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}