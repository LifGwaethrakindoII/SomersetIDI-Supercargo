using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinHinge : MonoBehaviour
{
	[SerializeField] private Transform _cabinDoor; 				/// <summary>Cabin's Door.</summary>
	[SerializeField] private Collider _interactableCollider; 	/// <summary>Interactable's Collider.</summary>
	private Quaternion _defaultCabinDoorRotation;
	private Quaternion _defaultInteractableRotation;

	/// <summary>Gets cabinDoor property.</summary>
	public Transform cabinDoor { get { return _cabinDoor; } }

	/// <summary>Gets interactableCollider property.</summary>
	public Collider interactableCollider { get { return _interactableCollider; } }

	/// <summary>Gets and Sets defaultCabinDoorRotation property.</summary>
	public Quaternion defaultCabinDoorRotation
	{
		get { return _defaultCabinDoorRotation; }
		set { _defaultCabinDoorRotation = value; }
	}

	/// <summary>Gets and Sets defaultInteractableRotation property.</summary>
	public Quaternion defaultInteractableRotation
	{
		get { return _defaultInteractableRotation; }
		set { _defaultInteractableRotation = value; }
	}

	private void Awake()
	{
		defaultCabinDoorRotation = cabinDoor.rotation;
		defaultInteractableRotation = interactableCollider.transform.rotation;
	}

	private void Update()
	{
		cabinDoor.rotation = defaultCabinDoorRotation;
		//interactableCollider.transform.rotation = defaultInteractableRotation;
	}
}
