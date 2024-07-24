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

        if (currentRotation < -135)
            currentRotation = -135;

        if (currentRotation > 135)
            currentRotation = 135;

        currentPressureIndicator.gameObject.transform.SetPositionAndRotation(currentPressureIndicator.gameObject.transform.position, 
            new Quaternion(currentRotation, currentPressureIndicator.gameObject.transform.rotation.y, currentPressureIndicator.gameObject.transform.rotation.z, currentPressureIndicator.gameObject.transform.rotation.w));
    }

    private void Start()
    {
        StartCoroutine(RandomSet());
    }
    
    public IEnumerator RandomSet()
    {
        while(!gameOver)
        {
            currentGoal = Random.Range(-13500, 13500) / 100;
            currentObjectiveIndicator.gameObject.transform.SetPositionAndRotation(currentObjectiveIndicator.gameObject.transform.position, 
                new Quaternion(currentGoal,  currentPressureIndicator.gameObject.transform.rotation.y, currentObjectiveIndicator.gameObject.transform.rotation.z, currentObjectiveIndicator.gameObject.transform.rotation.w));
            yield return new WaitForSeconds(Random.Range(0f, 50f));
        }
    }

}
