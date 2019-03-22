using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
	[SerializeField] TilesSettings tilesSettings;
	public int spriteNumber;
	public TileType tileType;
	public float hardness;
	

	private void Start()
	{
		TilesSettings.TileSett tile = tilesSettings.GetTileSettings(tileType);
		hardness = tile.hardness;


		GetComponent<SpriteRenderer>().sprite = tilesSettings.GetSprite(tileType, spriteNumber);
	}
}

public enum TileType
{
	Surface,
	Ground,
	Empty,
	Building,
	Rope,
	Hole
}