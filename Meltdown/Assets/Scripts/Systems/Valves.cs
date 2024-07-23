using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valves : MonoBehaviour
{
    [Range(0, 360)] public float currentRotation = 180;
    public float give = 10;
    public float rotationSpeed = 5;

    public bool gameOver = false;

    [SerializeField] private GameObject currentPressureIndicator;
    [SerializeField] private GameObject currentObjectiveIndicator;

    private float currentGoal = 180f;

    private void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            currentRotation -= Time.deltaTime * rotationSpeed;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentRotation += Time.deltaTime * rotationSpeed;
        }

        if (currentRotation < 0)
            currentRotation = 0;

        if (currentRotation > 360)
            currentRotation = 360;

        currentPressureIndicator.gameObject.transform.SetPositionAndRotation(currentPressureIndicator.gameObject.transform.position, 
            new Quaternion(currentPressureIndicator.gameObject.transform.rotation.x, currentRotation, currentPressureIndicator.gameObject.transform.rotation.z, currentPressureIndicator.gameObject.transform.rotation.w));
    }

    public IEnumerator RandomSet()
    {
        while(!gameOver)
        {
            currentGoal = Random.Range(1, 36000) / 100;
            currentObjectiveIndicator.gameObject.transform.SetPositionAndRotation(currentObjectiveIndicator.gameObject.transform.position, 
                new Quaternion(currentObjectiveIndicator.gameObject.transform.rotation.x, currentGoal, currentObjectiveIndicator.gameObject.transform.rotation.z, currentObjectiveIndicator.gameObject.transform.rotation.w));
            yield return new WaitForSeconds(Random.Range(0f, 50f));
        }
    }

}
