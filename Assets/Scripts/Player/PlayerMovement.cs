using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region Instance
	private static PlayerMovement instance;
	public static PlayerMovement Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<PlayerMovement>();
				if (instance == null)
					Debug.LogError("No PlayerMovement found on the scene!");
			}
			return instance;
		}
	}
	#endregion

	public int xPos, yPos;

	[SerializeField] float timeToNextMove;

	bool canMove = true;
	float tileSize;
	int a = 0;

	private void Awake()
	{
		tileSize = MapManager.Instance.tileSize;
	}

	public void MoveUp()
	{
		if (canMove)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
			StartCoroutine(WaitForMove());
		}
	}

	public void MoveDown()
	{
		if (canMove)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - tileSize);
			StartCoroutine(WaitForMove());
		}
	}

	public void MoveLeft()
	{
		if (canMove)
		{
			transform.position = new Vector3(transform.position.x - tileSize, transform.position.y);
			StartCoroutine(WaitForMove());
		}
	}

	public void MoveRight()
	{
		if (canMove)
		{
			transform.position = new Vector3(transform.position.x + tileSize, transform.position.y);
			StartCoroutine(WaitForMove());
		}
	}

	public void MoveUpperLeft()
	{
		// sprawdź co możesz zrobić
	}

	public void MoveUpperRight()
	{
		// sprawdź co możesz zrobić
	}

	public void MoveDownLeft()
	{
		// sprawdź co możesz zrobić
	}

	public void MoveDownRight()
	{
		// sprawdź co możesz zrobić
	}

	IEnumerator WaitForMove()
	{
		Debug.Log("Coroutine " + a++);
		canMove = false;
		yield return new WaitForSeconds(timeToNextMove);
		canMove = true;
	}
}
