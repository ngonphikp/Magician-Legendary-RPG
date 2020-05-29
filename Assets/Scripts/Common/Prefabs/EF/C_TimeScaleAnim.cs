using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TimeScaleAnim : MonoBehaviour
{
    [SerializeField]
    private bool isUpdate = false;

    private Animation anim;
    private Animator animator;

    private void Start()
    {
        anim = GetComponent<Animation>();        
        animator = GetComponent<Animator>();
        Change();
    }

    public void Change()
    {
        if(anim != null && GameManager.instance)
        {
            foreach (AnimationState state in anim)
            {
                state.speed = GameManager.instance.myTimeScale;
            }
        }
        if(animator != null && GameManager.instance)
        {
            animator.speed = GameManager.instance.myTimeScale;
        }
    }

    private void Update()
    {
        if (isUpdate) Change();
    }
}
