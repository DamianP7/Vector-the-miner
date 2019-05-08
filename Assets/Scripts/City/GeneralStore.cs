using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStore : Building
{
    [SerializeField] ItemsSettings itemsSettings;
    [SerializeField] ItemsPrices itemsPrices;

    PlayerStats playerStats;
    PlayerInventory playerInventory;

    private void Awake()
    {
        buildingAction = OpenWindow;
    }

    private void Start()
    {
        playerStats = Player.Instance.stats;
        playerInventory = Player.Instance.inventory;
    }

    public void OpenWindow()
    {
        GameWindows.WindowsController.Instance.ShowWindow(
            GameWindows.WindowsController.Instance.generalStoreWindow);
        InputManager.Instance.StopMovement();
    }

    public void BuyItem(ItemType item, int price, int quantity = 1)
    {
        int cost = price * quantity;
        playerStats.Cash -= cost;
        playerInventory.AddItem(item, quantity);
    }

    public int GetPrice(ItemType item)
    {
        return itemsPrices.itemsPrices.Find(x => x.item == item).price;
    }
}
