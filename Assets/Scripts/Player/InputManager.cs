using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	#region Instance
	private static InputManager instance;
	public static InputManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<InputManager>();
				if (instance == null)
					Debug.LogError("No InputManager found on the scene!");
			}
			return instance;
		}
	}
	#endregion
	public delegate void DirDelegate(Direction dir);
	DirDelegate MoveToDirection;

	[SerializeField] GameObject buttons;

	private void Start()
	{
		ChangeToPlayerMovement();
	}

	public void ChangeToPlayerMovement()
	{
		StartMovement();
		MoveToDirection = new DirDelegate(Player.Instance.controller.TryMove);
	}

	public void ChangeToThisMovement(DirDelegate dirDelegate)
	{
		StartMovement();
		MoveToDirection = new DirDelegate(dirDelegate);
	}

	public void StopMovement()
	{
		buttons.SetActive(false);
	}
	public void StartMovement()
	{
		buttons.SetActive(true);
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0.5f) // left right
		{
			if (Input.GetAxis("Vertical") > 0.5f)
				MoveToDirection(Direction.UpRight);
			else if (Input.GetAxis("Vertical") < -0.5f)
				MoveToDirection(Direction.DownRight);
			else
				MoveToDirection(Direction.Right);
		}
		else if (Input.GetAxis("Horizontal") < -0.5f)  // left right
		{
			if (Input.GetAxis("Vertical") > 0.5f)
				MoveToDirection(Direction.UpLeft);
			else if (Input.GetAxis("Vertical") < -0.5f)
				MoveToDirection(Direction.DownLeft);
			else
				MoveToDirection(Direction.Left);
		}
		else if (Input.GetAxis("Vertical") > 0.5f)
			MoveToDirection(Direction.Up);
		else if (Input.GetAxis("Vertical") < -0.5f)
			MoveToDirection(Direction.Down);
	}


	public void UpButton()
	{
		MoveToDirection(Direction.Up);
	}

	public void DownButton()
	{
		MoveToDirection(Direction.Down);
	}

	public void LeftButton()
	{
		MoveToDirection(Direction.Left);
	}

	public void RightButton()
	{
		MoveToDirection(Direction.Right);
	}

	public void UpperLeftButton()
	{
		MoveToDirection(Direction.UpLeft);
	}

	public void UpperRightButton()
	{
		MoveToDirection(Direction.UpRight);
	}

	public void DownLeftButton()
	{
		MoveToDirection(Direction.DownLeft);
	}

	public void DownRightButton()
	{
		MoveToDirection(Direction.DownRight);
	}
}
