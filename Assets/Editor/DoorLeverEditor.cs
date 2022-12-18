using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Supercargo
{
[CustomEditor(typeof(DoorLever))]
public class DoorLeverEditor : Editor
{
	private DoorLever doorLever;

	void OnEnable()
	{
		doorLever = target as DoorLever;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Test Min. rotation"))
		{
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = doorLever.minRotation;
			doorLever.transform.rotation = rotation;
		}
		if(GUILayout.Button("Test Max. rotation"))
		{
			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = doorLever.maxRotation;
			doorLever.transform.rotation = rotation;
		}
	}
}
}