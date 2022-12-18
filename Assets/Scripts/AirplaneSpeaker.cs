using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supercargo
{
[RequireComponent(typeof(FlowEventTriggerer))]
[RequireComponent(typeof(AudioSource))]
public class AirplaneSpeaker : MonoBehaviour
{
	[SerializeField] private AudioClip _audio; 		/// <summary>Audio to Play.</summary>
	[SerializeField] private float _startAfter; 	/// <summary>Start Sound After X Seconds.</summary>
	private FlowEventTriggerer _eventTriggerer; 	/// <summary>flowevetnTriggerer's Component.</summary>
	private AudioSource _audioSource; 				/// <summary>AudioSource's Component.</summary>

	/// <summary>Gets audio property.</summary>
	public AudioClip audio { get { return _audio; } }

	/// <summary>Gets startAfter property.</summary>
	public float startAfter { get { return _startAfter; } }

	/// <summary>Gets and Sets eventTriggerer Component.</summary>
	public FlowEventTriggerer eventTriggerer
	{ 
		get
		{
			if(_eventTriggerer == null)
			{
				_eventTriggerer = GetComponent<FlowEventTriggerer>();
			}
			return _eventTriggerer;
		}
	}

	/// <summary>Gets and Sets audioSource Component.</summary>
	public AudioSource audioSource
	{ 
		get
		{
			if(_audioSource == null)
			{
				_audioSource = GetComponent<AudioSource>();
			}
			return _audioSource;
		}
	}

	private void Awake()
	{
		audioSource.Stop();
	}

	private IEnumerator Start()
	{
		WaitForSeconds wait = new WaitForSeconds(startAfter);
		yield return wait;
		audioSource.PlayOneShot(audio);
		while(audioSource.isPlaying) yield return null;
		eventTriggerer.InvokeEvent();
		Debug.Log("[AirplaneSpeaker] PILOT'S ORDER GIVEN!! INVOKING EVENT " + eventTriggerer.eventID.ToString());
	}
}
}