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
	}

}
