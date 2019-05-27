using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OresPrices))]
public class OrePricesEditor : Editor
{
	OresPrices myTarget;
	protected List<bool> oreFolded;

	protected void OnEnable()
	{
		myTarget = (OresPrices)target;
		oreFolded = new List<bool>();
		for (int i = 0; i < myTarget.oresPrices.Count; i++)
		{
			oreFolded.Add(false);
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Ore Prices", EditorStyles.boldLabel);

		int index = 0;
		foreach (OresPrices.OreIntPair ore in myTarget.oresPrices)
		{
			ShowOre(ore, index);
			index++;
		}


		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(target);
	}

	void ShowOre(OresPrices.OreIntPair ore, int index)
	{
		oreFolded[index] = EditorGUILayout.Foldout(oreFolded[index], ore.ore.ToString(), true);

		if (oreFolded[index++])
		{
			ore.ore = (Ore)EditorGUILayout.EnumPopup("Ore:", ore.ore);
			ore.price = EditorGUILayout.IntField("Price ($):", ore.price);
			if (ore.price < 1)
				ore.price = 1;
		}
	}
}
