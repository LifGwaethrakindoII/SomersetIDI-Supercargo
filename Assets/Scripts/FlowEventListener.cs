using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercargo;
using UnityEngine.Events;

public class FlowEventListener : MonoBehaviour
{
	[SerializeField] private int _expectedID; 							/// <summary>Expected ID.</summary>
	[Space(5f)]
	[Header("Events:")]
	[SerializeField] private UnityEvent _onAwake; 						/// <summary>Event invoken when this GameObject awakes.</summary>
	[SerializeField] private UnityEvent _onEventWithExpectedIDInvoked; 	/// <summary>Event invoked wuen Event with expected ID is invoken.</summary>

	/// <summary>Gets expectedID property.</summary>
	public int expectedID { get { return _expectedID; } }

	/// <summary>Gets onEventWithExpectedIDInvoked property.</summary>
	public UnityEvent onEventWithExpectedIDInvoked { get { return _onEventWithExpectedIDInvoked; } }

	/// <summary>Gets onAwake property.</summary>
	public UnityEvent onAwake { get { return _onAwake; } }

	private void Awake()
	{
		onAwake.Invoke();
		FlowEventTriggerer.onEventTriggered += EvaluateEvent;
	}

	private void OnDestroy()
	{
		FlowEventTriggerer.onEventTriggered -= EvaluateEvent;
	}

	private void EvaluateEvent(int _ID)
	{
		if(_ID == expectedID) onEventWithExpectedIDInvoked.Invoke();
	}
}
