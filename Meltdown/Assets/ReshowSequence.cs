using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReshowSequence : MonoBehaviour
{
    private SequenceNodes function;

    private void Start()
    {
        function = FindFirstObjectByType<SequenceNodes>();
    }

    private void OnMouseDown()
    {
        function.RecallButtonSequence();
    }
}
