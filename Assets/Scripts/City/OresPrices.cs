using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OresPrices : ScriptableObject
{
	public List<OreIntPair> oresPrices;

	[System.Serializable]
	public class OreIntPair
	{
		public Ore ore;
		public int price;
	}
}
