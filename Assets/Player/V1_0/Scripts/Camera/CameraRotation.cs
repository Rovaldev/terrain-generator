using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //Public variables
    public float sensitivity;
    public Camera camera;
    public bool canRotate = true;

    //Private variables
    private float xRotation = 0;

    private void Update()
    {
        if (canRotate)
        {
            float _horizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float _vertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            //Rotate the player an camera's x axis
            transform.Rotate(Vector3.up * _horizontal);
            xRotation -= _vertical;

            //Clamps the rotation to avoid looking more tha the possible angle
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
