using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSave
{
	/// <summary>
	/// Autosave otwartych scen po kliknięciu play
	/// </summary>
	static AutoSave()
	{
		EditorApplication.playmodeStateChanged += () =>
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				Debug.Log("Auto-saving all open scenes...");
				EditorSceneManager.SaveOpenScenes();
				AssetDatabase.SaveAssets();
			}
		};
	}
}
