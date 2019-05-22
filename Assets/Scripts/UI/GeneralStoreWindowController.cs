using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class GeneralStoreWindowController : ScrollingListButtons<ItemOnList, ItemInStore>
{
	ItemsSettings itemsSettings;
	[SerializeField] GeneralStore generalStore;

	private void Start()
	{
		itemsSettings = generalStore.itemsSettings;
		listOfItems = itemsSettings.items;
		UpdatePrices();
	}


	void UpdatePrices()
	{
		foreach (ItemInStore item in itemSlots)
		{
			item.Initialize(generalStore, itemsSettings, this);
		}
	}

	public new void ShowItems()
	{
		base.ShowItems();
	}

	protected override void ShowItem(ItemInStore slot, ItemOnList item)
	{
		slot.gameObject.SetActive(true);
		slot.SetItem(item.type);
	}

	protected override void DisableSlot(ItemInStore slot)
	{
		slot.gameObject.SetActive(false);
	}

}