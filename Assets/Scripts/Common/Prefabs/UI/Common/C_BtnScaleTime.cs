using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BtnScaleTime : MonoBehaviour
{
    public void ScaleTime()
    {
        if (FightingGame.instance) FightingGame.instance.ScaleTime();
    }
}
