using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OresSettings : ScriptableObject
{
	public List<OreGroup> ores;

	[System.Serializable]
	public class OreGroup
	{
		[SerializeField]
		string ore;
		public Ore oreType;
		public int minOnMap, maxOnMap;
		public OreSettings smallGroup, mediumGroup, largeGroup;
	}

}

[System.Serializable]
public class OreSettings
{
	public int minimumDepth, maximumDepth, minGroupSize, maxGroupSize, minChance, maxChance, oreAmount;
}