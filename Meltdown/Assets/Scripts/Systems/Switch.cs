using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject indicator;

    public bool enable = true;

    private void OnMouseDown()
    {
        print("hit");
        if (enable)
            enable = false;
        else
            enable = true;
    }
}
