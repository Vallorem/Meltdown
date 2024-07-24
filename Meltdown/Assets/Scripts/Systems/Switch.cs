using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject indicator;

    public bool enable = true;

    private PressureSystem pressure;
    public AudioSource audio;

    private void Start()
    {
        pressure = FindFirstObjectByType<PressureSystem>();
        StartCoroutine(RandomlyShut());
    }

    private void OnMouseDown()
    {
        print("hit");
        if (enable)
            enable = false;
        else
            enable = true;

        audio.Play();
        
        if (enable)
            pressure.currentPuzzlesUncompleted -= 1 / 8;
        else
            pressure.currentPuzzlesUncompleted += 1 / 8;
    }

    private void Update()
    {
        if(enable)
            indicator.GetComponent<MeshRenderer>().materials[0].color = Color.white;
        else
            indicator.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }
    
    private IEnumerator RandomlyShut()
    {
        while(true)
        {
            enable = false;

            yield return new WaitForSeconds(Random.Range(20, 30));
        }

        yield return null;
    }

}
