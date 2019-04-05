using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemsSettings : ScriptableObject
{
	public List<ItemOnList> items;
}

[System.Serializable]
public class ItemOnList
{
	public ElementType type;
	public Sprite sprite;
}