using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : ScriptableObject
{
	public Tool tool = Tool.Bag;
	public List<Upgrade> levels = new List<Upgrade>();

	public Upgrade GetUpgradeForLevel(int level)
	{
		return levels.Find(x => x.level == level);
	}
}

[System.Serializable]
public class Upgrade
{
	public string name = "NewUpgrade";
	public int level = 0;
	public Sprite icon;
	public int cost = 0;
	public Tool tool = Tool.Bag;
	public List<UpgradeCost> oresToUpgrade = new List<UpgradeCost>();

	[System.Serializable]
	public class UpgradeCost
	{
		public Ore ore = Ore.Coal;
		//[HideInInspector]
		public int quantity = 0;
		public int maxQuantity = 0;
	}
}

public enum Tool
{
	Pickaxe,
	Bag,
	Helmet,
}