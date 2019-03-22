using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0.5f) // left right
		{
			if (Input.GetAxis("Vertical") > 0.5f)
				UpperRightButton();
			else if (Input.GetAxis("Vertical") < -0.5f)
				DownRightButton();
			else
				RightButton();
		}
		else if (Input.GetAxis("Horizontal") < -0.5f)  // left right
		{
			if (Input.GetAxis("Vertical") > 0.5f)
				UpperLeftButton();
			else if (Input.GetAxis("Vertical") < -0.5f)
				DownLeftButton();
			else
				LeftButton();
		}
		else if (Input.GetAxis("Vertical") > 0.5f)
			UpButton();
		else if (Input.GetAxis("Vertical") < -0.5f)
			DownButton();
	}


	public void UpButton()
	{
		PlayerMovement.Instance.Move(Direction.Up);
	}

	public void DownButton()
	{
		PlayerMovement.Instance.Move(Direction.Down);
	}

	public void LeftButton()
	{
		PlayerMovement.Instance.Move(Direction.Left);
	}

	public void RightButton()
	{
		PlayerMovement.Instance.Move(Direction.Right);
	}

	public void UpperLeftButton()
	{
		PlayerMovement.Instance.Move(Direction.UpLeft);
	}

	public void UpperRightButton()
	{
		PlayerMovement.Instance.Move(Direction.UpRight);
	}

	public void DownLeftButton()
	{
		PlayerMovement.Instance.Move(Direction.DownLeft);
	}

	public void DownRightButton()
	{
		PlayerMovement.Instance.Move(Direction.DownRight);
	}
}
