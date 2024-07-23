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
            if (camera.transform.rotation.y > 90 && camera.transform.rotation.y < 269)
                camera.transform.Rotate(0, -30 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (camera.transform.rotation.y > 89 && camera.transform.rotation.y < 270)
                camera.transform.Rotate(0, 30 * Time.deltaTime, 0);
        }
    }
}
