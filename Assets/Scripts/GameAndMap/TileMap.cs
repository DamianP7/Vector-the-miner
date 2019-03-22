using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField] TilesSettings tilesSettings;
	[SerializeField] SpriteRenderer spriteRenderer;
	public int spriteNumber;
	public TileType tileType;
	public float hardness;



	private void Start()
	{
		RefreshTile();
	}

	private void RefreshTile()
	{
		TilesSettings.TileSett tile = tilesSettings.GetTileSettings(tileType);
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