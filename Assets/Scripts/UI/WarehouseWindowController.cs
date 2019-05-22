using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class WarehouseWindowController : ScrollingListButtons<Upgrade, ItemToUpgrade>
{
	[SerializeField] Warehouse warehouse;

	void UpdateList()
	{
		listOfItems = new List<Upgrade>();
		foreach (KeyValuePair<Tool, Upgrade> upgrade in warehouse.currentUpgrades)
		{
			listOfItems.Add(upgrade.Value);
		}
	}

	public new void ShowItems()
	{
		UpdateList();
		base.ShowItems();
	}

	protected override void ShowItem(ItemToUpgrade slot, Upgrade item)
	{
		slot.gameObject.SetActive(true);
		slot.SetupVariables(warehouse);
		slot.SetupItem(item);
	}

	protected override void DisableSlot(ItemToUpgrade slot)
	{
		slot.gameObject.SetActive(false);
	}
}
