using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AutoRotate : Physics2DObject
{
    [SerializeField]
    private float rotationSpeed = 5;

    [SerializeField]
    private bool isRotate = true;

    public bool IsRotate { get => isRotate; set { isRotate = value; } }

    private float currentRotation;

    private void FixedUpdate()
    {
        if (isRotate)
        {
            currentRotation += .02f * rotationSpeed * 10f;
            rigidbody2D.MoveRotation(-currentRotation);
        }        
    }
}
