using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Pickaxe")]
public class UpgradePickaxe : Upgrades
{
	public List<int> power;

	public int GetPowerOnLevel(int level)
	{
		return power[level];
	}
}
