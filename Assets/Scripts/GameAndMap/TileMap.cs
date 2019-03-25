using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField] TilesSettings tilesSettings;
	[SerializeField] SpriteRenderer spriteRenderer;
	public int spriteNumber;
	public float hardness;
	public TileType tileType = TileType.Ground;
	public Ore ore = Ore.Ground;
	public int oreAmount = 0;

	private void Start()
	{
		RefreshTile();
	}

	private void RefreshTile()
	{
		TilesSettings.TileSett tile;
		if (ore == Ore.Ground)
			tile = tilesSettings.GetTileSettings(tileType);
		else
		{
			Debug.Log("my ore: " + ore.ToString());
			tile = tilesSettings.GetOreSettings(ore);
		}

		hardness = tile.hardness;
		if (spriteNumber < tile.tileSprites.Length)
			spriteRenderer.sprite = tile.tileSprites[spriteNumber];
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

	public void PlaceObject()
	{

	}

	public void PickUpObject()
	{

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