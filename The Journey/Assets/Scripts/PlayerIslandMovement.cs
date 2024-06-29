using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIslandMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    CharacterController2D _characterController;

    bool fly, crouch, jump;
    float movementX;

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
    }

    private void FixedUpdate()
    {
        _characterController.Move(movementX * speed * Time.fixedDeltaTime, crouch, jump, fly);
        jump = false;
    }
}
