using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{
	Dictionary<Tool, int> levelsOfTools;
	UpgradeBag upgradeBag;
	UpgradeHelmet upgradeHelmet;
	UpgradePickaxe upgradePickaxe;

	public PlayerEquipment(UpgradeBag upgradeBag, UpgradeHelmet upgradeHelmet, UpgradePickaxe upgradePickaxe)
	{
		this.upgradeBag = upgradeBag;
		this.upgradeHelmet = upgradeHelmet;
		this.upgradePickaxe = upgradePickaxe;

		levelsOfTools = new Dictionary<Tool, int>();
		levelsOfTools.Add(Tool.Bag, 0);
		UpgradeTool(Tool.Bag);
		levelsOfTools.Add(Tool.Helmet, 0);
		UpgradeTool(Tool.Helmet);
		levelsOfTools.Add(Tool.Pickaxe, 0);
		UpgradeTool(Tool.Pickaxe);
	}

	public int GetToolLevel(Tool tool)
	{
		return levelsOfTools[tool];
	}

	public void UpgradeTool(Tool tool)
	{
		levelsOfTools[tool]++;

		int level = levelsOfTools[tool];

		switch (tool)
		{
			case Tool.Pickaxe:
				break;
			case Tool.Bag:
				Player.Instance.bag.UpgradeBag(upgradeBag.GetCapacityOnLevel(level));
				break;
			case Tool.Helmet:
				break;
		}
	}
}