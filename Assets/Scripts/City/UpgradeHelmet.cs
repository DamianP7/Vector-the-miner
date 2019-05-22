using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Helmet")]
public class UpgradeHelmet : Upgrades
{
	public List<int> batteryDurability;

	public int GetCapacityOnLevel(int level)
	{
		return batteryDurability[level - 1];
	}
}
