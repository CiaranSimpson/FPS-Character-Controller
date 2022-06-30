using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{


    CharacterController charController;


    Vector2 moveDir;
    public float moveSpeed = 1;

    public bool isWalking;
    bool isSprinting = false;

    Vector3 velocity;

    public float jumpHeight = 3;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    public void GetMoveDir(InputAction.CallbackContext context)
    {
        //gets the currently inputted movement direction
        moveDir = context.ReadValue<Vector2>();

    }

    private void Update()
    {
        //if player is on the ground, velocity is 0
        if (charController.isGrounded)
        {
            velocity.y = 0;
        }

        handleSprinting();

        HandleWalking();

        HandleJumping();

        velocity.y += Physics.gravity.y * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);

    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && charController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    private void handleSprinting()
    {
        //new input does not currently support holding down a key. Hence using the old input system in such cases.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void HandleWalking()
    {
        // if movedir's values are signficant enough that the player can be said to be attempting to move
        if ((moveDir.x < 0.1f && moveDir.y < 0.1f) && (moveDir.x > -0.1f && moveDir.y > -0.1f))
        {
            isWalking = false;
        }
        else
        {
            //gets movement direction 
            Vector3 move = transform.right * moveDir.x + transform.forward * moveDir.y;
            if (!isSprinting)
            {
                charController.Move(move * moveSpeed * Time.deltaTime);
            }
            else
            {
                charController.Move(move * (moveSpeed + 8) * Time.deltaTime);
            }

            isWalking = true;
        }
    }

    

}
