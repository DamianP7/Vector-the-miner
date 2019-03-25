using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OresLines))]
public class OresLinesEditor : Editor
{
	OresLines myTarget;
	public float squareSize = 2.66f;

	void OnEnable()
	{
		myTarget = (OresLines)target;
		if (myTarget.colors.Count == 0)
		{
			myTarget.colors = new List<Color>();
			for (int i = 0; i < myTarget.ores.ores.Count; i++)
			{
				myTarget.colors.Add(Color.black);
			}
		}
		else if (myTarget.colors.Count < myTarget.ores.ores.Count)
		{
			while (myTarget.colors.Count < myTarget.ores.ores.Count)
			{
				myTarget.colors.Add(Color.black);
			}
		}
		if (myTarget.enabledAreas.Count == 0)
		{
			myTarget.enabledAreas = new List<bool>();
			for (int i = 0; i < myTarget.ores.ores.Count; i++)
			{
				myTarget.enabledAreas.Add(false);
			}
		}
		else if (myTarget.enabledAreas.Count < myTarget.ores.ores.Count)
		{
			while (myTarget.enabledAreas.Count < myTarget.ores.ores.Count)
			{
				myTarget.enabledAreas.Add(false);
			}
		}
		//if (myTarget.groups.GetLength(0) < myTarget.ores.ores.Count)
		{
			myTarget.groups = new bool[myTarget.ores.ores.Count, 3];
			for (int i = 0; i < myTarget.ores.ores.Count; i++)
			{
				myTarget.groups[i, 0] = false;
				myTarget.groups[i, 1] = false;
				myTarget.groups[i, 2] = false;
			}
		}
	}


	public override void OnInspectorGUI()
	{
		for (int i = 0; i < myTarget.ores.ores.Count; i++)
		{
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Show " + myTarget.ores.ores[i].oreType.ToString(), GUILayout.ExpandWidth(false)))
			{
				myTarget.enabledAreas[i] = !myTarget.enabledAreas[i];
			}
			if (myTarget.enabledAreas[i])
			{
				myTarget.colors[i] = EditorGUILayout.ColorField(myTarget.colors[i]);
				GUILayout.EndHorizontal();
				myTarget.groups[i, 0] = EditorGUILayout.Toggle("small", myTarget.groups[i, 0], GUILayout.ExpandWidth(false));
				myTarget.groups[i, 1] = EditorGUILayout.Toggle("medium", myTarget.groups[i, 1], GUILayout.ExpandWidth(false));
				myTarget.groups[i, 2] = EditorGUILayout.Toggle("large", myTarget.groups[i, 2], GUILayout.ExpandWidth(false));
				GUILayout.BeginHorizontal();
			}
			GUILayout.EndHorizontal();
		}

		DrawDefaultInspector();
	}



}