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
		PlayerMovement.Instance.MoveUp();
	}

	public void DownButton()
	{
		PlayerMovement.Instance.MoveDown();
	}

	public void LeftButton()
	{
		PlayerMovement.Instance.MoveLeft();
	}

	public void RightButton()
	{
		PlayerMovement.Instance.MoveRight();
	}

	public void UpperLeftButton()
	{
		PlayerMovement.Instance.MoveUpperLeft();
	}

	public void UpperRightButton()
	{
		PlayerMovement.Instance.MoveUpperRight();
	}

	public void DownLeftButton()
	{
		PlayerMovement.Instance.MoveDownLeft();
	}

	public void DownRightButton()
	{
		PlayerMovement.Instance.MoveDownRight();
	}
}
