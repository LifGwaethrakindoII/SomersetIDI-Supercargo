using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities.VR
{
[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour, IPickable
{
	private VRHand _hand;
	private PickableState _state; 							/// <summary>Pickable's Current State.</summary>
	private PickableState _previousState; 					/// <summary>Pickable's Current State.</summary>
	private Rigidbody _rigidbody; 							/// <summary>Rigidbody's Component.</summary>

#region Getters/Setters:
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
}
}