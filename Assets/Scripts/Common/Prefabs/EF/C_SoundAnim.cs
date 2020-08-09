using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SoundAnim : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip = null;

    private void Start()
    {
        SoundManager.instance.PlayOneShotAs2(audioClip, (FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
    }
}
