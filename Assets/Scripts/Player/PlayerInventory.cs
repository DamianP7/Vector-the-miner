using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
	InventoryUI inventoryUI;

	List<ItemInBag> items;

	public PlayerInventory(InventoryUI inventoryUI)
	{
		this.inventoryUI = inventoryUI;
		StartItems();
	}

	private void StartItems()
	{
		items = new List<ItemInBag>();
		ItemInBag startItem = new ItemInBag();
		startItem.item = ItemType.Support;
		startItem.quantity = 2;
		items.Add(startItem);
        startItem = new ItemInBag();
        startItem.item = ItemType.Torch;
        startItem.quantity = 5;
        items.Add(startItem);
        startItem = new ItemInBag();
        startItem.item = ItemType.Ladder;
        startItem.quantity = 12;
        items.Add(startItem);
        UpdateInventory();
	}

	public void AddItem(ItemType item, int quantity = 1)
	{
		if (items == null)
			items = new List<ItemInBag>();

		int index = items.FindIndex(x => x.item == item);
		if (index != -1)
			items[index].quantity += quantity;
		else
		{
			ItemInBag newItem = new ItemInBag();
			newItem.item = item;
			newItem.quantity = quantity;
			items.Add(newItem);
		}
		UpdateInventory();
	}

	public List<ItemInBag> TakeItems()
	{
		List<ItemInBag> temp = items;
		ClearInventory();
		return temp;
	}

	public int TakeItem(ItemType item)
	{
		int index = items.FindIndex(x => x.item == item);
		if (index != -1)
		{
			if (items[index].quantity == 1)
			{
				items.RemoveAt(index);
				UpdateInventory();
				return 1;
			}
			else
			{
				items[index].quantity--;
				UpdateInventory();
				return items[index].quantity + 1;
			}
		}
		else
			return 0;
	}

	public void ClearInventory()
	{
		items = new List<ItemInBag>();
		UpdateInventory();
	}

	private void UpdateInventory()
	{
		if (items.Count == 0)
			inventoryUI.UpdateInventory();
		else
			inventoryUI.UpdateInventory(items);
	}
}
