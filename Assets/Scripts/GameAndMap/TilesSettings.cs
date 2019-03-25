using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TilesSettings : ScriptableObject
{
	public List<TileSett> tiles;

	public Sprite GetSprite(TileType type, int number)
	{
		TileSett tile = tiles.Find(x => x.tileType == type);
		if (tile == null)
		{
			Debug.LogError("Unexpected tile type: " + type.ToString() + " (" + this.name + ')');
			return null;
		}
		else
		{
			if (number < tile.tileSprites.Length)
				return tile.tileSprites[number];
			else
			{
				if (tile.tileSprites.Length == 0)
				{
					if (tile.tileType != TileType.Empty)    // TODO: temp
						Debug.LogError("There're no sprites (" + this.name + ')');
					return null;
				}
				else
				{
					Debug.LogError("Unknown sprite number: " + number + " (" + this.name + ')');
					return tile.tileSprites[0];
				}
			}
		}
	}

	public TileSett GetTileSettings(TileType type)
	{
		TileMap tile = new TileMap();
		TileSett sett = tiles.Find(x => x.tileType == type);
		if (sett == null)
		{
			Debug.LogError("Unexpected tile type (" + this.name + ')');
			return null;
		}
		return sett;
	}

	public TileSett GetOreSettings(Ore ore)
	{
		TileMap tile = new TileMap();
		TileSett sett = tiles.Find(x => x.ore == ore);
		if (sett == null)
		{
			Debug.LogError("Unexpected ore type (" + ore.ToString() + ')');
			return null;
		}
		return sett;
	}




	[System.Serializable]
	public class TileSett
	{
		[SerializeField] string name;
		public TileType tileType;
		public Ore ore;
		public float hardness;
		public Sprite[] tileSprites;
	}
}
