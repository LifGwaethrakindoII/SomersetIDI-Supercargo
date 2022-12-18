using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VoidlessUtilities.VR
{
public delegate void OnPicked(VRPickable _pickable);
public delegate void OnDropped(VRPickable _pickable);
/*public delegate void OnPicked();
public delegate void OnDropped();*/

[RequireComponent(typeof(Rigidbody))]
public class VRPickable : MonoBehaviour, IPickable
{
	public event OnPicked onPicked;
	public event OnDropped onDropped;

	[SerializeField] private bool _parentToHand; 			/// <summary>Parent this pickable to hand?.</summary>
	[Space(5f)]
	[Header("Events:")]
	[SerializeField] private UnityEvent _onPickedEvent; 	/// <summary>OnPicked's Event.</summary>
	[SerializeField] private UnityEvent _onDroppedEvent; 	/// <summary>OnDropped's Event.</summary>
	private VRHand _hand;
	private PickableState _state; 							/// <summary>Pickable's Current State.</summary>
	private PickableState _previousState; 					/// <summary>Pickable's Current State.</summary>
	private Rigidbody _rigidbody; 							/// <summary>Rigidbody's Component.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets parentToHand property.</summary>
	public bool parentToHand
	{
		get { return _parentToHand; }
		set { _parentToHand = value; }
	}

	/// <summary>Gets onPickedEvent property.</summary>
	public UnityEvent onPickedEvent { get { return _onPickedEvent; } }

	/// <summary>Gets onDroppedEvent property.</summary>
	public UnityEvent onDroppedEvent { get { return _onDroppedEvent; } }

	/// <summary>Gets and Sets hand property.</summary>
	public VRHand hand
	{
		get { return _hand; }
		set { _hand = value; }
	}

	/// <summary>Gets and Sets state property.</summary>
	public PickableState state
	{
		get { return _state; }
		set { _state = value; }
	}

	/// <summary>Gets and Sets previousState property.</summary>
	public PickableState previousState
	{
		get { return _previousState; }
		set { _previousState = value; }
	}

	/// <summary>Gets and Sets rigidbody Component.</summary>
	public Rigidbody rigidbody
	{ 
		get
		{
			if(_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}
			return _rigidbody;
		}
	}
#endregion

#region FiniteStateMachine:
	/// <summary>Enters PickableState State.</summary>
	/// <param name="_state">PickableState State that will be entered.</param>
	public virtual void OnEnter(PickableState _state) { /*...*/ }
	
	/// <summary>Leaves PickableState State.</summary>
	/// <param name="_state">PickableState State that will be left.</param>
	public virtual void OnExit(PickableState _state) { /*...*/ }
#endregion

	private void Awake()
	{
		hand = null;
	}

	/// <summary>Callback invoked when a pick request is made by a VRHand.</summary>
	/// <param name="_hand">VRHand requesting the pick.</param>
	public virtual void OnPickRequest(VRHand _hand)
	{
		AcceptDropRequest(_hand);
		AcceptPickRequest(_hand);
	}

	/// <summary>Callback invoked when a drop request is made by a VRHand.</summary>
	/// <param name="_hand">VRHand requesting the drop.</param>
	public virtual void OnDropRequest(VRHand _hand)
	{
		AcceptDropRequest(_hand);
	}

	protected virtual void AcceptPickRequest(VRHand _hand)
	{
		hand = _hand;
		hand.pickable = this;
		if(parentToHand) transform.parent = _hand.transform;

		if(onPicked != null) onPicked(this);
		onPickedEvent.Invoke();
	}

	protected virtual void AcceptDropRequest(VRHand _hand)
	{
		if(hand != null)
		{
			hand.pickable = null;
			hand = null;
			if(parentToHand) transform.parent = null;

			if(onDropped != null) onDropped(this);
			onDroppedEvent.Invoke();
		}
	}
}
}