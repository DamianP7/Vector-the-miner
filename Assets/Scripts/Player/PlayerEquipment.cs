using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{
	Dictionary<Tool, int> levelsOfTools;

	public PlayerEquipment()
	{
		levelsOfTools = new Dictionary<Tool, int>();
		levelsOfTools.Add(Tool.Bag, 1);
		levelsOfTools.Add(Tool.Helmet, 1);
		levelsOfTools.Add(Tool.Pickaxe, 1);
	}

	public int GetToolLevel(Tool tool)
	{
		return levelsOfTools[tool];
	}

	public void UpgradeTool(Tool tool)
	{
		levelsOfTools[tool]++;

		switch (tool)
		{
			case Tool.Pickaxe:
				break;
			case Tool.Bag:
				break;
			case Tool.Helmet:
				break;
		}
	}
}