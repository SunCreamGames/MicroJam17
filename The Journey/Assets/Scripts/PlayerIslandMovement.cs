using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIslandMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    CharacterController2D characterController;

    bool fly, crouch, jump;
    float movementX, movementY;

    void Start()
    {
        fly = crouch = jump = false;
    }

    void Update()
    {
        var horizontalIput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Jump"))
            jump = true;


        if (verticalInput > 0)
        {
            fly = true;
            crouch = false;
        }
        else if (verticalInput < 0)
        {
            fly = false;
            crouch = true;
        }
        else
        {
            fly = crouch = false;
        }
        movementX = horizontalIput;
        movementY = verticalInput;
    }

    private void FixedUpdate()
    {
        characterController.Move(movementX * speed * Time.fixedDeltaTime, movementY * speed * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
