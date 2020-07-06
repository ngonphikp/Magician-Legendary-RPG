using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TimeScalePS : MonoBehaviour
{
    [SerializeField]
    private bool isUpdate = false;

    private ParticleSystem ps;
    private float speed;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        speed = ps.main.simulationSpeed;
        Change();
    }

    private void Change()
    {
        try
        {
            if (ps != null)
            {
                var main = ps.main;
                main.simulationSpeed = speed * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
            }
        }
        catch (System.Exception)
        {
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
