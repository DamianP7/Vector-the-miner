using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class OreStoreWindowController : ScrollingListButtons<OreInBag, OreStoreRecord>
{
	[SerializeField] OresSettings oresSettings;
	[SerializeField] OreStore oreStore;
    [SerializeField] GameObject emptyBagText;

	private void Start()
	{
		listOfItems = Player.Instance.bag.GetOres();
		UpdatePrices();
	}

	
	void UpdatePrices()
	{
		foreach (OreStoreRecord item in itemSlots)
		{
			item.Initialize(oreStore, oresSettings, this);
		}
	}

	public new void ShowItems()
	{
        listOfItems = Player.Instance.bag.GetOres();

        if (listOfItems.Count == 0)
            emptyBagText.SetActive(true);
        else
            emptyBagText.SetActive(false);

		base.ShowItems();
	}

	protected override void ShowItem(OreStoreRecord slot, OreInBag item)
	{
		slot.gameObject.SetActive(true);
		slot.SetOre(item.ore);
	}

	protected override void DisableSlot(OreStoreRecord slot)
	{
		slot.gameObject.SetActive(false);
	}

}