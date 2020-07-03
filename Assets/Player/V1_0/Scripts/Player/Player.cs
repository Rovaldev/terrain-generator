using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public variables
    public float speed = 10f;
    public float jumpForce = 100f;

    //Private variables
    private MovementMotor motor;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        motor = GetComponent<MovementMotor>();
    }

    private void Update()
    {
        Movement();  
    }

    //Movement method
    private void Movement()
    {
        //Calculate the axis of movement
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        Vector3 _direction = transform.right * _horizontal + transform.forward * _vertical;

        //Move the player
        motor.Move(_direction, speed);

        //If is is grounded then can jump
        if (motor.isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
                motor.Jump(jumpForce);
    }
}
