using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;

    public void GetMousePos (InputAction.CallbackContext context)
    {
        mousePos = (context.ReadValue<Vector2>() / 2);
        //print(mousePos);
    }

    
    void Update()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        UpdateX();
        UpdateY();
    }

    void UpdateX()
    {
        cam.transform.Rotate(new Vector3(-mousePos.y, 0, 0), Space.Self);
    }

    void UpdateY ()
    {
        transform.Rotate(new Vector3(0, mousePos.x, 0), Space.World);
    }
}
