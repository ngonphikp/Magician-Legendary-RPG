using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Giúp các lớp kế thừa dễ dàng truy cập vào thành phần Animator
[RequireComponent(typeof(Animator))]
public class Dynamic2DObject : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
