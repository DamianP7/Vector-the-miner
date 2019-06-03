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
				position = new Vector3(position.x, position.y - tileSize);
				break;
			case Direction.DownLeft:
				position = new Vector3(position.x - tileSize, position.y - tileSize);
				break;
			case Direction.Left:
				position = new Vector3(position.x - tileSize, position.y);
				break;
			case Direction.UpLeft:
				position = new Vector3(position.x - tileSize, position.y + tileSize);
				break;
			case Direction.Up:
				position = new Vector3(position.x, position.y + tileSize);
				break;
			case Direction.UpRight:
				position = new Vector3(position.x + tileSize, position.y + tileSize);
				break;
			case Direction.Right:
				position = new Vector3(position.x + tileSize, position.y);
				break;
			case Direction.DownRight:
				position = new Vector3(position.x + tileSize, position.y - tileSize);
				break;
		}

        Player.Instance.Position = position;
    }
}
