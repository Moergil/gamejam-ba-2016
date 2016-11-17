using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

[CustomEditor(typeof(RunPathManager))]
public class RunPathManagerEditor : Editor
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
			RunPathManager runPathManager = (RunPathManager)target;
			runPathManager.EditorInstantiateAllSegmentsTest();
		}

		bool clear = GUILayout.Button("Clear");
		if (clear) {
			RunPathManager runPathManager = (RunPathManager)target;
			runPathManager.EditorClearAllSegments();
		}
	}

}
