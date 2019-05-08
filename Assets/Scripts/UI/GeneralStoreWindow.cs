using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollingList;

public class GeneralStoreWindow : GameWindows.Window
{
	[SerializeField] GeneralStoreWindowController windowController;

	public override void ShowWindow()
	{
		windowController.ShowItems();
		base.ShowWindow();
	}
}
