using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Building
{
	[SerializeField] List<Upgrades> upgrades;

	PlayerEquipment playerEquipment;
	PlayerBag playerBag;
	public Dictionary<Tool, Upgrade> currentUpgrades;

	private void Awake()
	{
		buildingAction = OpenWindow;
	}

	private void Start()
	{
		playerEquipment = Player.Instance.equipment;
		playerBag = Player.Instance.bag;
		currentUpgrades = GetListOfCurrentlUpgrades();
	}

	public void OpenWindow()
	{
		GameWindows.WindowsController.Instance.ShowWindow(
			GameWindows.WindowsController.Instance.warehouseWindow);
		InputManager.Instance.StopMovement();
	}

	/// <summary>
	/// Tworzy listę Upgrade'ów na odpowiednich levelach. Po jednym na każde narzędzie.
	/// </summary>
	public Dictionary<Tool, Upgrade> GetListOfCurrentlUpgrades()
	{
		Dictionary<Tool, Upgrade> upgradesList = new Dictionary<Tool, Upgrade>();

		foreach (Upgrades upgrade in upgrades)
		{
			Tool tool = upgrade.tool;
			int level = playerEquipment.GetToolLevel(tool);
			upgradesList.Add(tool, upgrade.GetUpgradeForLevel(level));
		}

		return upgradesList;
	}

	void UpdateUpgrade(Tool tool)
	{
		int level = playerEquipment.GetToolLevel(tool);
		currentUpgrades[tool] = upgrades.Find(x => x.tool == tool).GetUpgradeForLevel(level);
	}

	public bool GetOre(Tool tool, Ore ore)
	{
		playerBag.TakeOre(ore);
		currentUpgrades[tool].oresToUpgrade.Find(x => x.ore == ore).quantity++;
		if (currentUpgrades[tool].oresToUpgrade.Find(x => x.ore == ore).quantity == 0)
			return true;
		else
			return false;
	}


	public Upgrade UpgradeTool(Tool tool)
	{
		playerEquipment.UpgradeTool(tool);
		UpdateUpgrade(tool);
		return currentUpgrades[tool];
	}
}
