using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valves : MonoBehaviour
{
    [Range(-135, 135)] public float currentRotation = 0;
    public float give = 10;
    public float rotationSpeed = 5;

    public bool gameOver = false;

    [SerializeField] private GameObject currentPressureIndicator;
    [SerializeField] private GameObject currentObjectiveIndicator;

    private PressureSystem system;
    private float currentGoal = 0f;

    private bool changed = false;
    private bool updated = false;
    
    private void OnMouseOver()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            currentRotation += Time.deltaTime * rotationSpeed;
            updated = false;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentRotation -= Time.deltaTime * rotationSpeed;
            updated = false;
        }

        if (currentRotation < -135)
            currentRotation = -135;

        if (currentRotation > 135)
            currentRotation = 135;

        currentPressureIndicator.gameObject.transform.SetPositionAndRotation(currentPressureIndicator.gameObject.transform.position, 
            new Quaternion(currentRotation / 360, currentPressureIndicator.gameObject.transform.rotation.y, currentPressureIndicator.gameObject.transform.rotation.z, currentPressureIndicator.gameObject.transform.rotation.w));
    }

    private void Start()
    {
        system = FindFirstObjectByType<PressureSystem>();
        StartCoroutine(RandomSet());
    }

    private void Update()
    {
        if(!updated)
        {
            updated = true;
            if(currentRotation > currentGoal - 10f || currentRotation < currentGoal + 10f)
            {
                if(!changed)
                {
                    changed = true;
                    system.currentBasePressureDrop -= .25f;
                }
            }
            else
            {
                if(!changed)
                {
                    changed = true;
                    system.currentBasePressureDrop += .25f;
                }
            }
        }
    }

    public IEnumerator RandomSet()
    {
        while(!gameOver)
        {
            changed = false;
            currentGoal = Random.Range(-13500, 13500) / 100;
            currentObjectiveIndicator.gameObject.transform.SetPositionAndRotation(currentObjectiveIndicator.gameObject.transform.position, 
                new Quaternion(currentGoal / 360,  currentPressureIndicator.gameObject.transform.rotation.y, currentObjectiveIndicator.gameObject.transform.rotation.z, currentObjectiveIndicator.gameObject.transform.rotation.w));
            yield return new WaitForSeconds(Random.Range(0f, 50f));
        }
    }

}
