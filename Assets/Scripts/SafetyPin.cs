using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoidlessUtilities;
using VoidlessUtilities.VR;

namespace Supercargo
{
[RequireComponent(typeof(FlowEventTriggerer))]
[RequireComponent(typeof(VRPickable))]
public class SafetyPin : MonoBehaviour
{
	[SerializeField] private LayerMask _entranceMask; 	/// <summary>Entrance LayerMask.</summary>
	[SerializeField] private LayerMask _exitMask; 		/// <summary>Exit LayerMask.</summary>
	[Space(5f)]
	[SerializeField] private int _entranceID; 			/// <summary>EntranceID.</summary>
	[SerializeField] private int _exitID; 				/// <summary>Exit ID.</summary>
	[Space(5f)]
	[SerializeField] private float _checkRadius; 		/// <summary>Check Radius.</summary>
	private FlowEventTriggerer _eventTriggerer;
	private VRPickable _pickable;

	/// <summary>Gets entranceMask property.</summary>
	public LayerMask entranceMask { get { return _entranceMask; } }

	/// <summary>Gets exitMask property.</summary>
	public LayerMask exitMask { get { return _exitMask; } }

	/// <summary>Gets entranceID property.</summary>
	public int entranceID { get { return _entranceID; } }

	/// <summary>Gets exitID property.</summary>
	public int exitID { get { return _exitID; } }

	/// <summary>Gets checkRadius property.</summary>
	public float checkRadius { get { return _checkRadius; } }

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

	/// <summary>Gets and Sets pickable Component.</summary>
	public VRPickable pickable
	{ 
		get
		{
			if(_pickable == null)
			{
				_pickable = GetComponent<VRPickable>();
			}
			return _pickable;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, checkRadius);
	}

	private void Awake()
	{
		pickable.onPicked += OnPicked;
		pickable.onDropped += OnDropped;
	}

	private void OnDestroy()
	{
		pickable.onPicked -= OnPicked;
		pickable.onDropped -= OnDropped;
	}

	private void OnPicked(VRPickable _pickable)
	{
		pickable.rigidbody.isKinematic = true;
		pickable.rigidbody.useGravity = false;
	}

	private void OnDropped(VRPickable _pickable)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, entranceMask);
		if(colliders.Length > 0 && colliders[0] != null)
		{
			Transform slot = colliders[0].transform;
			transform.position = slot.position;
			transform.rotation = slot.rotation;
			transform.parent = slot;
			eventTriggerer.eventID = entranceID;
			eventTriggerer.InvokeEvent();
			GetComponent<BoxCollider>().enabled = !true;
		}
		else
		{
			pickable.rigidbody.isKinematic = false;
			pickable.rigidbody.useGravity = true;
			GetComponent<BoxCollider>().enabled = true;
		}
	}

	/// <summary>Event triggered when this Collider exits another Collider trigger.</summary>
	/// <param name="col">The other Collider involved in this Event.</param>
	private void OnTriggerExit(Collider col)
	{
		GameObject obj = col.gameObject;
	
		if(obj.IsOnLayerMask(exitMask) || obj.tag == "Hole")
		{
			eventTriggerer.eventID = exitID;
			eventTriggerer.InvokeEvent();
		}
	}
}
}