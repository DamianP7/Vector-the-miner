using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	[SerializeField]
	Sprite left, right, front, back;

	[SerializeField]
	SpriteRenderer renderer;

	public void GoLeft()
	{
		renderer.sprite = left;
	}

	public void GoRight()
	{
		renderer.sprite = right;
	}

	public void GoUp()
	{
		renderer.sprite = back;
	}

	public void GoDown()
	{
		renderer.sprite = back;
	}
}
