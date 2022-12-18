using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoidlessUtilities.VR
{
public class VRHead : MonoBehaviour
{
	public const SteamVR_TrackedObject.EIndex INDEX_LEFT_HAND = SteamVR_TrackedObject.EIndex.Device4;
	public const SteamVR_TrackedObject.EIndex INDEX_RIGHT_HAND = SteamVR_TrackedObject.EIndex.Device3;
	public const SteamVR_TrackedObject.EIndex INDEX_LEFT_HAND_ALT = SteamVR_TrackedObject.EIndex.Device2;
	public const SteamVR_TrackedObject.EIndex INDEX_RIGHT_HAND_ALT = SteamVR_TrackedObject.EIndex.Device1;

	[SerializeField] private Transform _eye; 		/// <summary>Eye's Transform.</summary>
	[SerializeField] private Transform _fixedEye; 	/// <summary>Fixed Eye's Transform.</summary>
	[Header("Controllers:")]
	[SerializeField] private VRHand _leftHand; 		/// <summary>Left Hand.</summary>
	[SerializeField] private VRHand _rightHand; 	/// <summary>RightHand.</summary>
	[Space(5f)]
	[SerializeField] private Vector3 _up; 			/// <summary>Up's Reference.</summary>
	private Vector3 _forward; 						/// <summary>Eye's Forward.</summary>
	private SteamVR_TrackedObject _trackedObject; 	/// <summary>TrackedObject's Component.</summary>

#if UNITY_EDITOR
	[SerializeField] private float rayLength; 		/// <summary>Rays' Length.</summary>
#endif

	/// <summary>Gets eye property.</summary>
	public Transform eye { get { return _eye; } }

	/// <summary>Gets fixedEye property.</summary>
	public Transform fixedEye { get { return _fixedEye; } }

	/// <summary>Gets leftHand property.</summary>
	public VRHand leftHand { get { return _leftHand; } }

	/// <summary>Gets rightHand property.</summary>
	public VRHand rightHand { get { return _rightHand; } }

	/// <summary>Gets and Sets up property.</summary>
	public Vector3 up
	{
		get { return _up; }
		set { _up = value; }
	}

	/// <summary>Gets and Sets forward property.</summary>
	public Vector3 forward
	{
		get { return _forward; }
		private set { _forward = value; }
	}

	/// <summary>Gets and Sets trackedObject Component.</summary>
	public SteamVR_TrackedObject trackedObject
	{ 
		get
		{
			if(_trackedObject == null)
			{
				_trackedObject = GetComponent<SteamVR_TrackedObject>();
			}
			return _trackedObject;
		}
	}

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		if(eye != null)
		{
			if(!Application.isPlaying) UpdateForward();
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(eye.position, forward * rayLength);
		}
#endif
	}

	private void Awake()
	{
		RecalibrateControllers();
	}

	private void Update()
	{
		if(eye != null) UpdateForward();
	}

	/// <summary>Recalibrates Controllers.</summary>
	public void RecalibrateControllers()
	{
		switch(leftHand.trackedObject.index)
		{
			case INDEX_RIGHT_HAND:
			case INDEX_RIGHT_HAND_ALT:
			VRHand temp = rightHand;
			_rightHand = leftHand;
			_leftHand = temp;
			break;
		}

		switch(rightHand.trackedObject.index)
		{
			case INDEX_LEFT_HAND:
			case INDEX_LEFT_HAND_ALT:
			VRHand temp = rightHand;
			_rightHand = rightHand;
			_rightHand = temp;
			break;
		}
	}

	/// <summary>Updates Forward Vector relative to the Eye's Forward and the Up reference.</summary>
	private void UpdateForward()
	{
		forward = Vector3.Cross(eye.right, up);
		fixedEye.rotation = Quaternion.LookRotation(forward);
	}
}
}