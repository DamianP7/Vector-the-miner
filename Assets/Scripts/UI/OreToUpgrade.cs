using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreToUpgrade : MonoBehaviour
{
	[SerializeField] Text quantityText;
	[SerializeField] Image icon;
	[SerializeField] RectTransform rectBar;
	[SerializeField] Button button;

	int quantity, quantityMax;
	Ore ore;
	ItemToUpgrade itemUpgradeController;

	int Quantity
	{
		get
		{
			return quantity;
		}
		set
		{
			quantity = value;
			quantityText.text = quantity.ToString() + '/' + quantityMax.ToString();
			rectBar.sizeDelta = new Vector2(200 * ((float)quantity / quantityMax), rectBar.sizeDelta.y);
		}
	}

	public void UpdateOre()
	{
		if (Player.Instance.bag.GetOreAmount(ore) > 0 && !CheckIfFull())
			button.interactable = true;
		else
			button.interactable = false;
	}

	public void SetupOre(int neededAmount, Sprite icon, Ore ore, ItemToUpgrade itemToUpgrade, int currentAmount = 0)
	{
		itemUpgradeController = itemToUpgrade;

		quantityMax = neededAmount;
		Quantity = currentAmount;
		this.icon.sprite = icon;
		this.ore = ore;

		UpdateOre();
	}

	public void GiveOre()
	{
		Quantity++;
		itemUpgradeController.GetOre(ore);
		UpdateOre();
	}

	public bool CheckIfFull()
	{
		if (quantity < quantityMax)
			return false;
		else
			return true;
	}
}
