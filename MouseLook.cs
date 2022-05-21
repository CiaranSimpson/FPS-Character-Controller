using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;

    public float upClamp, downClamp;

    [Range(1,10)] public float sensitivity = 2;

    public void GetMousePos (InputAction.CallbackContext context)
    {
        mousePos = ((context.ReadValue<Vector2>() / 2) * sensitivity);
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
        float desiredRotation = -mousePos.y;
        float finalRotation = desiredRotation;

        bool clamp = CiaransLibrary.Clamp(cam.transform.rotation.x, upClamp, -downClamp);
        //print(clamp);
        if (!clamp)
        {
            cam.transform.Rotate(new Vector3(finalRotation, 0, 0), Space.Self);
        }
        else
        {
            
        }

        
        //Debug.Log(desiredRotation.ToString() + " " + finalRotation.ToString());
    }

    void UpdateY()
    {
        transform.Rotate(new Vector3(0, mousePos.x, 0), Space.World);
    }

    

}
