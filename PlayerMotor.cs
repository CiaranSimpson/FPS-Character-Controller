using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{

    Rigidbody rigidBody;
    CharacterController charController;
    //Player player;

    Vector2 moveDir;

    public float forwardSpeed, sidewaysSpeed, backwardsSpeed;
    public float jumpHeight = 1;
    bool isJumping;
    bool isGrounded;
    float gravityValue = -1.7f;

    //Camera cam;

   
    public enum Mode
    {
        walking,freeCam, frozen
    }

    public Mode movementMode;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        //player = GetComponent<Player>();
        //cam = Camera.main;
    }

    private void FixedUpdate()
    {
        //determine if player is on ground or not
        isGrounded = charController.isGrounded;

        //if not in freecam mode, apply gravity
        if (movementMode != Mode.freeCam)
        {
            ApplyGravity();
        }
        //move the player
        Move();
    }

    public void GetMoveDir(InputAction.CallbackContext context)
    {
        //gets the currently inputted movement direction
        moveDir = context.ReadValue<Vector2>();

    }

    public void jumpEvent(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (isGrounded)
            {
                //start jumping, then stop the jumping .1 of a second later.
                isJumping = true;
                Invoke("killJump", 0.1f);
            }
        }

    }

     void killJump()
    {
        isJumping = false;
    }

    

    void Move()
    {
        // if in walking mode
        if (movementMode == Mode.walking)
        {
            //moves the character in whatever direction your inputting, moving at a specified speed
            HandleWalking(forwardSpeed,sidewaysSpeed,backwardsSpeed, jumpHeight);
        } 

    }

    void ApplyGravity()
    {
        //pull the player down by specified speed
        charController.SimpleMove(Vector3.down * 1.3f);
    }

    void HandleWalking(float forwardSpeed,float sidewaysSpeed, float backwardsSpeed, float jumpHeight)
    {
        if (moveDir.y > 0.01)
        {
            charController.Move(transform.forward * (forwardSpeed));
        }
        else if (moveDir.y < -0.01)
        {
            charController.Move(-transform.forward * (backwardsSpeed));
        }

        if (moveDir.x > 0.01)
        {
            charController.Move(transform.right * (sidewaysSpeed));
        }
        else if (moveDir.x < -0.01)
        {
            charController.Move(-transform.right * (sidewaysSpeed));
        }

        if (isJumping)
        {
            //Jump
            Vector3 newVelocity = new Vector3();
            newVelocity.y += Mathf.Sqrt((jumpHeight / 100) * -3.0f * gravityValue);
            newVelocity.y += gravityValue * Time.deltaTime;
            charController.Move(newVelocity);
        }

    }

    

}
