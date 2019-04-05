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
	}

	public void RefreshElements()
	{
		if (elementsOnTile == null)
			return;

		Rope rope = GetElement(ElementType.Rope) as Rope;
		if (rope != null)
		{
			extraSpriteRenderer.sprite = itemsSettings.items.Find(x => x.type == ElementType.Rope).sprite;
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

	public void PlaceObject(Element element)
	{
		if (elementsOnTile == null)
			elementsOnTile = new List<Element>();

		elementsOnTile.Add(element);
		extraSpriteRenderer.sprite = itemsSettings.items.Find(x => x.type == ElementType.Rope).sprite;
	}

	public void PickUpObject()
	{

	}

	public Element GetElement(ElementType type)
	{
		if (elementsOnTile == null)
			return null;
		Element element = elementsOnTile.Find(x => x.type == type);
		if (element != null)
			return element;
		else
			return null;
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