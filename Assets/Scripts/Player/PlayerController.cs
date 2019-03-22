using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Instance
	private static PlayerController instance;
	public static PlayerController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<PlayerController>();
				if (instance == null)
					Debug.LogError("No PlayerController found on the scene!");
			}
			return instance;
		}
	}
	#endregion


}
