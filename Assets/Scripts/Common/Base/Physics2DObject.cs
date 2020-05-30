using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Giúp các lớp kế thừa dễ dàng truy cập vào thành phần Rigidbody2D
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Physics2DObject : MonoBehaviour
{
    [HideInInspector]
    public new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
