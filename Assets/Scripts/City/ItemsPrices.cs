using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemsPrices : ScriptableObject
{
    public List<ItemDescription> itemsPrices;

    [System.Serializable]
    public class ItemDescription
    {
        public ItemType item;
        public int price;
    }
}
