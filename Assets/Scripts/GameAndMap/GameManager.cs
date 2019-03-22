using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Instance
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<GameManager>();
				if (instance == null)
					Debug.LogError("No GameManager" +
						" found on the scene!");
			}
			return instance;
		}
	}
	#endregion



}
