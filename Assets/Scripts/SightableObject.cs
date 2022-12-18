using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Supercargo;

[RequireComponent(typeof(FlowEventTriggerer))]
public class SightableObject : MonoBehaviour
{
	[SerializeField] private float _expectedSightedDuration; 	/// <summary>Expected Sighted Duration.</summary>
	[SerializeField] private UnityEvent _onSighted; 			/// <summary>Event Invoked when the object is sighted.</summary>
	private float _currentSightedDuration;
	private FlowEventTriggerer _eventTriggerer;

	/// <summary>Gets expectedSightedDuration property.</summary>
	public float expectedSightedDuration { get { return _expectedSightedDuration; } }

	/// <summary>Gets and Sets currentSightedDuration property.</summary>
	public float currentSightedDuration
	{
		get { return _currentSightedDuration; }
		set { _currentSightedDuration = value; }
	}

	/// <summary>Gets onSighted property.</summary>
	public UnityEvent onSighted { get { return _onSighted; } }

	/// <summary>Gets and Sets eventTriggerer Component.</summary>
	public FlowEventTriggerer eventTriggerer
	{ 
		get
		{
			if(_eventTriggerer == null)
			{
				_eventTriggerer = GetComponent<FlowEventTriggerer>();
			}
			return _eventTriggerer;
		}
	}

	private void Awake()
	{
		currentSightedDuration = 0.0f;
	}

	public void TickTimer()
	{
		//Debug.Log("[SightableObject] Ticking Timer...");
		currentSightedDuration += Time.deltaTime;
		if(currentSightedDuration >= expectedSightedDuration)
		{
			onSighted.Invoke();
			eventTriggerer.InvokeEvent();
		}
	}

	public void ResetTimer()
	{
		//Debug.Log("[SightableObject] Reseting Timer...");
		currentSightedDuration = 0.0f;
	}
}
