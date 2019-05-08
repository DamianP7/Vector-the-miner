using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemToPlace itemToPlace;

    [SerializeField] ItemsSettings itemsSettings;
    [SerializeField] List<CanvasItem> itemsOnBar;

    List<ItemSpritePair> icons;
    List<ItemInBag> items;
    int listOffset;

    private void Awake()
    {
        listOffset = 0;
    }

    void SetupItemIcons()
    {
        icons = new List<ItemSpritePair>();
        foreach (ItemOnList item in itemsSettings.items)
        {
            ItemSpritePair itemSprite = new ItemSpritePair();
            itemSprite.item = item.type;
            itemSprite.sprite = item.sprite;
            icons.Add(itemSprite);
        }
    }

    public void UpdateInventory(List<ItemInBag> itemList)
    {
        if (icons == null)
            SetupItemIcons();

        this.items = itemList;

        for (int i = 0; i < itemsOnBar.Count; i++)
        {
            if (i + listOffset < items.Count)
            {
                itemsOnBar[i].gameObject.SetActive(true);
                itemsOnBar[i].icon.sprite = icons.Find(x => x.item == items[i + listOffset].item).sprite;
                itemsOnBar[i].quantity.text = items[i + listOffset].quantity.ToString();

            }
            else
            {
                itemsOnBar[i].gameObject.SetActive(false);
            }

        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < itemsOnBar.Count; i++)
        {
            itemsOnBar[i].gameObject.SetActive(false);
        }
    }

    void MoveLeft()
    {
        listOffset = listOffset > 0 ? listOffset - 1 : 0;
    }

    void MoveRight()
    {
        listOffset = listOffset < items.Count - 1 ? listOffset + 1 : items.Count - 1;
    }

    public void UseItem(int slot)
    {
        ItemInBag item = items[slot + listOffset];
        Element element = new Element();
        element.type = item.item;
        element.sprite = icons.Find(x => x.item == item.item).sprite;
        itemToPlace.SpawnItem(element);
        Player.Instance.inventory.TakeItem(item.item);
    }

    [System.Serializable]
    class ItemSpritePair
    {
        public ItemType item;
        public Sprite sprite;
    }

    [System.Serializable]
    class CanvasItem
    {
        public GameObject gameObject;
        public Image icon;
        public Text quantity;
    }
}
