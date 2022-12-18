using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using VoidlessUtilities.VR;
using VoidlessUtilities;

namespace Supercargo
{
public class DoorLever : VRPickable
{
	[Space(5f)]
	[Header("Lever's Attributes:")]
	[SerializeField] private float _force; 					/// <summary>Additional Force.</summary>
	[SerializeField] private Vector3 _minOffset; 			/// <summary>Minimum Offset.</summary>
	[SerializeField] private Vector3 _maxOffset; 			/// <summary>Maximum Offset.</summary>
	[SerializeField] private Vector3 _minRotation; 			/// <summary>Minimum Rotation.</summary>
	[SerializeField] private Vector3 _maxRotation; 			/// <summary>Maximum Rotation.</summary>
	[SerializeField] private NormalizedVector3 _normal; 	/// <summary>Normal.</summary>
	[SerializeField] private bool invert; 					/// <summary>Invert Sign?.</summary>
	[SerializeField][Range(0.0f, 0.5f)] private float _pullTolerance; 			/// <summary>Pull's Tolerance.</summary>
#if UNITY_EDITOR
	[SerializeField] private Color color; 					/// <summary>Gizmos' Color.</summary>
	[SerializeField] private float radius; 					/// <summary>Gizmos' Radius.</summary>
#endif
	private Quaternion minQuaternion;
	private Quaternion maxQuaternion;
	private bool _pulled;
	private bool _pushed;
	private bool _slided;
	private Vector3 _normalizedProgress;

	/// <summary>Gets minRotation property.</summary>
	public Vector3 minRotation { get { return _minRotation; } }

	/// <summary>Gets maxRotation property.</summary>
	public Vector3 maxRotation { get { return _maxRotation; } }

	/// <summary>Gets pulled property.</summary>
	public bool pulled { get { return _pulled; } }

	/// <summary>Gets pushed property.</summary>
	public bool pushed { get { return _pushed; } }

	/// <summary>Gets slided property.</summary>
	public bool slided { get { return _slided; } }

	/// <summary>Gets normalizedProgress property.</summary>
	public Vector3 normalizedProgress { get { return _normalizedProgress; } }

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		Gizmos.color = color;
		Handles.color = color;
		Gizmos.DrawWireSphere(transform.position + (transform.right * _minOffset.x), radius);
		Gizmos.DrawWireSphere(transform.position + (transform.right * _maxOffset.x), radius);
		Gizmos.DrawWireSphere(transform.position + (transform.up * _minOffset.y), radius);
		Gizmos.DrawWireSphere(transform.position + (transform.up * _maxOffset.y), radius);
		Gizmos.DrawWireSphere(transform.position + (transform.forward * _minOffset.z), radius);
		Gizmos.DrawWireSphere(transform.position + (transform.forward * _maxOffset.z), radius);

		Quaternion min = Quaternion.Euler(_minRotation);
		Quaternion max = Quaternion.Euler(_maxRotation);
		Color blue = Color.blue;
		blue.a = 0.5f;
		Handles.color = blue;
		/*Handles.DrawSolidArc(transform.position, transform.forward, _normal, _maxRotation.z, 1.0f);
		Handles.DrawSolidArc(transform.position, transform.forward, _normal, _minRotation.z, 1.0f);*/
		//Handles.DrawSolidArc(transform.position, transform.forward, _normal, Mathf.Abs(_minRotation.z - _maxRotation.z), 1.0f);
#endif
	}

	private void Awake()
	{
		minQuaternion = Quaternion.Euler(_minRotation);
		maxQuaternion = Quaternion.Euler(_maxRotation);
	}

	private void Update()
	{
		if(hand != null)
		{
			Vector3 minOffset = transform.position + _minOffset;
			Vector3 maxOffset = transform.position + _maxOffset;
			Vector3 handOffset = Extensions.Clamp(hand.transform.position, minOffset, maxOffset);

			_normalizedProgress = new Vector3(
				handOffset.x.Remap(0.0f, 1.0f, minOffset.x, maxOffset.x),
				handOffset.y.Remap(0.0f, 1.0f, minOffset.y, maxOffset.y),
				handOffset.z.Remap(0.0f, 1.0f, minOffset.z, maxOffset.z));

			transform.rotation = Quaternion.Lerp(minQuaternion, maxQuaternion, normalizedProgress.y);

			_pulled = (normalizedProgress.y >= (1.0f - _pullTolerance));
			_pushed = (pulled && (normalizedProgress.z >= (1.0f - _pullTolerance)));
			_slided = ((pulled && pushed) && (normalizedProgress.z >= (1.0f - _pullTolerance)));
		}
	}
}
}