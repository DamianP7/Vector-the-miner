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
	[SerializeField] TileMap prefabSurface, prefabEmpty, prefabHole, prefabBedrock, prefabOre;
	/*
	[Header("Sprites")]
	[SerializeField] Sprite[] spritesGround;
	[SerializeField] Sprite[] spritesSurface;*/

	[Header("Variables")]
	public float tileSize;
	[SerializeField] int mapSizeX, mapSizeY, bedrockSize, surfaceLevel;
	[SerializeField] bool forcedGenerating = false;
	[SerializeField] TilesSettings tilesSettings;
	[SerializeField] OresSettings oresSettings;
	int sGround, sSurface, sHole; // sprite quantity

	TileMap[,] tiles;
	MapGenerator.TileProject[,] tileProject;


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
		prefabGround.ore = Ore.Ground;
		mapSizeX += bedrockSize * 2;
		mapSizeY += bedrockSize;
		tiles = new TileMap[mapSizeX, mapSizeY];
		float yPos = 0;

		SetupPlayer();

		MapGenerator generator = new MapGenerator();
		tileProject = generator.GenerateOres(mapSizeX, mapSizeY, surfaceLevel, bedrockSize, oresSettings, prefabGround);

		// row above surface
		SpawnAboveSurface(ref yPos);

		// row surface
		SpawnSurface(ref yPos);

		// underground
		SpawnGrounds(yPos);

		//bedrock
		SpawnBedrock();
	}

	private void SpawnGrounds(float yPos)
	{
		float xPos;

		for (int i = surfaceLevel + 1; i < mapSizeY - bedrockSize; i++)
		{
			xPos = -mapSizeX * tileSize / 2;
			xPos += tileSize * bedrockSize;
			for (int j = bedrockSize; j < mapSizeX - bedrockSize; j++)
			{
				if (tileProject[j, i] == null)
				{
					tiles[j, i] = InstantiateTile(TileType.Ground, xPos, yPos);
					//Debug.Log("		Instantiate ground: " + tiles[j, i].ore.ToString());
				}
				else
				{
					prefabOre.ore = tileProject[j, i].ore;
					prefabOre.spriteNumber = tileProject[j, i].groupSize;
					tiles[j, i] = InstantiateTile(prefabOre, xPos, yPos);
				}

				xPos += tileSize;
			}

			yPos -= tileSize;
		}
	}

	private void SpawnAboveSurface(ref float yPos)
	{
		float xPos = -mapSizeX * tileSize / 2;
		xPos += tileSize * bedrockSize;
		for (int j = 0; j < surfaceLevel; j++)
		{
			for (int i = bedrockSize; i < mapSizeX - bedrockSize; i++)
			{
				tiles[i, j] = InstantiateTile(TileType.Empty, xPos, yPos);
				xPos += tileSize;
			}
			yPos -= tileSize;
		}
	}

	private void SpawnSurface(ref float yPos)
	{
		float xPos = -mapSizeX * tileSize / 2;
		xPos += tileSize * bedrockSize;
		for (int i = bedrockSize; i < mapSizeX - bedrockSize; i++)
		{
			if (i == mapSizeX / 2)  // TODO: temp
			{
				tiles[mapSizeX / 2, surfaceLevel] = InstantiateTile(TileType.Hole, xPos, yPos);
				Rope rope = new Rope();
				rope.length = 10;
				rope.isLast = true;
				tiles[mapSizeX / 2, surfaceLevel].PlaceObject(rope);
			}
			else
				tiles[i, 1] = InstantiateTile(TileType.Surface, xPos, yPos);
			xPos += tileSize;
		}
		yPos -= tileSize;
	}

	private void SetupPlayer()
	{
		PlayerController.Instance.xPos = mapSizeX / 2;
		PlayerController.Instance.yPos = 0;
	}

	private void SpawnBedrock()
	{
		float xPos, yPos = 0;
		for (int i = 0; i < mapSizeY - bedrockSize; i++)
		{
			xPos = -mapSizeX * tileSize / 2;
			for (int j = 0; j < bedrockSize; j++)
			{
				tiles[j, i] = InstantiateTile(TileType.Locked, xPos, yPos);
				xPos += tileSize;
			}
			xPos = (mapSizeX - 2) * tileSize / 2;
			for (int j = mapSizeX - 1; j >= mapSizeX - bedrockSize; j--)
			{
				tiles[j, i] = InstantiateTile(TileType.Locked, xPos, yPos);
				xPos -= tileSize;
			}
			yPos -= tileSize;
		}

		for (int i = mapSizeY - bedrockSize; i < mapSizeY; i++)
		{
			xPos = -mapSizeX * tileSize / 2;
			for (int j = 0; j < mapSizeX; j++)
			{
				tiles[j, i] = InstantiateTile(TileType.Locked, xPos, yPos);
				xPos += tileSize;
			}
			yPos -= tileSize;
		}
	}

	private void DrawMap()
	{
		float xPos;
		float yPos = 0;

		PlayerController.Instance.xPos = mapSizeX / 2;
		PlayerController.Instance.yPos = 0;

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
			case TileType.Locked:
				prefab = prefabBedrock;
				spriteNum = 0;
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

		newTile = Instantiate(tile, new Vector3(xPos, yPos), Quaternion.identity, this.transform) as TileMap;
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
		if (x < 0 || y < 0 || x == tiles.GetLength(0) || y == tiles.GetLength(1))
		{
			TileMap tile = new TileMap();
			tile.tileType = TileType.Locked;
			return tile;
		}
		else
			return tiles[x, y];
	}
}
