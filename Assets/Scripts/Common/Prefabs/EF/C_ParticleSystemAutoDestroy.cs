using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_ParticleSystemAutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float time = 0.0f;

    private ParticleSystem ps;
    private float t = 0.0f;

    [Obsolete]
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        if(ps != null) t = ps.duration + ps.startDelay;
    }

    private void Start()
    {
        if (time > 0.0f && time > t) t = time;
        AutoDestroy();        
    }

    private async void AutoDestroy()
    {
        await Task.Delay(TimeSpan.FromSeconds(t / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
        Destroy(gameObject);
    }
}
