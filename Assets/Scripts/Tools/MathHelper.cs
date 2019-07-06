using UnityEngine;

public static class MathHelper
{
	/// <summary>
	/// 计算线和平面的交点
	/// </summary>
	/// <param name="panelPos">平面上任意一点</param>
	/// <param name="panelNormal">平面的法线</param>
	/// <param name="linePos">线上任意一点</param>
	/// <param name="lineDir">线的方向</param>
	/// <param name="crossPos">返回的交点</param>
	/// <returns>是否有交点</returns>
	public static bool GetLineAndPanelCrossPos(Vector3 panelPos, Vector3 panelNormal, Vector3 linePos, Vector3 lineDir, out Vector3 crossPos)
	{
		float denominator = Vector3.Dot((panelPos - linePos), panelNormal);
		float molecule = Vector3.Dot(lineDir, panelNormal);
		if (molecule != 0)
		{
			float distance = denominator / molecule;

			crossPos = distance * lineDir + linePos;
			return true;
		}
		crossPos = Vector3.zero;
		return false;
	}

	public static Vector3 GetReflectDir(Vector3 incidence, Vector3 normal)
	{
		normal.Normalize();
		incidence.Normalize();
		return (incidence - 2 * Vector3.Dot(incidence, normal) * normal).normalized;
	}

}

