using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

using InputButton = UnityEngine.EventSystems.PointerEventData.InputButton;
using FramePressState = UnityEngine.EventSystems.PointerEventData.FramePressState;

namespace VoidlessUtilities.VR
{
[RequireComponent(typeof(VRHand))]
[RequireComponent(typeof(LineRenderer))]
public class VRUIHandInput : MonoBehaviour
{
	[Header("Input's Attributes:")]
	[SerializeField] private EVRButtonId _input0; 				/// <summary>Input 0's ID.</summary>
	[SerializeField] private EVRButtonId _input1; 				/// <summary>Input 1's ID.</summary>
	[SerializeField] private EVRButtonId _input2; 				/// <summary>Input 2's ID.</summary>
	[SerializeField] private EVRButtonId _scrollAxis; 			/// <summary>Scroll's Axis.</summary>
	[Space(5f)]
	[Header("Ray's Attributes:")]
	[SerializeField] private Vector3 _rayOriginPoint; 			/// <summary>Ray Origin's Point relative to this GameObject's Transform.</summary>
	[SerializeField] private NormalizedVector3 _rayDirection; 	/// <summary>Ray's Direction.</summary>
	[SerializeField] private float _rayLength; 					/// <summary>Ray's Length.</summary>
#if UNITY_EDITOR
	[Space(5f)]
	[Header("Gizmos' Attributes:")]
	[SerializeField] private Color gizmosColor; 				/// <summary>Gizmos' Color.</summary>
	[SerializeField] private float gizmosRadius; 				/// <summary>Gizmos' Radius.</summary>
#endif
	private Button _selectable; 							/// <summary>Current's Button.</summary>
	private PointerEventData _data; 							/// <summary>Pointer's Event Data.</summary>
	private VRHand _hand; 										/// <summary>Hand's Component.</summary>
	private LineRenderer _lineRenderer; 						/// <summary>LineRenderer's Component.</summary>
	private Ray _ray; 											/// <summary>Ray casted from the hand.</summary>
	private Vector2 lastScrollAxis; 							/// <summary>Last's Scroll Axis.</summary>

#region Getters/Setters:
	/// <summary>Gets and Sets input0 property.</summary>
	public EVRButtonId input0
	{
		get { return _input0; }
		set { _input0 = value; }
	}

	/// <summary>Gets and Sets input1 property.</summary>
	public EVRButtonId input1
	{
		get { return _input1; }
		set { _input1 = value; }
	}

	/// <summary>Gets and Sets input2 property.</summary>
	public EVRButtonId input2
	{
		get { return _input2; }
		set { _input2 = value; }
	}

	/// <summary>Gets and Sets scrollAxis property.</summary>
	public EVRButtonId scrollAxis
	{
		get { return _scrollAxis; }
		set { _scrollAxis = value; }
	}

	/// <summary>Gets and Sets rayOriginPoint property.</summary>
	public Vector3 rayOriginPoint
	{
		get { return _rayOriginPoint; }
		set { _rayOriginPoint = value; }
	}

	/// <summary>Gets and Sets rayDirection property.</summary>
	public NormalizedVector3 rayDirection
	{
		get { return _rayDirection; }
		set { _rayDirection = value; }
	}

	/// <summary>Gets and Sets rayLength property.</summary>
	public float rayLength
	{
		get { return _rayLength; }
		set { _rayLength = value; }
	}

	/// <summary>Gets and Sets selectable property.</summary>
	public Button selectable
	{
		get { return _selectable; }
		set { _selectable = value; }
	}

	/// <summary>Gets and Sets data property.</summary>
	public PointerEventData data
	{
		get { return _data; }
		set { _data = value; }
	}

	/// <summary>Gets and Sets hand Component.</summary>
	public VRHand hand
	{ 
		get
		{
			if(_hand == null)
			{
				_hand = GetComponent<VRHand>();
			}
			return _hand;
		}
	}

	/// <summary>Gets and Sets lineRenderer Component.</summary>
	public LineRenderer lineRenderer
	{ 
		get
		{
			if(_lineRenderer == null)
			{
				_lineRenderer = GetComponent<LineRenderer>();
			}
			return _lineRenderer;
		}
	}

	/// <summary>Gets and Sets ray property.</summary>
	public Ray ray
	{
		get { return _ray; }
		set { _ray = value; }
	}
#endregion

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		Vector3 point = transform.TransformPoint(rayOriginPoint);
		Vector3 direction = (transform.TransformDirection(rayDirection) * rayLength);

		Gizmos.color = gizmosColor;
		Gizmos.DrawRay(point, direction);
		Gizmos.DrawWireSphere(point + direction, gizmosRadius);
#endif
	}

#region UnityMethods:
	/// <summary>VRUIHandInput's instance initialization.</summary>
	void Awake()
	{
		data = new PointerEventData(EventSystem.current);
		UpdatePointerEventData(InputButton.Left, FramePressState.NotChanged);
	}
	
	/// <summary>VRUIHandInput's tick at each frame.</summary>
	void Update ()
	{
		ray = new Ray(transform.TransformPoint(rayOriginPoint), (transform.TransformDirection(rayDirection) * rayLength));
		lineRenderer.SetPosition(0, ray.origin);
		lineRenderer.SetPosition(1, ray.origin + ray.direction);
		CastRay();
		EvaluatePointerEventData();
	}
#endregion

	private void EvaluatePointerEventData()
	{
		InputButton inputButton = default(InputButton);
		FramePressState pressState = default(FramePressState);

		if(hand.device.GetPressDown(input0))
		{
			pressState = FramePressState.Pressed;
			inputButton = InputButton.Left;
		} else if(hand.device.GetPressDown(input1))
		{
			pressState = FramePressState.Pressed;
			inputButton = InputButton.Middle;
		} else if(hand.device.GetPressDown(input2))
		{
			pressState = FramePressState.Pressed;
			inputButton = InputButton.Right;
		} else if(hand.device.GetPress(input0))
		{
			pressState = FramePressState.NotChanged;
			inputButton = InputButton.Left;
		} else if(hand.device.GetPress(input1))
		{
			pressState = FramePressState.NotChanged;
			inputButton = InputButton.Middle;
		} else if(hand.device.GetPress(input2))
		{
			pressState = FramePressState.NotChanged;
			inputButton = InputButton.Right;	
		} else if(hand.device.GetPressUp(input0))
		{
			pressState = FramePressState.Released;
			inputButton = InputButton.Left;
		} else if(hand.device.GetPressUp(input1))
		{
			pressState = FramePressState.Released;
			inputButton = InputButton.Middle;
		} else if(hand.device.GetPressUp(input2))
		{
			pressState = FramePressState.Released;
			inputButton = InputButton.Right;
		} else pressState = FramePressState.NotChanged;

		UpdatePointerEventData(inputButton, pressState);
	}

	private void UpdatePointerEventData(InputButton _inputButton, FramePressState _pressState)
	{
		Vector2 lastPosition = data.position;
		Vector2 currentScrollAxis = hand.device.GetAxis(scrollAxis);

		data.button = _inputButton;
		data.position = ray.origin + ray.direction;
		data.scrollDelta = currentScrollAxis - lastScrollAxis;
		data.worldPosition = data.position;
		data.delta = data.position - lastPosition;
		data.worldNormal = ray.direction;
		lastScrollAxis = currentScrollAxis;

		switch(_pressState)
		{
			case FramePressState.Pressed:
			data.pressPosition = data.position;
			if(selectable != null && data.button == InputButton.Left) selectable.OnPointerDown(data);
			break;

			case FramePressState.NotChanged:
			break;

			case FramePressState.Released:
			//if(selectable != null && data.button == InputButton.Left) selectable.OnPointerUp(data);
			if(selectable != null && data.button == InputButton.Left) selectable.onClick.Invoke();
			break;
		}
	}

	private void CastRay()
	{
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, rayLength, VoidlessLayerMask.LAYER_VALUE_UI))
		{
			lineRenderer.SetPosition(1, hit.point);
			Button currentButton = hit.transform.GetComponent<Button>();
			if(currentButton != null)
			{
				if(selectable != null)
				{
					if(selectable != currentButton)
					{
						selectable.OnPointerExit(data);
						selectable = currentButton;
						selectable.OnPointerEnter(data);
					}
				}
				else
				{
					selectable = currentButton;
					selectable.OnPointerEnter(data);
				}
			}
		}
		else if(selectable != null)
		{
			selectable.OnPointerExit(data);
			selectable = null;
		}
	}
}
}