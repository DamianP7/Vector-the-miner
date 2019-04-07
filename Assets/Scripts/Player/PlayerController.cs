using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour   //TODO: bez MonoBehaviour
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

	[SerializeField] PlayerBag playerBag;
	[SerializeField] PlayerAnimations animations; // TODO: jeszcze nie wiem jak to zrobić. MYŚL!!

	public int xPos, yPos;
	public Direction lastDirection = Direction.Center;

	// TODO: zmień PlayerController na ważniejszy. Movement ma wysłać info o chęci wykonania
	// akcji a tutaj ma działać cała logika
	public Action TryMove(Direction dir)
	{
		Action action = GetAction(dir);

		if (action == Action.Nothing)
			return action;

		if (action == Action.Dig)
		{
			TileMap tile = GetTileInDirection(dir);

			// przekaż tile do animatora który będzie uruchamiał eventy w animacji
			Dig(tile);

		}
		else if (action == Action.DigAndRope)
		{
			TileMap myTile = GetMyTile();
			TileMap tile = GetTileInDirection(dir);
			Rope rope = myTile.GetElement(ElementType.Rope) as Rope;
			rope.length--;
			rope.isLast = true;
			Dig(tile, rope);

			rope.isLast = false;
			myTile.RefreshElements();
			// przekaż tile do animatora który będzie uruchamiał eventy w animacji

		}



		lastDirection = dir;

		return action;
	}

	void Dig(TileMap tile, Rope rope = null)
	{
		// Sprawdź czy jest w stanie kopać
		if (tile.ore != Ore.Ground)
		{
			playerBag.AddOre(tile.ore, tile.oreAmount);
		}
		if (rope != null)
			tile.Dig(rope);
		else
			tile.Dig();
	}

	Action GetAction(Direction dir)
	{
		TileMap tile = GetTileInDirection(dir);

		switch (tile.tileType)  // TODO: tileType switch
		{
			case TileType.Surface:
				return Action.Nothing;

			case TileType.Ground:
				{
					if (dir == Direction.Left || dir == Direction.Right)
						return Action.Dig;
					if (dir == Direction.Down)
						return CheckDig(Direction.Down);
					// if ladder
				}
				break;

			case TileType.Empty:
				if (dir == Direction.Left || dir == Direction.Right)
					return Action.Move;
				if (dir == Direction.Up && CheckClimbing())
					return Action.Move;
				break;

			case TileType.Building:
				if (dir == Direction.Left || dir == Direction.Right)    // up
					return Action.Move;
				break;

			case TileType.Hole:
				// jeśli jest przedmiot umozliwiający poruszanie
				if (dir == Direction.Left || dir == Direction.Right)
					return Action.Move;
				else if (dir == Direction.Down)
				{
					if (GetTileInDirection(Direction.Down).GetElement(ElementType.Rope) != null)
						return Action.Move;
				}
				else if (dir == Direction.Up)
				{
					if (GetMyTile().GetElement(ElementType.Rope) != null)
						return Action.Move;
				}
				else if (dir == Direction.UpLeft
				&& GetTileInDirection(Direction.Up).tileType == TileType.Hole
				&& GetTileInDirection(Direction.Left).tileType == TileType.Ground)
					return Action.Climb;
				if (dir == Direction.UpRight
					&& GetTileInDirection(Direction.Up).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Right).tileType == TileType.Ground)
					return Action.Climb;
				if (dir == Direction.DownLeft
					&& GetTileInDirection(Direction.Left).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Down).tileType == TileType.Ground)
					return Action.JumpDown;
				if (dir == Direction.DownRight
					&& GetTileInDirection(Direction.Right).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Down).tileType == TileType.Ground)
					return Action.JumpDown;
				break;

			default:
				return Action.Nothing;
		}
		return Action.Nothing;
	}

	/// <summary>
	/// Sprawdza czy można kopać w dół. Zwraca odpowiednią akcję
	/// </summary>
	/// <param name="dir"></param>
	/// <returns></returns>
	private Action CheckDig(Direction dir)
	{
		TileMap tile = GetTileInDirection(dir);

		// dig if( gracz ma wystarczająco mocny ekwipunek )
		if (CheckRope())
			return Action.DigAndRope;
		else
			return Action.Dig;

	}

	/// <summary>
	/// Zwraca true jeśli można rozwinąć linę w dół
	/// </summary>
	/// <returns></returns>
	private bool CheckRope()
	{
		TileMap myTile = GetTileInDirection(Direction.Center);
		Rope rope = myTile.GetElement(ElementType.Rope) as Rope;
		if (rope == null)
		{
			return false;
		}
		if (rope.length > 0)
		{
			return true;
		}
		else
			return false;
	}

	/// <summary>
	/// Zwraca kafelek z podanego kierunku
	/// </summary>
	/// <param name="dir"></param>
	/// <returns></returns>
	public TileMap GetTileInDirection(Direction dir)
	{
		int x, y;
		switch (dir)
		{
			case Direction.Down:
				x = xPos;
				y = yPos + 1;
				break;
			case Direction.DownLeft:
				x = xPos - 1;
				y = yPos + 1;
				break;
			case Direction.Left:
				x = xPos - 1;
				y = yPos;
				break;
			case Direction.UpLeft:
				x = xPos - 1;
				y = yPos - 1;
				break;
			case Direction.Up:
				x = xPos;
				y = yPos - 1;
				break;
			case Direction.UpRight:
				x = xPos + 1;
				y = yPos - 1;
				break;
			case Direction.Right:
				x = xPos + 1;
				y = yPos;
				break;
			case Direction.DownRight:
				x = xPos + 1;
				y = yPos + 1;
				break;
			default:
				x = xPos;
				y = yPos;
				break;
		}

		return MapManager.Instance.GetTile(x, y);
	}

	/// <summary>
	/// Sprawdza czy można sie wspinać (na razie tylko lina)
	/// </summary>
	/// <returns></returns>
	private bool CheckClimbing()
	{
		if (GetTileInDirection(Direction.Center).tileType == TileType.Hole)
		{
			Element element = GetTileInDirection(Direction.Center).GetElement(ElementType.Rope);
			if (element == null)
				return false;
			else
				return true;
		}
		else
			return false;
	}

	public TileMap GetMyTile()
	{
		return GetTileInDirection(Direction.Center);
	}

	public void MovePos()
	{
		switch (lastDirection)
		{
			case Direction.Down:
				yPos++;
				break;
			case Direction.DownLeft:
				xPos--;
				yPos++;
				break;
			case Direction.Left:
				xPos--;
				break;
			case Direction.UpLeft:
				xPos--;
				yPos--;
				break;
			case Direction.Up:
				yPos--;
				break;
			case Direction.UpRight:
				xPos++;
				yPos--;
				break;
			case Direction.Right:
				xPos++;
				break;
			case Direction.DownRight:
				xPos++;
				yPos++;
				break;
			case Direction.Center:
				break;
		}
		lastDirection = Direction.Center;
	}


}

public enum Direction
{
	Down,
	DownLeft,
	Left,
	UpLeft,
	Up,
	UpRight,
	Right,
	DownRight,
	Center
}

public enum Action
{
	Move, Dig, Climb, JumpDown, Nothing, DigAndRope // TooLow, TooHight
}