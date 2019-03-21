using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class HierarchyGUI
{
	static Texture2D UITexture, canvasTexture, cameraTexture, gameManagerTexture, playerTexture, segmentTexture, levelManagerTexture;
	static GUIStyle style;

	//constructor
	static HierarchyGUI()
	{
		//Subscribe to the event that is called for every visible list item in the HierarchyWindow on every OnGUI event
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

		UITexture = (Texture2D)Resources.Load("Editor/UI Texture");
		canvasTexture = (Texture2D)Resources.Load("Editor/Canvas Texture");
		cameraTexture = (Texture2D)Resources.Load("Editor/Camera Texture");
		playerTexture = (Texture2D)Resources.Load("Editor/Player Texture");
		segmentTexture = (Texture2D)Resources.Load("Editor/Segment Texture");
		gameManagerTexture = (Texture2D)Resources.Load("Editor/GameManager Texture");
		levelManagerTexture = (Texture2D)Resources.Load("Editor/LevelManager Texture");

		//new guistyle for the markers
		style = new GUIStyle();
		style.fontSize = 12;

	}

	static void OnHierarchyGUI(int instanceID, Rect selectionRect)
	{
		//get the gameObject reference using its instance ID
		GameObject go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

		//get rect m8
		Rect rect = new Rect(selectionRect);
		rect.x = rect.width;

		if (go == null)
			return;

		//Toggle
		if (EditorPrefs.GetInt(go.GetInstanceID() + "toggle", 0) == 1)
		{
			go.SetActive(GUI.Toggle(rect, go.activeInHierarchy, ""));
		}

		//UI marker
		if (EditorPrefs.GetInt(go.GetInstanceID() + "UI", 0) == 1)
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = UITexture;
			GUI.Label(rect, "", style);
		}

		//Game Manager marker
		if (CheckInEditor(go, "G"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = gameManagerTexture;
			GUI.Label(rect, "", style);
		}

		//Canvas marker
		if (CheckInEditor(go, "Can"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = canvasTexture;
			GUI.Label(rect, "", style);
		}

		//Camera marker
		if (CheckInEditor(go, "Cam"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = cameraTexture;
			GUI.Label(rect, "", style);
		}

		//Player marker
		if (CheckInEditor(go, "P"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = playerTexture;
			GUI.Label(rect, "", style);
		}

		//Segment marker
		if (CheckInEditor(go, "S"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = segmentTexture;
			GUI.Label(rect, "", style);
		}

		//Level Manager marker
		if (CheckInEditor(go, "L"))
		{
			rect.x = rect.width - 15;
			rect.width = 15;
			rect.height = rect.height - 2;
			style.normal.background = levelManagerTexture;
			GUI.Label(rect, "", style);
		}

	}

	static bool CheckInEditor(GameObject go, string stringId)
	{
		int val = EditorPrefs.GetInt(go.GetInstanceID() + stringId, -1);
		if (val == 1)
			return true;
		else if (val == 0)
			return false;
		else
		{
			switch (stringId)
			{
				case "Cam":
					if (go.name.Contains("Camera"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				case "Can":
					if (go.name.Contains("Canvas"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				case "G":
					if (go.name.Contains("GameManager"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				case "P":
					if (go.name.Contains("Player"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				case "S":
					if (go.name.Contains("Segment"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				case "L":
					if (go.name.Contains("LevelManager"))
					{
						EditorPrefs.SetInt(go.GetInstanceID() + stringId, 1);
						return true;
					}
					else
					{
						return false;
					}
				default:
					return false;
			}
		}
	}


	[MenuItem("GameObject/Marker/Toggle %#T", false, 0)]
	static void AddToggle()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "toggle", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "toggle", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "toggle", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/UI", false, 9)]
	static void AddUIMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "UI", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "UI", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "UI", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/Canvas", false, 1)]
	static void AddCanvasMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "Can", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "Can", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "Can", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/GameManager", false, 2)]
	static void AddGameManagerMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "G", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "G", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "G", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/Player", false, 3)]
	static void AddPlayerMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "P", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "P", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "P", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/Segment", false, 4)]
	static void AddSegmentMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "S", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "S", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "S", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/LevelManager", false, 5)]
	static void AddLevelManagerMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "L", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "L", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "L", 0);
			}
		}
	}

	[MenuItem("GameObject/Marker/Camera", false, 6)]
	static void AddCameraMarker()
	{
		foreach (Object o in Selection.gameObjects)
		{
			if (EditorPrefs.GetInt(o.GetInstanceID() + "Cam", 0) == 0)
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "Cam", 1);
			}

			else
			{
				EditorPrefs.SetInt(o.GetInstanceID() + "Cam", 0);
			}
		}
	}


	//Remove all markers
	[MenuItem("GameObject/Marker/Remove", false, 13)]
	static void RemoveMarkers()
	{
		foreach (Object o in Selection.gameObjects)
		{
			EditorPrefs.SetInt(o.GetInstanceID() + "toggle", 0);
			EditorPrefs.SetInt(o.GetInstanceID() + "G", 0);
			EditorPrefs.SetInt(o.GetInstanceID() + "UI", 0);
		}
	}
	/*
	//Stop drawing all markers
	[MenuItem("GameObject/Marker/Stop Drawing", false, 14)]
	static void StopMarkers()
	{
		EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
	}

	//Start drawing all markers
	[MenuItem("GameObject/Marker/Start Drawing", false, 14)]
	static void StartMarkers()
	{
		EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
	}

	//Stop drawing all markers
	[MenuItem("GameObject/Marker/Stop Drawing", true)]
	static bool ValidateStopMarkers()
	{
		System.Delegate[] delegates = EditorApplication.hierarchyWindowItemOnGUI.GetInvocationList();
		foreach (var item in delegates)
		{
			if (item.Method.Name.Equals("OnHierarchyGUI"))
				return true;
		}
		return false;
	}

	//Start drawing all markers
	[MenuItem("GameObject/Marker/Start Drawing", true)]
	static bool ValidateStartMarkers()
	{
		System.Delegate[] delegates = EditorApplication.hierarchyWindowItemOnGUI.GetInvocationList();
		foreach (var item in delegates)
		{
			if (item.Method.Name.Equals("OnHierarchyGUI"))
				return false;
		}
		return true;
	}*/
}