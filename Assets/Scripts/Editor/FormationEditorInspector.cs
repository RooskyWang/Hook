using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FormationEditor))]
public class FormationEditorInspector : Editor
{
	private FormationEditor formation;
	private int createPosCount = 5;

	private void OnEnable()
	{
		formation = serializedObject.targetObject as FormationEditor;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		EditorGUILayout.HelpBox("第一步：创建站位点并调整位置", MessageType.Info);
		createPosCount = EditorGUILayout.IntField("创建位置点的数量", createPosCount);

		if (GUILayout.Button("创建位置点"))
		{
			formation.CreatePos(createPosCount);
		}

		EditorGUILayout.HelpBox("第二步：创建测试单位，直观的看到站位点", MessageType.Info);
		if (GUILayout.Button("创建展示单位【测试站位】"))
		{
			formation.CreateShowUnit();
		}

		EditorGUILayout.HelpBox("第三步：删除测试单位并保存prefab", MessageType.Info);
		if (GUILayout.Button("删除展示单位"))
		{
			formation.DestoryShowUnit();
		}
	}

	private void OnDisable()
	{
		formation = null;
	}
}