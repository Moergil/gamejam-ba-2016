using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

[CustomEditor(typeof(RunPathSpawner))]
public class RunPathSpawnerEditor : Editor
{

	//SerializedProperty lookAtPoint;
    
	void OnEnable()
	{
		//lookAtPoint = serializedObject.FindProperty("lookAtPoint");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		//serializedObject.Update();
		//EditorGUILayout.PropertyField(lookAtPoint);
		//serializedObject.ApplyModifiedProperties();

		bool build = GUILayout.Button("Build");
		if (build) {
			RunPathSpawner runPathManager = (RunPathSpawner)target;
			runPathManager.EditorInstantiateAllSegmentsTest();
		}

		bool clear = GUILayout.Button("Clear");
		if (clear) {
			RunPathSpawner runPathManager = (RunPathSpawner)target;
			runPathManager.EditorClearAllSegments();
		}
	}

}
