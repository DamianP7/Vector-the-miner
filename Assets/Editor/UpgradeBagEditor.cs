using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeBag))]
public class UpgradeBagEditor : UpgradeEditor
{

	protected override void GetTarget()
	{
		myTarget = (UpgradeBag)target;
	}

	protected override void ShowUniqueVariables(int index)
	{
		UpgradeBag obj = (UpgradeBag)myTarget;
		obj.capacity[index] = EditorGUILayout.IntField("Capacity: ", obj.capacity[index]);
	}

	protected override void AddUniqueVariables()
	{
		UpgradeBag obj = (UpgradeBag)myTarget;
		obj.capacity.Add(0);
	}
}
