using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoidlessUtilities.VR;
using VoidlessUtilities;

public class DoorHandler : VRPickable
{
	[SerializeField] private Vector3 defaultPosition; 	/// <summary>Default Position.</summary>
	[SerializeField] private Vector3 offset; 	/// <summary>Offset.</summary>
	[SerializeField][Range(0.0f, 0.5f)] private float _pullTolerance;
	private float _progressX;
	private float _progressZ;
	private bool _pushed;
	private bool _slided;
#if UNITY_EDITOR
	[SerializeField] private Color color; 					/// <summary>Gizmos' Color.</summary>
	[SerializeField] private float radius; 					/// <summary>Gizmos' Radius.</summary>
#endif

	/// <summary>Gets pushed property.</summary>
	public bool pushed { get { return _pushed; } }

	/// <summary>Gets slided property.</summary>
	public bool slided { get { return _slided; } }

	/// <summary>Gets progressX property.</summary>
	public float progressX { get { return _progressX; } }

	/// <summary>Gets progressZ property.</summary>
	public float progressZ { get { return _progressZ; } }

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		if(!Application.isPlaying)
		{
			Gizmos.color = color;
			Gizmos.DrawWireSphere(transform.position + (transform.right * offset.x), radius);
			//Gizmos.DrawWireSphere(transform.position + (transform.right * _maxOffset.x), radius);
			Gizmos.DrawWireSphere(transform.position + (transform.forward * offset.z), radius);
			//Gizmos.DrawWireSphere(transform.position + (transform.forward * _maxOffset.z), radius);
		}
		else
		{
			Gizmos.color = color;
			Gizmos.DrawWireSphere(defaultPosition + (transform.right * offset.x), radius);
			//Gizmos.DrawWireSphere(defaultPosition + (transform.right * _maxOffset.x), radius);
			Gizmos.DrawWireSphere(defaultPosition + (transform.forward * offset.z), radius);
			//Gizmos.DrawWireSphere(defaultPosition + (transform.forward * _maxOffset.z), radius);
		}
#endif
	}

	private void Awake()
	{
		defaultPosition = transform.position;
	}

	private void Update()
	{
		if(hand != null)
		{
			Vector3 calculatedOffset = defaultPosition + offset;
			Vector3 handOffset = Extensions.Clamp(hand.transform.position, defaultPosition, calculatedOffset);
			_progressX = handOffset.x.Remap(0.0f, 1.0f, Mathf.Min(defaultPosition.x, calculatedOffset.x), Mathf.Max(defaultPosition.x, calculatedOffset.x));
			_progressZ = handOffset.z.Remap(0.0f, 1.0f, Mathf.Min(defaultPosition.z, calculatedOffset.z), Mathf.Max(defaultPosition.z, calculatedOffset.z));
		}
		else
		{
			//_progressX = 0.0f;
			//_progressZ = 0.0f;
		}

		_pushed = (progressZ >= (1.0f - _pullTolerance));
		_slided = (progressX >= (1.0f - _pullTolerance));
	}
}
