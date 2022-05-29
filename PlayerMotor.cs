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

    public enum Mode
    {
        walking, frozen
    }

    public Mode movementMode;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        //player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        Move();
    }

    public void GetMoveDir (InputAction.CallbackContext context)
    {
         moveDir = context.ReadValue<Vector2>();
        
    }

    public void Jump (InputAction.CallbackContext context)
    {
        print(1);
        if (context.phase == InputActionPhase.Started)
        {
            print(2);
            rigidBody.AddForce(transform.up * 10);
        }
    }

    void Move()
    {
        if (movementMode == Mode.walking)
        {
            HandleWalking();
        }

    }

    void ApplyGravity()
    {

        charController.SimpleMove(-Vector3.down * 1.3f);
    }

    void HandleWalking()
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
    }

    
}
