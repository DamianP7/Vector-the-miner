using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeHelmet))]
public class UpgradeHelmetEditor : UpgradeEditor
{

	protected override void GetTarget()
	{
		myTarget = (UpgradeHelmet)target;
	}

	protected override void ShowUniqueVariables(int index)
	{
		UpgradeHelmet obj = (UpgradeHelmet)myTarget;
		obj.batteryDurability[index] = EditorGUILayout.IntField("Battery: ", obj.batteryDurability[index]);
	}

	protected override void AddUniqueVariables()
	{
		UpgradeHelmet obj = (UpgradeHelmet)myTarget;
		obj.batteryDurability.Add(0);
	}
}
