using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{

	class OreToSpawn
	{
		public Ore ore;
		public OreSettings settings;
		public Dictionary<int, float> chancesPerRow;    // floor - chances
		public int groupSize;

		public void CalculateChances()
		{
			chancesPerRow = new Dictionary<int, float>();
			float midFloor = (settings.maximumDepth - settings.minimumDepth) / 2 + settings.minimumDepth;
			float chancePerFloor = (settings.maxChance - settings.minChance) / ((settings.maximumDepth - settings.minimumDepth) / 2);
			float actualChance = settings.minChance;

			for (int i = settings.minimumDepth; i <= settings.maximumDepth; i++)
			{
				if (i < midFloor)
				{
					chancesPerRow.Add(i, actualChance);
					actualChance += chancePerFloor;
				}
				else if (i > midFloor)
				{
					actualChance -= chancePerFloor;
					chancesPerRow.Add(i, actualChance);
				}
				else
				{
					actualChance = settings.maxChance;
					chancesPerRow.Add(i, actualChance);
				}
			}
		}
	}

	public class TileProject
	{
		public Ore ore;
		public int groupSize;
		public List<TileProject> group;

		public TileProject()
		{
			group = new List<TileProject>();
			group.Add(this);
		}

		public void UpdateList(TileProject newTile)
		{
			for (int i = 1; i < group.Count; i++)
			{
				group[i].group.Add(newTile);
			}
		}
	}

	List<OreToSpawn> waitingOres = new List<OreToSpawn>();
	List<OreToSpawn> readyOres = new List<OreToSpawn>();
	TileProject[,] tempTiles;

	public TileProject[,] GenerateOres(int mapSizeX, int mapSizeY, int surfaceLevel, int bedrockSize, OresSettings oresSettings, TileMap groundPrefab)
	{
		OreToSpawn ore;
		tempTiles = new TileProject[mapSizeX, mapSizeY];

		for (int i = 0; i < oresSettings.ores.Count; i++)
		{
			ore = new OreToSpawn();
			ore.ore = oresSettings.ores[i].oreType;
			ore.settings = oresSettings.ores[i].smallGroup;
			ore.groupSize = 0;
			ore.CalculateChances();
			waitingOres.Add(ore);
			ore = new OreToSpawn();
			ore.ore = oresSettings.ores[i].oreType;
			ore.settings = oresSettings.ores[i].mediumGroup;
			ore.groupSize = 1;
			ore.CalculateChances();
			waitingOres.Add(ore);
			ore = new OreToSpawn();
			ore.ore = oresSettings.ores[i].oreType;
			ore.settings = oresSettings.ores[i].largeGroup;
			ore.groupSize = 2;
			ore.CalculateChances();
			waitingOres.Add(ore);
		}

		for (int i = surfaceLevel + 1; i < mapSizeY - bedrockSize; i++)
		{
			CheckAvailability(i);
			for (int j = bedrockSize; j < mapSizeX - bedrockSize; j++)
			{
				for (int k = 0; k < readyOres.Count; k++)
				{
					if (Randomize(readyOres[k].chancesPerRow[i]) && tempTiles[j, i] == null)
					{
						SpawnGroup(j, i, readyOres[k]);
						break;
					}
				}
			}
		}
		return tempTiles;
	}

	TileMap[,] TileprojectToTilemap(TileProject[,] projects, TileMap prefab)
	{
		TileMap[,] tiles = new TileMap[projects.GetLength(0), projects.GetLength(1)];

		for (int i = 0; i < projects.GetLength(0); i++)
		{
			for (int j = 0; j < projects.GetLength(1); j++)
			{
				if (projects[i, j] != null)
				{
					tiles[i, j] = new TileMap();
					tiles[i, j].hardness = 0;
					//tiles[i, j] = prefab.;
					tiles[i, j].ore = projects[i, j].ore;
					tiles[i, j].spriteNumber = projects[i, j].groupSize;
					//tiles[i, j].oreAmount = projects[i, j].;
					tiles[i, j].tileType = TileType.Ground;
				}
				else
					tiles[i, j] = null;
			}
		}

		return tiles;
	}

	void SpawnGroup(int x, int y, OreToSpawn ore)
	{
		TileProject tile = new TileProject();
		tile.groupSize = ore.groupSize;
		tile.ore = ore.ore;
		tempTiles[x, y] = tile;
		int groupSize = Random.Range(ore.settings.minGroupSize, ore.settings.maxGroupSize + 1);
		int check = 0;
		List<int> dirs = RandomizeDir();
		// dodaj while który w przypadku dużych grup będzie umieszczał złoża z innych pól a nie tylko z pierwszego
		for (int i = 0; i < dirs.Count; i++)
		{
			if (dirs[i] == 0)
			{
				if (tempTiles[x + 1, y] == null)
				{
					tempTiles[x + 1, y] = tile;
					tempTiles[x + 1, y].groupSize = Random.Range(0, ore.groupSize + 1);
					tempTiles[x, y].UpdateList(tempTiles[x + 1, y]);
					tempTiles[x + 1, y].group = tempTiles[x, y].group;
					groupSize--;
					// dodaj w każdym ifie zliczanie wygenerowanych złóż żeby na końcu podliczyć ich ilość
					// i na bierząco kontrolować czy nie spawnuje sie ich za dużo
				}
			}
			else if (dirs[i] == 1)
			{
				if (tempTiles[x, y + 1] == null)
				{
					tempTiles[x, y + 1] = tile;
					tempTiles[x, y].UpdateList(tempTiles[x, y + 1]);
					tempTiles[x, y + 1].group = tempTiles[x, y].group;
					groupSize--;
				}
			}
			else if (dirs[i] == 2)
			{
				if (tempTiles[x - 1, y] == null)
				{
					tempTiles[x - 1, y] = tile;
					tempTiles[x, y].UpdateList(tempTiles[x - 1, y]);
					tempTiles[x - 1, y].group = tempTiles[x, y].group;
					groupSize--;
				}
			}
			else if (dirs[i] == 3)
			{
				if (tempTiles[x, y - 1] == null)
				{
					tempTiles[x, y - 1] = tile;
					tempTiles[x, y].UpdateList(tempTiles[x, y - 1]);
					tempTiles[x, y - 1].group = tempTiles[x, y].group;
					groupSize--;
				}
			}
			if (groupSize == 0)
				return;

			check++;
			if (check > 10)
				return;
		}
	}

	List<int> RandomizeDir()
	{
		List<int> list = new List<int>();
		List<int> start = new List<int>();
		start.Add(0);
		start.Add(1);
		start.Add(2);
		start.Add(3);
		while (start.Count > 0)
		{
			int num = Random.Range(0, start.Count);
			list.Add(start[num]);
			start.RemoveAt(num);
		}
		return list;
	}

	void CheckAvailability(int depth)
	{
		List<OreToSpawn> oresTemp = new List<OreToSpawn>();
		for (int i = 0; i < readyOres.Count; i++)
		{
			if (readyOres[i].settings.maximumDepth < depth)
				oresTemp.Add(readyOres[i]); // stay; else delete
		}
		readyOres = oresTemp;
		oresTemp = new List<OreToSpawn>();
		for (int i = 0; i < waitingOres.Count; i++)
		{
			if (depth >= waitingOres[i].settings.minimumDepth)
				readyOres.Add(waitingOres[i]);  // move to ready
			else
				oresTemp.Add(waitingOres[i]);   // wait
		}
		waitingOres = oresTemp;
	}

	bool Randomize(int winCondition, int min = 0, int max = 100)
	{
		if (Random.Range(min, max) < winCondition)
			return true;
		else
			return false;
	}

	bool Randomize(float winCondition, float min = 0, float max = 100)
	{
		if (Random.Range(min, max) < winCondition)
			return true;
		else
			return false;
	}
}
