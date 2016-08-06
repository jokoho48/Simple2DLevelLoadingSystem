using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(loadLevelCore))]
public class ObjectBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		loadLevelCore myScript = (loadLevelCore)target;
		if(GUILayout.Button("Build Object"))
		{
			myScript.rebuildLevel();
		}
	}
}