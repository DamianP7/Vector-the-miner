using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Element
{
	public string itemName;
	public Direction[] directionsToMove;
	public Sprite sprite;
	public ItemType type;



}

public enum ItemType
{
	Rope, Torch, Support, Ladder
}