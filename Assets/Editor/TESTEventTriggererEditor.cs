using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Supercargo
{
[CustomEditor(typeof(TESTEventTriggerer))]
public class TESTEventTriggererEditor : Editor
{
	private TESTEventTriggerer eventTriggerer;

	private void OnEnable()
	{
		eventTriggerer = target as TESTEventTriggerer;
	}

	public override void OnInspectorGUI()
	{
		if(GUILayout.Button("Invoke Event")) eventTriggerer.TriggerEvent();
	}
}
}