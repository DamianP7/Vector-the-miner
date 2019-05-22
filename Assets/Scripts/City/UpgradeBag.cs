using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Bag")]
public class UpgradeBag : Upgrades
{
	public List<int> capacity;

	public int GetCapacityOnLevel(int level)
	{
		return capacity[level];
	}
}
