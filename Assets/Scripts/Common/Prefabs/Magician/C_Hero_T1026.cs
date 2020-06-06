using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_Hero_T1026 : MonoBehaviour
{
    [Header("Anim2")]
    [SerializeField]
    private float time2d0 = 1.0f;
    [SerializeField]
    private GameObject fx2d0 = null;

    [Header("Anim3")]
    [SerializeField]
    private float time3d0 = 0.8f;
    [SerializeField]
    private float time3db = 0.1f;
    [SerializeField]
    private GameObject bl3d0 = null;
    [SerializeField]
    private Transform posB3 = null;
    [SerializeField]
    private Vector3 offset3 = new Vector3();

    [Header("Anim4")]
    [SerializeField]
    private float time4d0 = 1.0f;
    [SerializeField]
    private GameObject fx4d0 = null;

    [Header("Anim5")]
    [SerializeField]
    private float time5dd = 0.5f;
    [SerializeField]
    private float time5da = 0.6f;
    [SerializeField]
    private float time5d0 = 1.0f;
    [SerializeField]
    private float time5d1 = 1.0f; // Delay hit
    [SerializeField]
    private float time5ds = 1.0f; // Delay start knock
    [SerializeField]
    private float time5df = 1.0f; // Delay finish knock
    [SerializeField]
    private float time5dm = 1.0f; // Time knock
    [SerializeField]
    private Vector3 offset5 = new Vector3(); // Offset Knock
    [SerializeField]
    private GameObject fx5d0 = null;

    private C_Hero hero;

    private void Awake()
    {
        hero = this.GetComponent<C_Hero>();
        preUpdate();
    }
    private async void preUpdate()
    {
        while (true)
        {
             if (PlayGame.instance && hero.isCombat && PlayGame.instance.targets.Count > 0)
            {
                if (hero.isAnim2)
                {
                    Anim2();
                }
                if (hero.isAnim3)
                {
                    Anim3();
                }
                if (hero.isAnim4)
                {
                    Anim4();
                }
                if (hero.isAnim5)
                {
                    Anim5();
                }
                if (hero.isAnim6)
                {
                    Anim6();
                }
                if (hero.isAnim7)
                {
                    Anim7();
                }
            }
            await Task.Yield();
        }
    }

    private void Anim2()
    {
        Debug.Log(gameObject.name + "Anim 2");
        C_LibSkill.FxHit(PlayGame.instance.targets, fx2d0, time2d0, false);
    }

    private void Anim3()
    {
        Debug.Log(gameObject.name + "Anim 3");
        C_LibSkill.Shoot(bl3d0, posB3, PlayGame.instance.targets, true, time3d0, time3db, offset3);
    }

    private void Anim4()
    {
        Debug.Log(gameObject.name + "Anim 4");
        C_LibSkill.FxHit(PlayGame.instance.targets, fx4d0, time4d0, false);
    }

    private void Anim5()
    {
        Debug.Log(gameObject.name + "Anim 5");

        C_LibSkill.DarkScreen(this.gameObject, time5dd, time5da, PlayGame.instance.targets);

        Vector3 G = C_LibSkill.GHero(PlayGame.instance.targets, this.transform);
        C_LibSkill.FxHitOne(PlayGame.instance.targets, this.transform, G, fx5d0, time5d0, true, time5d1, 1, time5ds, time5df, time5dm, offset5);
    }

    private void Anim6()
    {
        Debug.Log(gameObject.name + "Anim 6");
    }

    private void Anim7()
    {
        Debug.Log(gameObject.name + "Anim 7");
    }
}
