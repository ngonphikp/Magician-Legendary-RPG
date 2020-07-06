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
        if(anim != null)
        {
            foreach (AnimationState state in anim)
            {
                state.speed = ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
            }
        }
        if(animator != null)
        {
            animator.speed = ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
        }
    }

    private void Update()
    {
        if (isUpdate) Change();
    }
}
