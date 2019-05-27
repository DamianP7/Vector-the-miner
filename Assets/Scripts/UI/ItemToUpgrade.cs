using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToUpgrade : MonoBehaviour
{
	[SerializeField] Text itemName, cost;
	[SerializeField] Image itemIcon;
	[SerializeField] Button upgradeButton;

	[SerializeField] OreToUpgrade[] ores;

	[SerializeField] OresSettings oreSettings;

	Warehouse warehouse;
	Upgrade itemLevel;
	Tool tool;
	int oreTypeQuantity;

	private void Start()
	{
		itemLevel = new Upgrade();
		itemLevel.level = 0;
	}

	int Cost
	{
		set
		{
			cost.text = '$' + value.ToString();
		}
	}

	public void SetupVariables(Warehouse warehouse)
	{
		this.warehouse = warehouse;
	}

	public void SetupItem(Upgrade upgrade)
	{
		itemLevel = upgrade;

		tool = itemLevel.tool;
		itemName.text = itemLevel.name;
		itemIcon.sprite = itemLevel.icon;
		Cost = itemLevel.cost;

		int index = 0;
		for (; index < itemLevel.oresToUpgrade.Count; index++)
		{
			ores[index].gameObject.SetActive(true);
			ores[index].SetupOre(itemLevel.oresToUpgrade[index].maxQuantity,
				oreSettings.GetSprite(itemLevel.oresToUpgrade[index].ore),
				itemLevel.oresToUpgrade[index].ore,
				this,
				itemLevel.oresToUpgrade[index].quantity);
			oreTypeQuantity = index;
		}

		for (; index < ores.Length; index++)
		{
			ores[index].gameObject.SetActive(false);
		}

		upgradeButton.interactable = CheckUpgradeButton();
	}

	public void GetOre(Ore ore)
	{
		warehouse.GetOre(tool, ore);
		upgradeButton.interactable = CheckUpgradeButton();
	}

	bool CheckUpgradeButton()
	{
		for (int i = 0; i <= oreTypeQuantity; i++)
		{
			if (!ores[i].CheckIfFull())
				return false;
		}

		if (Player.Instance.stats.Cash < itemLevel.cost)
			return false;
		else
			return true;
	}

	public void UpgradeTool()
	{
		Player.Instance.stats.Cash -= itemLevel.cost;
		SetupItem(warehouse.UpgradeTool(tool));
	}
}
