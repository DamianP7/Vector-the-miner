using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradePickaxe))]
public class UpgradePickaxeEditor : UpgradeEditor
{

	protected override void GetTarget()
	{
		myTarget = (UpgradePickaxe)target;
	}

	protected override void ShowUniqueVariables(int index)
	{
		UpgradePickaxe obj = (UpgradePickaxe)myTarget;
		obj.power[index] = EditorGUILayout.IntField("Power: ", obj.power[index]);
	}

	protected override void AddUniqueVariables()
	{
		UpgradePickaxe obj = (UpgradePickaxe)myTarget;
		obj.power.Add(0);
	}
}
