using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoidlessUtilities.VR;

namespace Supercargo
{
public class Door : MonoBehaviour
{
	[SerializeField] private DoorLever _doorLever; 							/// <summary>Door's Lever.</summary>
	[SerializeField] private DoorHandler _doorHandler; 						/// <summary>Door's Handler.</summary>
	[SerializeField] private VRPickable _handler; 							/// <summary>Handler.</summary>
	[SerializeField] private TransformInterpolationData _inferiorLockData; 	/// <summary>Inferior Lock.</summary>
	[SerializeField] private TransformInterpolationData _doorHingeData; 		/// <summary>Door's Hinge.</summary>
	private bool handled;
	private bool handledDoor;
	private Vector3 interpolatedPosition;

	/// <summary>Gets doorLever property.</summary>
	public DoorLever doorLever { get { return _doorLever; } }

	/// <summary>Gets doorHandler property.</summary>
	public DoorHandler doorHandler { get { return _doorHandler; } }

	/// <summary>Gets handler property.</summary>
	public VRPickable handler { get { return _handler; } }

	/// <summary>Gets inferiorLockData property.</summary>
	public TransformInterpolationData inferiorLockData { get { return _inferiorLockData; } }

	/// <summary>Gets doorHingeData property.</summary>
	public TransformInterpolationData doorHingeData { get { return _doorHingeData; } }

	private void OnEnable()
	{
		if(handler != null)
		{
			handler.onPicked += OnHandlerPicked;
			handler.onDropped += OnHandlerDropped;
			doorHandler.onPicked += OnHandlerPicked;
			doorHandler.onDropped += OnHandlerDropped;
		}
	}

	private void OnDisable()
	{
		if(handler != null)
		{
			handler.onPicked -= OnHandlerPicked;
			handler.onDropped -= OnHandlerDropped;
			doorHandler.onPicked -= OnHandlerPicked;
			doorHandler.onDropped -= OnHandlerDropped;
		}
	}

	private void Awake()
	{
		handled = handler != null ? (handler.hand != null) : false;
		interpolatedPosition = inferiorLockData.minPosition;
	}

	private void Update()
	{
		if(doorLever != null)
		{
			interpolatedPosition.x = Mathf.Lerp(inferiorLockData.minPosition.x, inferiorLockData.maxPosition.x, doorHandler.progressX);
			interpolatedPosition.y = Mathf.Lerp(inferiorLockData.minPosition.y, inferiorLockData.maxPosition.y, doorLever.normalizedProgress.y);
			interpolatedPosition.z = Mathf.Lerp(inferiorLockData.minPosition.z, inferiorLockData.maxPosition.z, doorHandler.progressZ);
			/*if(handled && doorLever.pulled) interpolatedPosition.z = Mathf.Lerp(inferiorLockData.minPosition.z, inferiorLockData.maxPosition.z, doorLever.normalizedProgress.z);
			if(handled && doorLever.pushed) interpolatedPosition.x = Mathf.Lerp(inferiorLockData.minPosition.x, inferiorLockData.maxPosition.x, doorLever.normalizedProgress.x);*/
			/*if(handledDoor)
			{
				if(doorLever.pulled)
				{
					interpolatedPosition.z = Mathf.Lerp(inferiorLockData.minPosition.z, inferiorLockData.maxPosition.z, doorHandler.progressZ);
					if(doorHandler.pushed) interpolatedPosition.x = Mathf.Lerp(inferiorLockData.minPosition.x, inferiorLockData.maxPosition.x, doorHandler.progressX);
				}
				
			}*/

			Vector3 angles = Vector3.Lerp(doorHingeData.minRotation, doorHingeData.maxRotation, doorHandler.progressX);
			Quaternion rotation = doorHingeData.transform.rotation;
			rotation.eulerAngles = angles;
			doorHingeData.transform.rotation = rotation;

			inferiorLockData.transform.localPosition = interpolatedPosition;
		}
	}

	private void OnHandlerPicked(VRPickable _pickable)
	{
		if(_pickable == handler) handled = true;
		else if(_pickable == doorHandler) handledDoor = true;
	}

	private void OnHandlerDropped(VRPickable _pickable)
	{
		if(_pickable == handler) handled = false;
		else if(_pickable == doorHandler) handledDoor = false;
	}
}

[System.Serializable]
public class TransformInterpolationData
{
	public Transform transform;
	public Vector3 minPosition;
	public Vector3 maxPosition;
	public Vector3 minRotation;
	public Vector3 maxRotation;
}
}