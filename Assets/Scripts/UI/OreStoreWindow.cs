using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class OreStoreWindow : GameWindows.Window
{
	[SerializeField] OreStoreWindowController windowController;

	public override void ShowWindow()
	{
		windowController.ShowItems();
		base.ShowWindow();
	}
}
