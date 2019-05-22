using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class WarehouseWindow : GameWindows.Window
{
	[SerializeField] WarehouseWindowController windowController;

	public override void ShowWindow()
	{
		windowController.ShowItems();
		base.ShowWindow();
	}
}
