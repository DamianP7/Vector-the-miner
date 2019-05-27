using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemsPrices))]
public class ItemPricesEditor : Editor
{
	ItemsPrices myTarget;
	protected List<bool> itemsFolded;

	protected void OnEnable()
	{
		myTarget = (ItemsPrices)target;
		itemsFolded = new List<bool>();
		for (int i = 0; i < myTarget.itemsPrices.Count; i++)
		{
			itemsFolded.Add(false);
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Item Prices", EditorStyles.boldLabel);

		int index = 0;
		foreach (ItemsPrices.ItemDescription item in myTarget.itemsPrices)
		{
			ShowItem(item, index);
			index++;
		}


		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(target);
	}

	void ShowItem(ItemsPrices.ItemDescription item, int index)
	{
		itemsFolded[index] = EditorGUILayout.Foldout(itemsFolded[index], item.item.ToString(), true);

		if (itemsFolded[index++])
		{
			item.item = (ItemType)EditorGUILayout.EnumPopup("Item:", item.item);
			item.price = EditorGUILayout.IntField("Price ($):", item.price);
			if (item.price < 1)
				item.price = 1;
		}
	}
}
