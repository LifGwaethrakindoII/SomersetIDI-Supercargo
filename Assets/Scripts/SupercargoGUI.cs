using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supercargo
{
public class SupercargoGUI : MonoBehaviour
{
	[Header("Texts:")]
	[SerializeField] private Text _label; 			/// <summary>Label.</summary>
	[SerializeField] private Text _content; 		/// <summary>Content.</summary>
	[Space(5f)]
	[Header("Buttons:")]
	[SerializeField] private Button _resetButton; 	/// <summary>Reset's Button.</summary>
	[SerializeField] private Button _saveButton; 	/// <summary>Save's Button.</summary>

	/// <summary>Gets label property.</summary>
	public Text label { get { return _label; } }

	/// <summary>Gets content property.</summary>
	public Text content { get { return _content; } }

	/// <summary>Gets resetButton property.</summary>
	public Button resetButton { get { return _resetButton; } }

	/// <summary>Gets saveButton property.</summary>
	public Button saveButton { get { return _saveButton; } }

	private void Awake()
	{
		resetButton.onClick.AddListener(OnResetButtonPressed);
		saveButton.onClick.AddListener(OnSaveButtonPressed);
		gameObject.SetActive(false);
		SupercargoLogicFlow.onGameOver += OnGameOver;
	}

	private void OnDestroy()
	{
		SupercargoLogicFlow.onGameOver -= OnGameOver;	
	}

	private void OnResetButtonPressed()
	{
		SupercargoLogicFlow.ResetScene();
	}

	private void OnSaveButtonPressed()
	{
		saveButton.gameObject.SetActive(false);
	}

	private void OnGameOver(bool _success, string _description)
	{
		label.text = _success ? "Exito!" : "Fallaste!";
		content.text = _success ? "Ejecutaste todos los pasos en orden. Felicidades." : ("Omitiste el paso: " + _description + ".");
		gameObject.SetActive(true);
	}
}
}