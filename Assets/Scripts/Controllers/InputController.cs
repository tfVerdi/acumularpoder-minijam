using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Input booleans")]
    public bool isWPressed;
    public bool isAPressed;
    public bool isSPressed;
    public bool isDPressed;
    public bool isSpacePressed;
    private float ErrorTolerance = 0.05f;
    public float horizontalAxisValue;
    public float verticalAxisValue;
    
    void FixedUpdate()
    {
        horizontalAxisValue = Input.GetAxis("Horizontal");
        verticalAxisValue = Input.GetAxis("Vertical");
        ParseMovementInput(verticalAxisValue, horizontalAxisValue);
        isSpacePressed = Input.GetKey(KeyCode.Space);
    }

    void ParseMovementInput(float verticalAxisValue, float horizontalAxisValue) {
        if(verticalAxisValue > ErrorTolerance) {
            isWPressed = true;
            isSPressed = false;
        } else if(verticalAxisValue < -ErrorTolerance) {
            isSPressed = true;
            isWPressed = false;
        } else {
            isWPressed = false;
            isSPressed = false;
        }

        if(horizontalAxisValue > ErrorTolerance) {
            isDPressed = true;
            isAPressed = false;
        } else if(horizontalAxisValue < -ErrorTolerance) {
            isAPressed = true;
            isDPressed = false;
        } else {
            isDPressed = false;
            isAPressed = false;
        }
    }
}
