using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    Vector2 inputdir;

    private Gamepad _gamepad;

    // Start is called before the first frame update
    void Start()
    {
        if (Gamepad.current != null)
        {
            _gamepad = Gamepad.current;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDpadNav(InputValue value)
    {
        bool up = false;
        bool down = false;
        bool left = false;
        bool right = false;

        inputdir = value.Get<Vector2>();

        if (inputdir == Vector2.up)
        {
            Debug.Log("Up pressed");
            up = true;
        }
        else if (inputdir == Vector2.down)
        {
            Debug.Log("Down pressed");
            down = true;
        }
        else if (inputdir == Vector2.left)
        {
            Debug.Log("Left pressed");
            left = true;
        }
        else if (inputdir == Vector2.right)
        {
            Debug.Log("Right pressed");
            right = true;
        }
    }

    private void OnButtonA()
    {
        Debug.Log("A pressed");
    }

    private void OnButtonB()
    {
        Debug.Log("B pressed");
    }

    private void OnButtonX()
    {
        Debug.Log("X pressed");
    }

    private void OnButtonY()
    {
        Debug.Log("Y pressed");
    }

    private void OnButtonLB()
    {
        Debug.Log("LB pressed");
    }

    private void OnButtonRB()
    {
        Debug.Log("RB pressed");
    }
}
