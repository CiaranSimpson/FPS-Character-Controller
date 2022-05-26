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

    public void GetMousePos(InputAction.CallbackContext context)
    {
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

    void GetValues()
    {
        rotY = inputY;
        rotX += -inputX;
        rotX = Mathf.Clamp(rotX, minX, maxX);
    }

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
