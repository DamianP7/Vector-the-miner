using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
	[SerializeField] BagUI bagUI;

	[SerializeField] int maxCapacity, spaceLeft;

	List<OreInBag> ores;

	private void Awake()
	{
		spaceLeft = maxCapacity;
		ores = new List<OreInBag>();
		UpdateBag();
	}

	public void AddOre(Ore ore, int quantity = 1)
	{
		if (ores == null)
			ores = new List<OreInBag>();

		if (spaceLeft <= 0) // BAG IS FULL!!   TODO: add info
			return;
		else if (spaceLeft < quantity)
			quantity = spaceLeft;

		int index = ores.FindIndex(x => x.ore == ore);
		if (index != -1)
			ores[index].quantity += quantity;
		else
		{
			OreInBag newOre = new OreInBag();
			newOre.ore = ore;
			newOre.quantity = quantity;
			ores.Add(newOre);
		}
		spaceLeft -= quantity;
		UpdateBag();
	}

	public List<OreInBag> GetOres()
	{
		return ores;
	}

	public int GetOreAmount(Ore ore)
	{
		int index = ores.FindIndex(x => x.ore == ore);
		if (index != -1)
			return ores[index].quantity;
		else
			return 0;
	}

	public List<OreInBag> TakeOres()
	{
		List<OreInBag> temp = ores;
		ClearBag();
		return temp;
	}

	public int TakeOre(Ore ore)
	{
		int index = ores.FindIndex(x => x.ore == ore);
		int am;
		if (index != -1)
		{
			am = ores[index].quantity;
			ores.RemoveAt(index);
		}
		else
			am = 0;

		spaceLeft += am;

		return am;
	}

	public void ClearBag()
	{
		ores = new List<OreInBag>();
		spaceLeft = maxCapacity;
		UpdateBag();
	}

	private void UpdateBag()
	{
		if (ores.Count == 0)
			bagUI.UpdateBag(spaceLeft, maxCapacity);
		else
			bagUI.UpdateBag(spaceLeft, maxCapacity, ores);
	}
}
