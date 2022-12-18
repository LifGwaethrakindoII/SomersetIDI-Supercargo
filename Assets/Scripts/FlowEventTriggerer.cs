using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supercargo
{
public delegate void OnEventTriggered(int _eventID);

public class FlowEventTriggerer : MonoBehaviour
{
	public static event OnEventTriggered onEventTriggered;

	[SerializeField] private int _eventID; 	/// <summary>Event's ID.</summary>

	/// <summary>Gets and Sets eventID property.</summary>
	public int eventID
	{
		get { return _eventID; }
		set { _eventID = value; }
	}

	public void InvokeEvent()
	{
		if(onEventTriggered != null) onEventTriggered(eventID);
	}
}
}