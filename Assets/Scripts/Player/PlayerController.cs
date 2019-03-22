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


	public int xPos, yPos;
	public Direction lastDirection = Direction.Center;

	public Action TryMove(Direction dir)
	{
		Action action = GetAction(dir);

		Debug.Log("Action: " + action.ToString());
		if (action == Action.Nothing)
			return action;





		lastDirection = dir;

		return action;
	}

	Action GetAction(Direction dir)
	{
		TileMap tile = GetTileInDirection(dir);

		switch (tile.tileType)  // TODO: tileType switch
		{
			case TileType.Surface:
				return Action.Nothing;

			case TileType.Ground:
				// if( gracz ma wystarczająco mocny ekwipunek )
				{
					if (dir == Direction.Left || dir == Direction.Right || dir == Direction.Down)
						return Action.Dig;
					// if ladder
				}
				break;

			case TileType.Empty:
				if (dir == Direction.Left || dir == Direction.Right)
					return Action.Move;
				if (dir == Direction.Up && GetTileInDirection(Direction.Center).tileType == TileType.Hole)
					return Action.Move;
				break;

			case TileType.Building:
				if (dir == Direction.Left || dir == Direction.Right)
					return Action.Move;
				break;

			case TileType.Hole:
				// jeśli jest przedmiot umozliwiający poruszanie
				if (dir == Direction.Left || dir == Direction.Right || dir == Direction.Down || dir == Direction.Up)
					return Action.Move;
				if (dir == Direction.UpLeft
					&& GetTileInDirection(Direction.Up).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Left).tileType == TileType.Ground)
					return Action.Climb;
				if (dir == Direction.UpRight
					&& GetTileInDirection(Direction.Up).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Right).tileType == TileType.Ground)
					return Action.Climb;
				if (dir == Direction.DownLeft
					&& GetTileInDirection(Direction.Left).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Center).tileType == TileType.Ground)
					return Action.JumpDown;
				if (dir == Direction.DownRight
					&& GetTileInDirection(Direction.Right).tileType == TileType.Hole
					&& GetTileInDirection(Direction.Center).tileType == TileType.Ground)
					return Action.JumpDown;
				break;

			default:
				return Action.Nothing;
		}
		return Action.Nothing;
	}

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
	Move, Dig, Climb, JumpDown, Nothing, // TooLow, TooHight
}