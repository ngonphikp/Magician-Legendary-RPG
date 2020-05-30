using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Giúp các lớp kế thừa dễ dàng truy cập vào thành phần Animator + Rigidbody2D
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class DynPhy2DObject : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public new Rigidbody2D rigidbody2D;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
