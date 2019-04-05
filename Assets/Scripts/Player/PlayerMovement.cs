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


		if (action == Action.Dig)
		{
			TileMap tile = playerController.GetTileInDirection(dir);

			// przekaż tile do animatora który będzie uruchamiał eventy w animacji
			tile.Dig(); // temp

		}

		if (action == Action.DigAndRope)
		{
			Debug.Log("Rope");
			TileMap myTile = playerController.GetMyTile();
			TileMap tile = playerController.GetTileInDirection(dir);
			Rope rope = myTile.GetElement(ElementType.Rope) as Rope;
			rope.length--;
			rope.isLast = true;
			tile.Dig(rope); // temp
			rope.isLast = false;
			myTile.RefreshElements();
			// przekaż tile do animatora który będzie uruchamiał eventy w animacji

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
				transform.position = new Vector3(transform.position.x, transform.position.y - tileSize);
				break;
			case Direction.DownLeft:
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y - tileSize);
				break;
			case Direction.Left:
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y);
				break;
			case Direction.UpLeft:
				transform.position = new Vector3(transform.position.x - tileSize, transform.position.y + tileSize);
				break;
			case Direction.Up:
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				break;
			case Direction.UpRight:
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y + tileSize);
				break;
			case Direction.Right:
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y);
				break;
			case Direction.DownRight:
				transform.position = new Vector3(transform.position.x + tileSize, transform.position.y - tileSize);
				break;
		}
	}

	public void MoveUp()
	{
		if (canMove)
		{
			Action action = playerController.TryMove(Direction.Up);
			if (action == Action.Move)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				StartCoroutine(WaitForMove());
			}
			if (action == Action.Dig)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				StartCoroutine(WaitForMove());
			}
			if (action == Action.Climb)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				StartCoroutine(WaitForMove());
			}
			if (action == Action.JumpDown)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + tileSize);
				StartCoroutine(WaitForMove());
			}
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
