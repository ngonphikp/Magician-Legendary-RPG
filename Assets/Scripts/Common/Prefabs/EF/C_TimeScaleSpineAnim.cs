using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TimeScaleSpineAnim : MonoBehaviour
{
    [SerializeField]
    private bool isUpdate = false;

    private SkeletonAnimation anim;

    private void Start()
    {
        anim = this.GetComponent<SkeletonAnimation>();
    }

    private void Change()
    {
        if(anim != null && GameManager.instance != null)
        {
            anim.timeScale = GameManager.instance.myTimeScale;
        }
    }

    private void Update()
    {
        if (isUpdate) Change();
    }

    private void OnEnable()
    {
        Change();
    }
}
