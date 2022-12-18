using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VoidlessUtilities;

namespace Supercargo
{
public enum TriggerEvent
{
	Enter = 1,
	Stay = 2,
	Exit = 4,

	EnterAndStay = Enter | Stay,
	EnterAndExit = Enter | Exit,
	ExitAndStay = Exit | Stay,
	All = Enter | Stay | Exit
}

public class ZoneTrigger : MonoBehaviour
{
	[SerializeField] private TriggerEvent _detectableEvents; 	/// <summary>Detectable Events.</summary>
	[SerializeField] private LayerMask _detectableMask; 		/// <summary>Detectable LayerMask.</summary>
	[Space(5f)]
	[Header("Events:")]
	[SerializeField] private UnityEvent _onEnter; 				/// <summary>OnEnter's Event.</summary>
	[SerializeField] private UnityEvent _onStay; 				/// <summary>OnStay's Event.</summary>
	[SerializeField] private UnityEvent _onExit; 				/// <summary>OnExit's Event.</summary>

	/// <summary>Gets and Sets detectableEvents property.</summary>
	public TriggerEvent detectableEvents
	{
		get { return _detectableEvents; }
		set { _detectableEvents = value; }
	}

	/// <summary>Gets and Sets detectableMask property.</summary>
	public LayerMask detectableMask
	{
		get { return _detectableMask; }
		set { _detectableMask = value; }
	}

	/// <summary>Gets onEnter property.</summary>
	public UnityEvent onEnter { get { return _onEnter; } }

	/// <summary>Gets onStay property.</summary>
	public UnityEvent onStay { get { return _onStay; } }

	/// <summary>Gets onExit property.</summary>
	public UnityEvent onExit { get { return _onExit; } }

	/// <summary>Event triggered when this Collider enters another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	private void OnTriggerEnter(Collider col)
	{
		if((detectableEvents | TriggerEvent.Enter) != detectableEvents) return;

		GameObject obj = col.gameObject;
		if(obj.IsOnLayerMask(detectableMask)/* || obj.tag == "Player"*/) onEnter.Invoke();
	}

	/// <summary>Event triggered when this Collider stays with another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	private void OnTriggerStay(Collider col)
	{
		if((detectableEvents | TriggerEvent.Stay) != detectableEvents) return;

		GameObject obj = col.gameObject;
		if(obj.IsOnLayerMask(detectableMask)/* || obj.tag == "Player"*/) onStay.Invoke();
	}

	private /// <summary>Event triggered when this Collider exits another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	void OnTriggerExit(Collider col)
	{
		if((detectableEvents | TriggerEvent.Exit) != detectableEvents) return;

		GameObject obj = col.gameObject;
		if(obj.IsOnLayerMask(detectableMask)/* || obj.tag == "Player"*/) onExit.Invoke();
	}
}
}