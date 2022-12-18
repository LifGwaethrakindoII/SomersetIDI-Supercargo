using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Supercargo
{
[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
	private Door door;

	private void OnEnable()
	{
		door = target as Door;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if(door.inferiorLockData != null)
		{
			if(GUILayout.Button("Test Inferior's Lock Min. Point")) door.inferiorLockData.transform.localPosition = door.inferiorLockData.minPosition;
			if(GUILayout.Button("Test Inferior's Lock Max. Point")) door.inferiorLockData.transform.localPosition = door.inferiorLockData.maxPosition;
		} 		
	}
}
}