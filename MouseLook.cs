using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    float inputY, inputX, rotY, rotX;
    public Camera cam;

    [Range (0,90)] public float maxX, minX;

    [Range(1, 10)] public float Xsensitivity = 2;
    [Range(1, 10)] public float Ysensitivity = 2;

    public CursorLockMode cursorMode = CursorLockMode.Locked;
    
    //Gets the movement of the mouse, and stores it in two seperate variables
    public void GetMousePos(InputAction.CallbackContext context)
    {
    //gets value from input-system, divides it by 2 to slow it down, and then times it by sensitivity
        inputY = ((context.ReadValue<Vector2>().x / 2) * Ysensitivity);
        inputX = ((context.ReadValue<Vector2>().y / 2) * Xsensitivity);
        //print(mousePos);
    }


    void Update()
    {
        GetValues();
        UpdateCamera();
        ManageCursor();

    }
    
    //converts mouse-movement into usable numbers that can be used for rotation, as well as clamping the X
    void GetValues()
    {
        rotY = inputY;
        rotX += -inputX; // the += is imperative - otherwise clamping will not work because the values will never actually be large enough
        
        //clamps the X. 
        rotX = Mathf.Clamp(rotX, minX, maxX);
    }
    
    //rotates the camera to match the rotation values
    void UpdateCamera()
    {
        transform.Rotate(new Vector3(0, rotY, 0), Space.World);

        cam.transform.localRotation = Quaternion.Euler(rotX, cam.transform.rotation.y, 0);
    }

    void ManageCursor()
    {
        Cursor.lockState = cursorMode;

    }



}
