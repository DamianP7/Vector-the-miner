using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	#region Instance
	private static SaveManager instance;
	public static SaveManager Instance
	{
		get
		{
			if (instance == null)
			{
				print("No SaveManager found on the scene!");
			}
			return instance;
		}
	}
	#endregion

	Save save;
	public bool loaded = false;
	[SerializeField] bool forceNewSave = false;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);

		if(!forceNewSave)
			LoadGame();
		else
			this.save = new Save();
	}

	private void Start()
	{
	}

	public Save GetSave()
	{
		return save;
	}

	public void SaveGame()
	{
		Save save = CreateSaveGameObject();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
		bf.Serialize(file, save);
		file.Close();

		Debug.Log("Game Saved");
	}

	private Save CreateSaveGameObject()
	{
		return save;
	}

	public void LoadGame()
	{
		Save save = GetSaveObject("/gamesave.save");
		if (save != null)
		{
			this.save = save;
			loaded = true;
			Debug.Log("Game Loaded");
		}
		else
		{
			this.save = new Save();
			Debug.Log("No game saved!");
		}
	}

	private Save GetSaveObject(string fileName)
	{
		Debug.Log(Application.persistentDataPath);
		if (File.Exists(Application.persistentDataPath + fileName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
			Save save = (Save)bf.Deserialize(file);
			file.Close();

			return save;
		}
		return null;
	}

	public TileMap[,] GetMap()
	{
		TileMap[,] tiles = new TileMap[save.map.GetLength(0), save.map.GetLength(1)];
		for (int i = 0; i < save.map.GetLength(0); i++)
		{
			for (int j = 0; j < save.map.GetLength(1); j++)
			{
				tiles[i, j] = new TileMap();
				tiles[i, j].tileType = save.map[i, j].tileType;
				tiles[i, j].spriteNumber = save.map[i, j].tileSprite;
				tiles[i, j].hardness = save.map[i, j].hardness;
			}

		}
		return tiles;
	}

	public void SaveMap()
	{
		TileMap[,] mapTiles = MapManager.Instance.SaveMap();

		save.map = new Tile[mapTiles.GetLength(0), mapTiles.GetLength(1)];
		
		for (int i = 0; i < mapTiles.GetLength(0); i++)
		{
			for (int j = 0; j < mapTiles.GetLength(1); j++)
			{
				save.map[i, j] = new Tile();
				save.map[i, j].tileType = mapTiles[i, j].tileType;
				save.map[i, j].tileSprite = mapTiles[i, j].spriteNumber;
				save.map[i, j].hardness = mapTiles[i, j].hardness;
			}

		}
		SaveGame();
	}

	private void LoadMap()
	{

	}
}
