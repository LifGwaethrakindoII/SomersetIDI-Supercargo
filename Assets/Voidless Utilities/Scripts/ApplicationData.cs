using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

namespace VoidlessUtilities
{
public delegate void OnDataUpdated(ApplicationData _data);

[CreateAssetMenu(menuName = ASSET_PATH_ROOT + "Data")]
[XmlRoot(Namespace="VoidlessUtilities", IsNullable = true)]
public class ApplicationData : ScriptableObject
{
	public const string RESOURCES_DATA_ROOT = "Assets/Resources/Data/"; 	/// <summary>Resources' Path Root.</summary>
	public const string ASSET_PATH_ROOT = "Application/"; 					/// <summary>Application's Scriptable Object Path's Root.</summary>

	public static event OnDataUpdated onDataUpdated;

	//[SerializeField] private TextAsset _file; 							/// <summary>File to serialize the data to.</summary>
	[SerializeField][XmlArray("PatternCommands")]
	public PatternCommand[] _commands; 										/// <summary>Application's Commands.</summary>
	[SerializeField][XmlArray("PatternClassifications")]
	public PatternClassification[] _classifications;
#if UNITY_EDITOR
	[HideInInspector] public Color _color;
	[HideInInspector] public string _dataName;
	[HideInInspector] public int _index;
	[HideInInspector] public bool _toggle;

	/// <summary>Gets and Sets color property.</summary>
	public Color color
	{
		get { return _color; }
		set { _color = value; }
	}

	/// <summary>Gets and Sets dataName property.</summary>
	public string dataName
	{
		get { return _dataName; }
		set { _dataName = value; }
	}

	/// <summary>Gets and Sets index property.</summary>
	public int index
	{
		get { return _index; }
		set { _index = value; }
	}

	/// <summary>Gets and Sets toggle property.</summary>
	public bool toggle
	{
		get { return _toggle; }
		set { _toggle = value; }
	}
#endif

	/// <summary>Gets and Sets file property.</summary>
	/*public TextAsset file
	{
		get { return _file; }
		set { _file = value; }
	}*/

	/// <summary>Gets and Sets commands property.</summary>
	//[SerializeField][XmlArray("Pattern_Commands"), XmlArrayItem("Pattern_Command")]
	public PatternCommand[] commands
	{
		get { return _commands; }
		private set { _commands = value; }
	}

	/// <summary>Gets and Sets classifications property.</summary>
	public PatternClassification[] classifications
	{
		get { return _classifications; }
		set { _classifications = value; }
	}

	/// <summary>Serializes ScriptableObject's Data into an XML File.</summary>
	/// <param name="_name">Name of the Serialized XML file.</param>
	public void SerializeData(string _name)
	{
		StringBuilder builder = new StringBuilder();

		builder.Append(RESOURCES_DATA_ROOT);
		builder.Append(_name);
		builder.Append(Extensions.EXTENSION_SUFIX_XML);

		this.Serialize(builder.ToString());
	}

	/// <summary>deserializes XML's Data into this ScriptableObject.</summary>
	/// <param name="_name">Name of the XML to deserialize.</param>
	public void DeserializeData(string _name)
	{
		StringBuilder builder = new StringBuilder();

		builder.Append(RESOURCES_DATA_ROOT);
		builder.Append(_name);
		builder.Append(Extensions.EXTENSION_SUFIX_XML);

		ApplicationData data = Extensions.Deserialize<ApplicationData>(builder.ToString());
		if(data != null)
		{
			commands = data.commands;
			classifications = data.classifications;
		}
	}

	public void UpdateData()
	{
		if(onDataUpdated != null) onDataUpdated(this);
	}
}
}