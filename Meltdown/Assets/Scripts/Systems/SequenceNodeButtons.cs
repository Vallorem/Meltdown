using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNodeButtons : MonoBehaviour
{
    private SequenceNodes nodes;
    public int buttonNo = 1;
    private void Start()
    {
        nodes = FindFirstObjectByType<SequenceNodes>();
    }

    private void OnMouseDown()
    {
        nodes.UpdatePlayerList(buttonNo);
    }
}
