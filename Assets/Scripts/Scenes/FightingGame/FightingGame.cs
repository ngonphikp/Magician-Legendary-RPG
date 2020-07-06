using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MonoBehaviour
{
    public static FightingGame instance = null;

    public float myTimeScale = 1.0f;
    public bool isScaleTime = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ScaleTime()
    {
        isScaleTime = !isScaleTime;
        myTimeScale = (isScaleTime) ? 2.0f : 1.0f;
    }
}
