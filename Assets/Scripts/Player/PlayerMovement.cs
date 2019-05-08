using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
	[SerializeField] PlayerAnimations animations;
    float tileSize;

    public PlayerMovement(PlayerAnimations animations)
    {
        this.animations = animations;
        tileSize = MapManager.Instance.tileSize;
    }

	public void MoveTransformInDirecton(Direction dir, float speed)
	{
        Vector2 position = Player.Instance.Position;

        switch (dir)
		{
			case Direction.Down:
				animations.GoDown();
				position = new Vector3(position.x, position.y - tileSize);
				break;
			case Direction.DownLeft:
				animations.GoLeft();
				position = new Vector3(position.x - tileSize, position.y - tileSize);
				break;
			case Direction.Left:
				animations.GoLeft();
				position = new Vector3(position.x - tileSize, position.y);
				break;
			case Direction.UpLeft:
				animations.GoLeft();
				position = new Vector3(position.x - tileSize, position.y + tileSize);
				break;
			case Direction.Up:
				animations.GoUp();
				position = new Vector3(position.x, position.y + tileSize);
				break;
			case Direction.UpRight:
				animations.GoRight();
				position = new Vector3(position.x + tileSize, position.y + tileSize);
				break;
			case Direction.Right:
				animations.GoRight();
				position = new Vector3(position.x + tileSize, position.y);
				break;
			case Direction.DownRight:
				animations.GoRight();
				position = new Vector3(position.x + tileSize, position.y - tileSize);
				break;
		}

        Player.Instance.Position = position;
    }
}
