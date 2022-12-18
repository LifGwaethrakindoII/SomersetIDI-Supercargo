using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace VoidlessUtilities.VR
{
public enum Controllers
{
	None = 0,
	Left = 1,
	Right = 2,
	Both = Left | Right
}

[RequireComponent(typeof(VRHead))]
[RequireComponent(typeof(CharacterController))]
public class VRCharacterControllerDisplacement : MonoBehaviour
{
	[Header("Speed's Attributes:")]
	[SerializeField] private float _speed; 								/// <summary>Displacement's Speed.</summary>
	[Range(1.0f, 10.0f)]
	[SerializeField] private float _additionalSpeedMultiplier; 			/// <summary>Additional Displacement's Speed Multiplier.</summary>
	[Range(0.0f, 1.0f)]
	[SerializeField] private float _sidewaysDisplacementMultiplier; 	/// <summary>Sideways Displacement's Multippler [Relative to the displacement's speed].</summary>
	[Range(0.0f, 1.0f)]
	[SerializeField] private float _backwardsDisplacementMultiplier; 	/// <summary>Backwards Displacement's Multiplier [Relative to the displacement's speed].</summary>
	[Space(5f)]
	[Header("Inputs' Configurations:")]
	[SerializeField] private Controllers _detectableControllers; 		/// <summary>Detectable Controllers.</summary>
	[SerializeField] private EVRButtonId _displacementAxis; 			/// <summary>Displacement's Axis.</summary>
	[SerializeField] private EVRButtonId _displacementInput; 			/// <summary>DisplacementInput.</summary>
	[SerializeField] private EVRButtonId _additionalSpeedInput; 		/// <summary>Backwards Displacement Input.</summary>
	private VRHead _head; 												/// <summary>VRHead's Component.</summary>
	private CharacterController _characterController; 					/// <summary>CharacterController's Component.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets speed property.</summary>
	public float speed
	{
		get { return _speed; }
		set { _speed = value; }
	}

	/// <summary>Gets and Sets additionalSpeedMultiplier property.</summary>
	public float additionalSpeedMultiplier
	{
		get { return _additionalSpeedMultiplier; }
		set { _additionalSpeedMultiplier = value; }
	}

	/// <summary>Gets and Sets sidewaysDisplacementMultiplier property.</summary>
	public float sidewaysDisplacementMultiplier
	{
		get { return _sidewaysDisplacementMultiplier; }
		set { _sidewaysDisplacementMultiplier = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>Gets and Sets backwardsDisplacementMultiplier property.</summary>
	public float backwardsDisplacementMultiplier
	{
		get { return _backwardsDisplacementMultiplier; }
		set { _backwardsDisplacementMultiplier = Mathf.Clamp(value, 0.0f, 1.0f); }
	}

	/// <summary>Gets and Sets detectableControllers property.</summary>
	public Controllers detectableControllers
	{
		get { return _detectableControllers; }
		set { _detectableControllers = value; }
	}

	/// <summary>Gets and Sets displacementAxis property.</summary>
	public EVRButtonId displacementAxis
	{
		get { return _displacementAxis; }
		set { _displacementAxis = value; }
	}

	/// <summary>Gets and Sets displacementInput property.</summary>
	public EVRButtonId displacementInput
	{
		get { return _displacementInput; }
		set { _displacementInput = value; }
	}

	/// <summary>Gets and Sets additionalSpeedInput property.</summary>
	public EVRButtonId additionalSpeedInput
	{
		get { return _additionalSpeedInput; }
		set { _additionalSpeedInput = value; }
	}

	/// <summary>Gets and Sets head Component.</summary>
	public VRHead head
	{ 
		get
		{
			if(_head == null)
			{
				_head = GetComponent<VRHead>();
			}
			return _head;
		}
	}

	/// <summary>Gets and Sets characterController Component.</summary>
	public CharacterController characterController
	{ 
		get
		{
			if(_characterController == null)
			{
				_characterController = GetComponent<CharacterController>();
			}
			return _characterController;
		}
	}
#endregion

	private void Update()
	{
		TrackControllers();
		ApplyGravity();
	}

	/// <summary>Tracks User's Controllers.</summary>
	private void TrackControllers()
	{
		bool detectLeftController = ((detectableControllers & Controllers.Left) == Controllers.Left);
		bool detectRightController = ((detectableControllers & Controllers.Right) == Controllers.Right);
		Vector2 axis = Vector2.zero;
		Vector3 displacement = Vector3.zero;
		float speedMultiplier = (detectLeftController && head.leftHand.device.GetPress(additionalSpeedInput)) || (detectRightController && head.rightHand.device.GetPress(additionalSpeedInput)) ? additionalSpeedMultiplier : 1.0f;

		if(detectLeftController) axis += head.leftHand.device.GetAxis(displacementAxis);
		if(detectRightController) axis += head.rightHand.device.GetAxis(displacementAxis);
		if(axis.sqrMagnitude > 1.0f) axis.Normalize();
		
		axis.x *= (speed * speedMultiplier * sidewaysDisplacementMultiplier);
		axis.y *= (speed * speedMultiplier * (axis.y > 0.0f ? 1.0f : backwardsDisplacementMultiplier));
		displacement = new Vector3(axis.x, 0.0f, axis.y);

		if((detectLeftController && head.leftHand.device.GetPress(displacementInput)) || (detectRightController && head.rightHand.device.GetPress(displacementInput)))
		Displace(head.fixedEye.TransformDirection(displacement));
	}

	/// <summary>Displaces the Character.</summary>
	/// <param name="_displacement">Displacement Vector.</param>
	/// <param name="_space">Space Relativeness [World as default].</param>
	/// <returns>True if the Character is grounded.</returns>
	private bool Displace(Vector3 _displacement, Space _space = Space.World)
	{
		if(_space == Space.Self) _displacement = transform.TransformDirection(_displacement);
		return characterController.SimpleMove(_displacement);
	}

	/// <summary>Applies Gravity To User.</summary>
	private void ApplyGravity()
	{
		if(!characterController.isGrounded) characterController.SimpleMove(-head.up);
	}
}
}