using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	#region Instance
	private static MapManager instance;
	public static MapManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<MapManager>();
				if (instance == null)
					Debug.LogError("No MapManager found on the scene!");
			}
			return instance;
		}
	}
	#endregion
	[Header("Prefabs")]
	[SerializeField] TileMap prefabGround;
	[SerializeField] TileMap prefabSurface;
	[SerializeField] TileMap prefabEmpty, prefabHole;
	/*
	[Header("Sprites")]
	[SerializeField] Sprite[] spritesGround;
	[SerializeField] Sprite[] spritesSurface;*/

	[Header("Variables")]
	public float tileSize;
	[SerializeField] int mapSizeX, mapSizeY;
	[SerializeField] bool forcedGenerating = false;
	[SerializeField] TilesSettings tilesSettings;
	int sGround, sSurface, sHole; // sprite quantity

	TileMap[,] tiles;


	private void Awake()
	{
		if (forcedGenerating || !SaveManager.Instance.loaded)
		{
			foreach (TilesSettings.TileSett tile in tilesSettings.tiles)
			{
				switch (tile.tileType)  //TODO: TileType switch
				{
					case TileType.Surface:
						sSurface = tile.tileSprites.Length;
						break;
					case TileType.Ground:
						sGround = tile.tileSprites.Length;
						break;
					case TileType.Empty:
						break;
					case TileType.Building:
						break;
					case TileType.Rope:
						break;
					case TileType.Hole:
						sHole = tile.tileSprites.Length;
						break;
				}
			}

			GenerateMap();
			SaveManager.Instance.SaveMap();
		}
		else
		{
			tiles = SaveManager.Instance.GetMap();
			DrawMap();
		}
	}

	#region GenerateMap
	private void GenerateMap()
	{
		tiles = new TileMap[mapSizeX, mapSizeY];
		float xPos;
		float yPos = 0;

		// row above surface
		xPos = -mapSizeX * tileSize / 2;
		for (int i = 0; i < mapSizeX; i++)
		{
			tiles[i, 0] = InstantiateTile(TileType.Empty, xPos, yPos);
			xPos += tileSize;
		}
		yPos -= tileSize;

		// row surface
		xPos = -mapSizeX * tileSize / 2;
		for (int i = 0; i < mapSizeX; i++)
		{
			if (i == mapSizeX/2)	// TODO: temp
				tiles[mapSizeX / 2, 1] = InstantiateTile(TileType.Hole, xPos, yPos);
			else
				tiles[i, 1] = InstantiateTile(TileType.Surface, xPos, yPos);
			xPos += tileSize;
		}
		PlayerMovement.Instance.xPos = mapSizeX / 2;
		PlayerMovement.Instance.yPos = 0;
		yPos -= tileSize;

		// underground
		for (int i = 2; i < mapSizeY; i++)
		{
			xPos = -mapSizeX * tileSize / 2;
			for (int j = 0; j < mapSizeX; j++)
			{
				tiles[j, i] = InstantiateTile(TileType.Ground, xPos, yPos);
				xPos += tileSize;
			}

			yPos -= tileSize;
		}
	}

	private void DrawMap()
	{
		float xPos;
		float yPos = 0;

		for (int i = 0; i < tiles.GetLength(1); i++)
		{
			xPos = -tiles.GetLength(0) * tileSize / 2;
			for (int j = 0; j < tiles.GetLength(0); j++)
			{
				tiles[j, i] = InstantiateTile(tiles[j, i], xPos, yPos);
				xPos += tileSize;
				Debug.Log("instantinate : " + tiles[j, i].name);
			}

			yPos -= tileSize;
		}
	}

	private TileMap InstantiateTile(TileType type, float xPos, float yPos)
	{
		TileMap newTile;
		TileMap prefab;
		int spriteNum;

		switch (type)   //TODO: TileType switch
		{
			case TileType.Surface:
				prefab = prefabSurface;
				spriteNum = Random.Range(0, sSurface);
				break;
			case TileType.Ground:
				prefab = prefabGround;
				spriteNum = Random.Range(0, sGround);
				break;
			case TileType.Empty:
				prefab = prefabEmpty;
				spriteNum = 0;
				break;
			case TileType.Hole:
				prefab = prefabHole;
				spriteNum = Random.Range(0, sHole);
				break;
			default:
				prefab = prefabGround;
				spriteNum = Random.Range(0, sGround);
				Debug.LogError("Unexpected type of tile (" + this.name + ')');
				break;
		}
		newTile = Instantiate(prefab, new Vector3(xPos, yPos), Quaternion.identity, this.transform) as TileMap;
		newTile.tileType = type;
		newTile.spriteNumber = spriteNum;
		return newTile;
	}

	private TileMap InstantiateTile(TileMap tile, float xPos, float yPos)
	{
		TileMap newTile;
		TileMap prefab;

		switch (tile.tileType)  //TODO: TileType switch
		{
			case TileType.Surface:
				prefab = prefabSurface;
				break;
			case TileType.Ground:
				prefab = prefabGround;
				break;
			case TileType.Empty:
				prefab = prefabEmpty;
				break;
			case TileType.Hole:
				prefab = prefabHole;
				break;
			default:
				prefab = prefabGround;
				Debug.LogError("Unexpected type of tile (" + this.name + ')');
				break;
		}
		newTile = Instantiate(prefab, new Vector3(xPos, yPos), Quaternion.identity, this.transform) as TileMap;
		newTile.tileType = tile.tileType;
		newTile.spriteNumber = tile.spriteNumber;
		return newTile;
	}
	#endregion

	public TileMap[,] SaveMap()
	{
		return tiles;
	}

	public TileMap GetTile(int x, int y)
	{
		return tiles[x, y];
	}
}
