using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigButton : MonoBehaviour
{
	private PressureSystem function;
	public AudioSource audio;
	private void Start()
	{
		function = FindFirstObjectByType<PressureSystem>();
	}

	private void OnMouseDown()
	{
		audio.Play();
		function.EndGame();
	}
}
