using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag
{
    [SerializeField]
    BagUI bagUI;

	int maxCapacity, spaceLeft;

	List<OreInBag> ores;

	public PlayerBag(BagUI bagUI, int maxCapacity)
	{
		this.bagUI = bagUI;
		this.maxCapacity = maxCapacity;
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

	public int TakeOre(Ore ore, int quantity = 1)
	{
		int index = ores.FindIndex(x => x.ore == ore);
		if (index != -1)
		{
			if (ores[index].quantity == quantity)
				ores.RemoveAt(index);
			else if (ores[index].quantity < quantity)
			{
				quantity = ores[index].quantity;
				ores.RemoveAt(index);
			}
			else
				ores[index].quantity -= quantity;
		}
		else
			quantity = 0;

		spaceLeft += quantity;

        UpdateBag();

        return quantity;
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

	public void LoadBag(List<OreInBag> ores)
	{
		this.ores = ores;
		//TOOD: count left space
	}
}
