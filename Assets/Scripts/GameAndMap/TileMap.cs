using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField] TilesSettings tilesSettings;
	[SerializeField] ItemsSettings itemsSettings;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] SpriteRenderer extraSpriteRenderer;
	[SerializeField] TextMesh text;
	public int spriteNumber;
	public float hardness;
	public TileType tileType = TileType.Ground;
	public Ore ore = Ore.Ground;
	public int oreAmount = 0;
	public List<Element> elementsOnTile;

	private void Start()
	{
		RefreshTile();
		RefreshElements();
	}

	private void RefreshTile()
	{
		TilesSettings.TileSett tile;
		if (ore == Ore.Ground)
			tile = tilesSettings.GetTileSettings(tileType);
		else
		{
			tile = tilesSettings.GetOreSettings(ore);
		}

		hardness = tile.hardness;
		if (spriteNumber < tile.tileSprites.Length)
			spriteRenderer.sprite = tile.tileSprites[spriteNumber];
		else
		{
			if(tileType != TileType.Empty)
				Debug.LogError("Too big spriteNumber (" + transform.position + ": " + spriteNumber);
		}
	}

	public void RefreshElements()
	{
		if (elementsOnTile == null)
			return;

		Rope rope = GetElement(ItemType.Rope) as Rope;
		if (rope != null)
		{
			extraSpriteRenderer.sprite = itemsSettings.items.Find(x => x.type == ItemType.Rope).sprite;
			if (rope.isLast)
				text.text = rope.length.ToString();
			else
				text.text = "";
		}
	}

	public void Hit()
	{
		// tile breaking animation
		// particles ???
	}

	public void Dig()
	{
		// tile breaking animation
		// particles

		tileType = TileType.Hole;
		ore = Ore.Ground;
		RefreshTile();
	}

	public void Dig(Rope rope)
	{
		// tile breaking animation
		// particles

		tileType = TileType.Hole;
		ore = Ore.Ground;
		if (elementsOnTile == null)
			elementsOnTile = new List<Element>();
		elementsOnTile.Add(rope);
		RefreshTile();
		RefreshElements();
	}

	public void FallDown()	// colapse?
	{

	}

	public bool PlaceObject(Element element)
	{
		if (elementsOnTile == null)
			elementsOnTile = new List<Element>();
		else if (elementsOnTile.Find(x => x.type == element.type) != null ||
			tileType != TileType.Hole)
		{
			return false;
		}

		elementsOnTile.Add(element);
		extraSpriteRenderer.sprite = itemsSettings.items.Find(x => x.type == element.type).sprite;
		RefreshElements();
		return true;
	}

	public void PickUpObject()
	{

	}

	public Element GetElement(ItemType type)
	{
		if (elementsOnTile == null)
			return null;
		Element element = elementsOnTile.Find(x => x.type == type);
		if (element != null)
			return element;
		else
			return null;
	}

    public bool CheckElement(ItemType type)
    {
        if (elementsOnTile == null)
            return false;
        Element element = elementsOnTile.Find(x => x.type == type);
        if (element != null)
            return true;
        else
            return false;
    }

}

public enum TileType
{
	Surface,
	Ground,
	Empty,
	Building,
	Hole,
	Locked,
}

public enum Ore
{
	Ground,
	Iron,
	Copper,
	Lead,
	Silver,
	Gold,
	Platinum,
	Ruby,
	Sapphire,
	Emerald,
	Diamond,
	Coal
}