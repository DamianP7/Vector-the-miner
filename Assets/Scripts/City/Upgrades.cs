using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : ScriptableObject
{
	public Tool tool;
	public List<Upgrade> levels;

	public Upgrade GetUpgradeForLevel(int level)
	{
		return levels.Find(x => x.level == level);
	}
}

[System.Serializable]
public class Upgrade
{
	public string name;
	public int level;
	public Sprite icon;
	public int cost;
	//TODO: Custom editor -> hide in inspector
	public Tool tool;
	public List<UpgradeCost> oresToUpgrade;

	[System.Serializable]
	public class UpgradeCost
	{
		public Ore ore;
		//[HideInInspector]
		public int quantity;
		public int maxQuantity;
	}
}

public enum Tool
{
	Pickaxe,
	Bag,
	Helmet,
}