using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMotor : MonoBehaviour
{
    //Variables publicas
    public float gravity;

    //Variables privadas
    private CharacterController character;
    private Vector3 target = Vector3.zero;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Gravity
        target.y -= gravity * Time.deltaTime;

        //Move
        character.Move(target * Time.deltaTime);
    }

    //Move method
    public void Move(Vector3 _target, float _speed)
    {
        target = _target * _speed + new Vector3(0, target.y, 0);
    }

    //Jump method
    public void Jump(float _force)
    {
        target.y = _force;
    }

    //Is the gameobject grounded?
    public bool isGrounded
    {
        get 
        { 
            return character.isGrounded; 
        }
    }
}
