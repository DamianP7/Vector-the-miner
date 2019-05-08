using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStore : Building
{
	[SerializeField] OreStoreWindowController storeWindow;

	[SerializeField] OresPrices oresPrices;

	PlayerBag playerBag;

    private void Awake()
    {
        buildingAction = OpenWindow;
    }

    private void Start()
	{
		playerBag = Player.Instance.bag;
    }

	public void OpenWindow()
	{
        GameWindows.WindowsController.Instance.ShowWindow(
            GameWindows.WindowsController.Instance.oreSoreWindow);
		InputManager.Instance.StopMovement();
	}

	public int GetQuantity(Ore ore)
	{
		return playerBag.GetOreAmount(ore);
	}

	/// <summary>
	/// Returns how many ores actualy sold.
	/// </summary>
	public int SellOres(Ore ore, int quantity, int price)
	{
		int sold = playerBag.TakeOre(ore, quantity);
		Player.Instance.stats.Cash += price * sold;
        return sold;
	}

	public int GetPrice(Ore ore)
	{
		return oresPrices.oresPrices.Find(x => x.ore == ore).price;
	}
}
