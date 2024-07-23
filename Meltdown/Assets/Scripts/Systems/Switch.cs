using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject indicator;

    public bool enable = true;

    private PressureSystem pressure;

    private void Start()
    {
        pressure = FindFirstObjectByType<PressureSystem>();
    }

    private void OnMouseDown()
    {
        print("hit");
        if (enable)
            enable = false;
        else
            enable = true;

        if (enable)
            pressure.currentPuzzlesUncompleted -= 1 / 8;
        else
            pressure.currentPuzzlesUncompleted += 1 / 8;
    }


}
