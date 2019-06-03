using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	const string IDLE = "Idle",
		MOVE = "Idle",
		HIT = "Hit";

	[SerializeField] Animator left, right, front, back;

	Direction lastDir;
	Animator animator;

	private void Awake()
	{
		animator = right;
	}

	public void PlayAnimation(Direction dir, Action action)
	{
		if (dir == Direction.DownLeft || dir == Direction.UpLeft)
			dir = Direction.Left;
		else if (dir == Direction.DownRight || dir == Direction.UpRight)
			dir = Direction.Right;
		else if (dir == Direction.Center)
			return;

		if (action == Action.Dig || action == Action.DigAndRope)
			HitInDirection(dir);
		else if (action == Action.Climb || action == Action.Move || action == Action.JumpDown)
			MoveInDirection(dir);
		else
			return;
	}

	public void MoveInDirection(Direction dir)
	{
		CheckAnimator(dir);
		animator.SetTrigger(MOVE);
	}

	public void HitInDirection(Direction dir)
	{
		CheckAnimator(dir);
		animator.SetTrigger(HIT);
	}

	public void GoLeft()
	{
		CheckAnimator(Direction.Left);
		animator.SetTrigger(MOVE);
	}

	public void GoRight()
	{
		CheckAnimator(Direction.Right);
		animator.SetTrigger(MOVE);
	}

	public void GoUp()
	{
		CheckAnimator(Direction.Up);
		animator.SetTrigger(MOVE);
	}

	public void GoDown()
	{
		CheckAnimator(Direction.Down);
		animator.SetTrigger(MOVE);
	}

	void CheckAnimator(Direction dir)
	{
		if (dir != lastDir)
		{
			if (dir == Direction.Up || dir == Direction.Down)
				return;


			animator.gameObject.SetActive(false);

			switch (dir)
			{
				case Direction.Down:
					animator = back;
					break;
				case Direction.Left:
					animator = left;
					break;
				case Direction.Up:
					animator = back;
					break;
				case Direction.Right:
					animator = right;
					break;
				case Direction.Center:
					animator = front;
					break;
			}

			lastDir = dir;

			animator.gameObject.SetActive(true);
		}
	}
}
