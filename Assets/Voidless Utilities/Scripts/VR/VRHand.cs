using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;

namespace VoidlessUtilities.VR
{
[RequireComponent(typeof(Body))]
public class VRHand : SteamVRDevice
{
	[Header("Pick's Attributes:")]
	[SerializeField] private LayerMask _pickableLayer; 	/// <summary>Pick Layer.</summary>
	[SerializeField] private Vector3 _pickZonePoint; 	/// <summary>Pick Zone's Point relative to this Hand's Transform.</summary>
	[SerializeField] private float _pickRadius; 		/// <summary>Pick Zone's Radius.</summary>
	[SerializeField] private float _force; 				/// <summary>Hand's Force.</summary>
	[Space(5f)]
	[Header("Input's Configurations:")]
	[SerializeField] private EVRButtonId _pickInput; 	/// <summary>Pick's Input.</summary>
	private VRPickable _pickable; 						/// <summary>Hand's Pickable.</summary>
	private Body _body; 								/// <summary>Body's Component.</summary>

#if UNITY_EDITOR
	[SerializeField] private Color color; 				/// <summary>Gizmos' Color.</summary>
#endif

#region Getters/Setters:
	/// <summary>Gets and Sets pickableLayer property.</summary>
	public LayerMask pickableLayer
	{
		get { return _pickableLayer; }
		set { _pickableLayer = value; }
	}

	/// <summary>Gets and Sets pickZonePoint property.</summary>
	public Vector3 pickZonePoint
	{
		get { return _pickZonePoint; }
		set { _pickZonePoint = value; }
	}

	/// <summary>Gets and Sets pickRadius property.</summary>
	public float pickRadius
	{
		get { return _pickRadius; }
		set { _pickRadius = value; }
	}

	/// <summary>Gets and Sets force property.</summary>
	public float force
	{
		get { return _force; }
		set { _force = value; }
	}

	/// <summary>Gets and Sets pickInput property.</summary>
	public EVRButtonId pickInput
	{
		get { return _pickInput; }
		set { _pickInput = value; }
	}

	/// <summary>Gets and Sets pickable property.</summary>
	public VRPickable pickable
	{
		get { return _pickable; }
		set { _pickable = value; }
	}

	/// <summary>Gets and Sets body Component.</summary>
	public Body body
	{ 
		get
		{
			if(_body == null)
			{
				_body = GetComponent<Body>();
			}
			return _body;
		}
	}
#endregion

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		Gizmos.color = color;
		Gizmos.DrawWireSphere(transform.TransformPoint(pickZonePoint), pickRadius);
#endif
	}

	private void Update()
	{
		TrackInput();
	}

	/// <summary>Tracks Controller's Inputs.</summary>
	protected virtual void TrackInput()
	{
		if(device.GetPressDown(pickInput) && pickable == null) OnPick();
		if(device.GetPressUp(pickInput) && pickable != null) OnDrop();
	}

	protected virtual void OnPick()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(pickZonePoint), pickRadius, pickableLayer);

		if(colliders.Length > 0)
		{
			foreach(Collider collider in colliders)
			{
				VRPickable currentPickable = collider.GetComponent<VRPickable>();
				if(currentPickable != null)
				{
					pickable = currentPickable;
					pickable.OnPickRequest(this);
					break;
				}
			}
		}
	}

	protected virtual void OnDrop()
	{
		pickable.OnDropRequest(this);
	}
}
}