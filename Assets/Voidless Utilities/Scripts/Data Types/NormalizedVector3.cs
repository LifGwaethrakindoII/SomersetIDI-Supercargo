using UnityEngine;
using System;

namespace VoidlessUtilities
{
[Serializable]
public struct NormalizedVector3
{
	private static readonly string LABEL_OUT_OF_RANGE = "The range is between 0 and 2."; 	/// <summary>IndexOutOfRangeException's additional message.</summary>
	private const float MIN_CONSTRAINT = -1.0f; 											/// <summary>NormalizedVector3's minimum value.</summary>
	private const float MAX_CONSTRAINT = 1.0f; 												/// <summary>NormalizedVector3's minimum value.</summary>

	[SerializeField] [Range(MIN_CONSTRAINT, MAX_CONSTRAINT)] private float _x; 				/// <summary>Vector3 X's Component.</summary>
	[SerializeField] [Range(MIN_CONSTRAINT, MAX_CONSTRAINT)] private float _y; 				/// <summary>Vector3 Y's Component.</summary>
	[SerializeField] [Range(MIN_CONSTRAINT, MAX_CONSTRAINT)] private float _z; 				/// <summary>Vector3 Z's Component.</summary>

	/// <summary>Gets and Sets x property.</summary>
	public float x
	{
		get { return _x; }
		set { _x = Mathf.Clamp(value, MIN_CONSTRAINT, MAX_CONSTRAINT); }
	}

	/// <summary>Gets and Sets y property.</summary>
	public float y
	{
		get { return _y; }
		set { _y = Mathf.Clamp(value, MIN_CONSTRAINT, MAX_CONSTRAINT); }
	}

	/// <summary>Gets and Sets z property.</summary>
	public float z
	{
		get { return _z; }
		set { _z = Mathf.Clamp(value, MIN_CONSTRAINT, MAX_CONSTRAINT); }
	}

	/// <summary>Gets and Sets Normalized Vector's component by given index [from 0 to 2].</summary>
	public float this[int _index]
	{
		get
		{
			switch(_index)
			{
				case 0: return x; break;
				case 1: return y; break;
				case 2: return z; break;
				default: throw new IndexOutOfRangeException(LABEL_OUT_OF_RANGE); break;
			}
		}
		set
		{
			switch(_index)
			{
				case 0: x = value; break;
				case 1: y = value; break;
				case 2: z = value; break;
				default: throw new IndexOutOfRangeException(LABEL_OUT_OF_RANGE); break;
			}
		}
	}

	/// <summary>Implicit NormalizedVector3 to Vector3 operator.</summary>
	public static implicit operator Vector3(NormalizedVector3 _vector) { return new Vector3(_vector.x, _vector.y, _vector.z); }

	/// <summary>Implicit Vector3 to NormalizedVector3 operator.</summary>
	public static implicit operator NormalizedVector3(Vector3 _vector) { return new NormalizedVector3(_vector.x, _vector.y, _vector.z); }

	/// <summary>Implicit NormalizedVector3 plus Vector3 value math operator.</summary>
	public static Vector3 operator + (NormalizedVector3 _n, Vector3 _vector) { return new Vector3(_n.x, _n.y, _n.z) + _vector; }

	/// <summary>Implicit NormalizedVector3 minus Vector3 value math operator.</summary>
	public static Vector3 operator - (NormalizedVector3 _n, Vector3 _vector) { return new Vector3(_n.x, _n.y, _n.z) - _vector; }

	/// <summary>Implicit NormalizedVector3 times Vector3 value math operator.</summary>
	public static Vector3 operator * (NormalizedVector3 _n, Vector3 _vector) { return new Vector3(_n.x * _vector.x, _n.y * _vector.y, _n.z * _vector.z); }

	/// <summary>Implicit NormalizedVector3 not equals Vector3 value bool operator.</summary>
	public static bool operator != (NormalizedVector3 _n, Vector3 _vector) { return (new Vector3(_n.x, _n.y, _n.z) != _vector); }

	/// <summary>Implicit NormalizedVector3 equals Vector3 value bool operator.</summary>
	public static bool operator == (NormalizedVector3 _n, Vector3 _vector) { return (new Vector3(_n.x, _n.y, _n.z) == _vector); }

	/// <summary>Implicit NormalizedVector3 times float value math operator.</summary>
	public static Vector3 operator * (NormalizedVector3 _n, float _number) { return new Vector3(_n.x, _n.y, _n.z) * _number; }

	/// <summary>Implicit NormalizedVector3 divided by float value math operator.</summary>
	public static Vector3 operator / (NormalizedVector3 _n, float _number) { return new Vector3(_n.x, _n.y, _n.z) / _number; }

	/// <summary>Gets normalized property.</summary>
	public NormalizedVector3 normalized { get { return new Vector3(x, y, z).normalized; } }

	/// <summary>NormalizedVector3's constructor.</summary>
	/// <param name="_x">X's component.</param>
	/// <param name="_y">Y's component.</param>
	/// <param name="_z">Z's component.</param>
	public NormalizedVector3(float _x, float _y, float _z) : this()
	{
		x = Mathf.Clamp(_x, MIN_CONSTRAINT, MAX_CONSTRAINT);
		y = Mathf.Clamp(_y, MIN_CONSTRAINT, MAX_CONSTRAINT);
		z = Mathf.Clamp(_z, MIN_CONSTRAINT, MAX_CONSTRAINT);
	}

	/// <returns>Normalized Vector's Magnitude.</returns>
	public float Magnitude()
	{
		return Mathf.Sqrt((x * x) + (y * y) + (z * z));
	}

	/// <summary>Normalizes Vector.</summary>
	public void Normalize()
	{
		float inverseMagnitude = 1.0f / Magnitude();
		x *= inverseMagnitude;
		y *= inverseMagnitude;
		z *= inverseMagnitude;
	}

	/// <summary>Converts Vector's data to a string.</summary>
	public override string ToString()
	{
		return "{" + x + ", " + y + ", " + z + "}";
	}
}	
}