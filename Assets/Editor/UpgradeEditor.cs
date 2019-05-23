using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Upgrades))]
public class UpgradeEditor : Editor
{
	protected Upgrades myTarget;
	protected List<bool> levelFolded;
	protected float SPRITE_HEIGHT = 80; // SPRITE HEIGHT
	protected bool oresToUpgrade = false;
	protected bool variables = false;
	protected int selected = 0;

	protected void OnEnable()
	{
		GetTarget();
		levelFolded = new List<bool>();
		for (int i = 0; i < myTarget.levels.Count; i++)
		{
			levelFolded.Add(false);
		}
		selected = myTarget.levels.Count - 1;
	}

	protected virtual void GetTarget()
	{
		myTarget = (Upgrades)target;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		myTarget.tool = (Tool)EditorGUILayout.EnumPopup("Tool", myTarget.tool);

		ShowLevels();

		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(target);
	}

	protected void ShowLevels()
	{
		EditorGUILayout.LabelField("Levels", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		int index = 0;
		// Show All
		foreach (Upgrade level in myTarget.levels)
		{
			ShowLevel(level, index);
			index++;
		}

		// Add new
		GUILayout.BeginHorizontal();
		GUILayout.Space(40);
		if (GUILayout.Button("Add", GUILayout.Width(70)))
		{
			AddNew();
		}

		// Delete choosen
		if (GUILayout.Button("Delete:", GUILayout.Width(70)))
		{
			DeleteAt(selected);
		}

		string[] levels = new string[myTarget.levels.Count];

		for (int i = 0; i < myTarget.levels.Count; i++)
		{
			levels[i] = myTarget.levels[i].name;
		}
		selected = EditorGUILayout.Popup(selected, levels);

		GUILayout.EndHorizontal();

		EditorGUI.indentLevel--;
	}

	protected void ShowLevel(Upgrade level, int index)
	{
		EditorGUI.indentLevel++;
		levelFolded[index] = EditorGUILayout.Foldout(levelFolded[index], level.name, true);
		if (levelFolded[index])
		{
			EditorGUI.indentLevel++;

			level.name = EditorGUILayout.TextField("Name: ", level.name);
			level.level = index + 1;
			EditorGUILayout.LabelField("Level: " + level.level);
			level.icon = SpriteObjectField.SpriteFieldHeight(level.icon, SPRITE_HEIGHT);
			level.tool = myTarget.tool;

			variables = EditorGUILayout.Foldout(variables, "Variables", true);
			if (variables)
			{
				ShowUniqueVariables(index);
			}

			if (index > 0)
			{
				GUILayout.Space(3);
				ShowUpgradeCost(level, index);
			}
			EditorGUI.indentLevel--;
		}
		EditorGUI.indentLevel--;
	}

	protected virtual void ShowUniqueVariables(int index)
	{

	}

	protected void ShowUpgradeCost(Upgrade level, int index)
	{
		level.cost = EditorGUILayout.IntField("Cost ($): ", level.cost);

		oresToUpgrade = EditorGUILayout.Foldout(oresToUpgrade, "Ores to upgrade", true);
		if (oresToUpgrade)
		{
			EditorGUI.indentLevel++;
			int oreIndex = 0;
			foreach (Upgrade.UpgradeCost ore in level.oresToUpgrade)
			{
				EditorGUILayout.LabelField("Ore #" + ++oreIndex, EditorStyles.boldLabel);
				ore.ore = (Ore)EditorGUILayout.EnumPopup("Ore", ore.ore);
				ore.maxQuantity = EditorGUILayout.IntField("Quantity: ", ore.maxQuantity);
				if (ore.maxQuantity < 0)
					ore.maxQuantity = 0;
				ore.quantity = 0;
				GUILayout.Space(2);
			}

			GUILayout.BeginHorizontal();
			GUILayout.Space(130);
			if (GUILayout.Button("+", GUILayout.Width(50)) && level.oresToUpgrade.Count < 3)
			{
				level.oresToUpgrade.Add(new Upgrade.UpgradeCost());
			}

			if (GUILayout.Button("-", GUILayout.Width(50)) && level.oresToUpgrade.Count > 0)
			{
				level.oresToUpgrade.RemoveAt(level.oresToUpgrade.Count - 1);
			}
			GUILayout.EndHorizontal();

			EditorGUI.indentLevel--;
		}
	}

	protected void AddNew()
	{
		myTarget.levels.Add(new Upgrade());
		AddUniqueVariables();
		levelFolded.Add(true);
		selected = myTarget.levels.Count - 1;
	}

	protected void DeleteAt(int index)
	{
		myTarget.levels.RemoveAt(index);
		levelFolded.RemoveAt(index);
		selected = myTarget.levels.Count - 1;
	}

	protected virtual void AddUniqueVariables()
	{

	}
}
