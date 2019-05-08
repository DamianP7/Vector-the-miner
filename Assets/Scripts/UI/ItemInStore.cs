using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInStore : MonoBehaviour
{
    public ItemType type;
    [SerializeField] Image icon;
    [SerializeField] Text itemNameText, quantityText, priceText;
    [SerializeField] Button buyButton;

    GeneralStore generalStore;
    ItemsSettings itemsSettings;
    GeneralStoreWindowController storeWindow;
    int quantity, price;

    int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
            priceText.text = '$' + price.ToString();
        }
    }

    public void Initialize(GeneralStore generalStore, ItemsSettings itemsSettings, GeneralStoreWindowController storeWindow)
    {
        this.generalStore = generalStore;
        this.itemsSettings = itemsSettings;
        this.storeWindow = storeWindow;
    }

    public void SetItem(ItemType item)
    {
        this.type = item;
        this.icon.sprite = itemsSettings.items.Find(x => x.type == item).icon;
        itemNameText.text = itemsSettings.items.Find(x => x.type == item).type.ToString();
        Price = generalStore.GetPrice(item);
        CheckIfAvailable();
    }

    public void BuyItem()
    {
        generalStore.BuyItem(type, price, quantity);
        storeWindow.ShowItems();
    }

    public bool CheckIfAvailable()
    {
        if (price <= Player.Instance.stats.Cash)
        {
            buyButton.interactable = true;
            return true;
        }
        else
        {
            buyButton.interactable = false;
            return false;
        }
    }
}
