using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject camera;

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            print(camera.transform.rotation.y);
                camera.transform.Rotate(0, -30 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
                camera.transform.Rotate(0, 30 * Time.deltaTime, 0);
        }
    }
}
