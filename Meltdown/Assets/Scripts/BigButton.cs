using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigButton : MonoBehaviour
{
	private PressureSystem function;

	private void Start()
	{
		function = FindFirstObjectByType<PressureSystem>();
	}

	private void OnMouseDown()
	{
		function.EndGame();
	}
}
