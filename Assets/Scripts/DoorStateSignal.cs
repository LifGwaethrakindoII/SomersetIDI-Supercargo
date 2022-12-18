using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateSignal : MonoBehaviour
{
	[SerializeField] private Transform _armedSignal; 		/// <summary>Armed's Signal.</summary>
	[SerializeField] private Transform _disarmedSignal; 	/// <summary>Disarmed's Signal.</summary>
	[SerializeField] private Transform _signalWaypoint; 	/// <summary>Signal's Waypoint.</summary>

	/// <summary>Gets armedSignal property.</summary>
	public Transform armedSignal { get { return _armedSignal; } }

	/// <summary>Gets disarmedSignal property.</summary>
	public Transform disarmedSignal { get { return _disarmedSignal; } }

	/// <summary>Gets signalWaypoint property.</summary>
	public Transform signalWaypoint { get { return _signalWaypoint; } }

	private void Awake()
	{
		Arm();
	}

	public void Arm()
	{
		armedSignal.gameObject.SetActive(true);
		disarmedSignal.gameObject.SetActive(false);
		armedSignal.position = signalWaypoint.position;
	}

	public void Disarm()
	{
		armedSignal.gameObject.SetActive(false);
		disarmedSignal.gameObject.SetActive(true);
		disarmedSignal.position = signalWaypoint.position;
	}
}
