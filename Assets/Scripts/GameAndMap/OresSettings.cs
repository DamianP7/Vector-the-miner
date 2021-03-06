﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Ores Settings")]
public class OresSettings : ScriptableObject
{
	public List<OreGroup> ores;

	public Sprite GetSprite(Ore ore)
	{
		return ores.Find(x => x.oreType == ore).icon;
	}

	[System.Serializable]
	public class OreGroup
	{
		public string ore;
		public Ore oreType;
		public Sprite icon;
		public int minOnMap, maxOnMap;
		public OreSettings smallGroup, mediumGroup, largeGroup;
	}
}

[System.Serializable]
public class OreSettings
{
	public int minimumDepth, maximumDepth, minGroupSize, maxGroupSize, minChance, maxChance, oreAmount;
}