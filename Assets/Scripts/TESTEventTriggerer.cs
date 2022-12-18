using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supercargo
{
[RequireComponent(typeof(FlowEventTriggerer))]
public class TESTEventTriggerer : MonoBehaviour
{
	private FlowEventTriggerer	_flowEventTriggerer; 	/// <summary>FlowEventTriggerer's Component.</summary>

	/// <summary>Gets and Sets flowEventTriggerer Component.</summary>
	public FlowEventTriggerer flowEventTriggerer
	{ 
		get
		{
			if(_flowEventTriggerer == null)
			{
				_flowEventTriggerer = GetComponent<FlowEventTriggerer>();
			}
			return _flowEventTriggerer;
		}
	}

	public void TriggerEvent()
	{
		flowEventTriggerer.InvokeEvent();
	}
}
}