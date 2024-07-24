using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Lever : MonoBehaviour
{
    private Vector3 startMouseDragPosition;
    private SimonSays simonSays;
    public int lever;
    public Animator animator;
    public AudioSource audio;
    private void OnMouseDown()
    {
        startMouseDragPosition = Input.mousePosition;
        if (startMouseDragPosition.y < 0)
            startMouseDragPosition = new Vector3(startMouseDragPosition.x, startMouseDragPosition.y * -1, startMouseDragPosition.z);
    }


    private void Start()
    {
        simonSays = FindFirstObjectByType<SimonSays>();
    }

    private void OnMouseDrag()
    {
        Vector3 newMousePos = Input.mousePosition;
        if (newMousePos.y < 0) newMousePos.y *= -1;
        if (startMouseDragPosition.y - newMousePos.y > 100f)  {simonSays.PullLever(lever); audio.Play();}
    }
}
