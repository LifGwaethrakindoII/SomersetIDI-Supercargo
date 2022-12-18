using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supercargo
{
public delegate void OnGameOver(bool _success, string _description);

public class SupercargoLogicFlow : MonoBehaviour
{
	public const int COUNT_STEPS = 9;

	public static event OnGameOver onGameOver;
	private static string scene;

	[SerializeField] private string _sceneName; 			/// <summary>Scene's Naeme.</summary>
	[SerializeField] private string[] _stepsDescriptions; 	/// <summary>Steps' Descriptions.</summary>
	private int _currentStep;

	/// <summary>Gets sceneName property.</summary>
	public string sceneName { get { return _sceneName; } }

	/// <summary>Gets and Sets stepsDescriptions property.</summary>
	public string[] stepsDescriptions
	{
		get { return _stepsDescriptions; }
		private set { _stepsDescriptions = value; }
	}

	/// <summary>Gets and Sets currentStep property.</summary>
	public int currentStep
	{
		get { return _currentStep; }
		private set { _currentStep = value; }
	}

	private void Reset()
	{
		stepsDescriptions = new string[COUNT_STEPS];
	}

	private void OnEnable()
	{
		FlowEventTriggerer.onEventTriggered += EvaluateStep;
	}

	private void OnDisable()
	{
		FlowEventTriggerer.onEventTriggered -= EvaluateStep;
	}

	private void Awake()
	{
		currentStep = 0;
		scene = sceneName;
	}

	private void EvaluateStep(int _stepID)
	{
		Debug.Log("[SupercargoLogicFlow] Invoked Event with ID: " + _stepID);

		if(currentStep == _stepID)
		{ // Good to go
			if(currentStep >= (stepsDescriptions.Length - 1) && onGameOver != null) onGameOver(true, stepsDescriptions[currentStep]);
			else currentStep++;
		} else if(onGameOver != null)
		{ // Game Over, session lost.
			onGameOver(false, stepsDescriptions[currentStep]);
			//enabled = false; // Stop listening to further flow events.
		}
	}

	public static void ResetScene()
	{
		SceneManager.LoadScene(scene);
	}
}
}