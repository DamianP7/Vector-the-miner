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

	[SerializeField] float timeToNextMove;
	[SerializeField] PlayerAnimations animations;

	bool canMove = true;
	float tileSize;
	PlayerController playerController;

	private void Start()
	{
		tileSize = MapManager.Instance.tileSize;
		playerController = PlayerController.Instance;
	}

	public void Move(Direction dir)
	{
		if (!canMove)
			return;

		Action action = playerController.TryMove(dir);
		Debug.Log(action.ToString());

		if (action == Action.Nothing)
		{
			// temp TODO: przemyśl to
			if (dir == Direction.DownLeft)
			{
				action = playerController.TryMove(Direction.Left);
				dir = Direction.Left;
			}
			else if (dir == Direction.UpLeft)
			{
				action = playerController.TryMove(Direction.Left);
				dir = Direction.Left;
			}
			else if (dir == Direction.DownRight)
			{
				action = playerController.TryMove(Direction.Right);
				dir = Direction.Right;
			}
			else if (dir == Direction.UpRight)
			{
				action = playerController.TryMove(Direction.Right);
				dir = Direction.Right;
			}
		}

		if (action != Action.Nothing) // TODO: temp
		{
			MoveTransformInDirecton(dir, 1);
			playerController.MovePos(); // zakończenie ruchu i update pozycji gracza na siatce
		}

		StartCoroutine(WaitForMove());
	}

	void MoveTransformInDirecton(Direction dir, float speed)
	{
		switch (dir)
		{
			case Direction.Down:
				animations.GoDown();
				transform.position = new Vector3(transform.position.x, transform.position.y - tileSize);
				break;
			case Direction.DownLeft:
				animations.GoLeft();
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y - tileSize);
				break;
			case Direction.Left:
				animations.GoLeft();
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y);
				break;
			case Direction.UpLeft:
				animations.GoLeft();
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y + tileSize);
				break;
			case Direction.Up:
				animations.GoUp();
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				break;
			case Direction.UpRight:
				animations.GoRight();
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y + tileSize);
				break;
			case Direction.Right:
				animations.GoRight();
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y);
				break;
			case Direction.DownRight:
				animations.GoRight();
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y - tileSize);
				break;
		}
	}

	IEnumerator WaitForMove()
	{
		canMove = false;
		yield return new WaitForSeconds(timeToNextMove);
		canMove = true;
	}

	public IEnumerator Moving(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}
}
