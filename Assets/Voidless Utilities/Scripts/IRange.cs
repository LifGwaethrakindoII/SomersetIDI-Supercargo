using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRange<T> where T : IComparable<T>
{
	T min { get; set; }
	T max { get; set; }
	T value { get; set; }

	T GetMinimum();
	T GetMaximum();
}
