using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DisplayPanel : MonoBehaviour
{
    private Animator animator = null;

    private void Awake()
    {
        this.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        animator.SetBool("isActive", true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        animator.SetBool("isActive", false);
    }
}
