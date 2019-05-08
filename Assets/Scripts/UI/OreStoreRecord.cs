using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreStoreRecord : MonoBehaviour
{
	public Ore ore;
	[SerializeField] Image icon;
	[SerializeField] Text oreNameText, quantityText, priceText;
	[SerializeField] Button sellOne, sellTen, sellAll;

	OreStore oreStore;
	OresSettings oresSettings;
	OreStoreWindowController storeWindow;
	int quantity, price;

	int Quantity
	{
		get
		{
			return quantity;
		}
		set
		{
			quantity = value;
			if (quantity > 0)
			{
				sellOne.interactable = true;
				sellAll.interactable = true;
			}
			else
			{
				sellOne.interactable = false;
				sellAll.interactable = false;
			}

			if (quantity >= 10)
			{
				sellTen.interactable = true;
			}
			else
			{
				sellTen.interactable = false;
			}
			quantityText.text = quantity.ToString();
		}
	}

	int Price
	{
		get
		{
			return price;
		}
		set
		{
			price = value;
			priceText.text = '$' + price.ToString();
		}
	}

	public void Initialize(OreStore oreStore, OresSettings oresSettings, OreStoreWindowController storeWindow)
	{
		this.oreStore = oreStore;
		this.oresSettings = oresSettings;
        this.storeWindow = storeWindow;
    }

	public void SetOre(Ore ore)
	{
		this.ore = ore;
		this.icon.sprite = oresSettings.ores.Find(x => x.oreType == ore).icon;
		oreNameText.text = oresSettings.ores.Find(x => x.oreType == ore).ore;
		Price = oreStore.GetPrice(ore);
		Quantity = oreStore.GetQuantity(ore);
	}

	public void SellOre(int quantity)
	{
		Quantity = oreStore.SellOres(ore, quantity, price);
		storeWindow.ShowItems();
	}
}
