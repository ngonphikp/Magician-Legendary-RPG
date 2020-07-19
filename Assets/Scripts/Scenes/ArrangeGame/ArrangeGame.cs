using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeGame : MonoBehaviour
{
    public static ArrangeGame instance = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private Transform listHeroAv = null;
    [SerializeField]
    private GameObject heroAvEl = null;

    [SerializeField]
    private Animator arrange = null;
    [SerializeField]
    private C_CellAT[] teamL = null;
    [SerializeField]
    private C_CellAT[] teamR = null;

    [SerializeField]
    private Button btnSave = null;
    [SerializeField]
    private Button btnFighting = null;

    private List<M_Hero> listHero = new List<M_Hero>();
    public int countActive = 0;

    public Dictionary<int, C_HeroAvEl> Objs = new Dictionary<int, C_HeroAvEl>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        btnFighting.gameObject.SetActive(GameManager.instance.isAttack);
        btnSave.gameObject.SetActive(!GameManager.instance.isAttack);

        LoadListHero();

        LoadAnim(true);
    }

    private void LoadAnim(bool v)
    {
        arrange.SetBool("isArrange", v);

        for (int i = 0; i < teamL.Length; i++) teamL[i].GetComponent<Animator>().SetBool("isArrange", v);
        for (int i = 0; i < teamR.Length; i++) teamR[i].GetComponent<Animator>().SetBool("isArrange", v);
    }

    private async void LoadListHero()
    {
        Objs.Clear();

        for (int i = 0; i < GameManager.instance.listHero.Count; i++)
        {
            C_HeroAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_HeroAvEl>();
            heroAv.setHero(GameManager.instance.listHero[i]);

            listHero.Add(GameManager.instance.listHero[i]);

            if (GameManager.instance.listHero[i].idx != -1)
            {
                if (countActive >= C_Params.maxActive) break;

                heroAv.Active();

                teamL[GameManager.instance.listHero[i].idx].setHero(GameManager.instance.listHero[i], canvas);

                countActive++;
            }

            Objs.Add(GameManager.instance.listHero[i].id_nv, heroAv);
        }

        Debug.Log("Count: " + Objs.Keys.Count);

        await Task.Yield();
    }

    public void Active(M_Hero hero)
    {
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.hero.idx == -1)
            {
                hero.idx = i;
                teamL[i].setHero(hero, canvas);
                countActive++;

                return;
            }
        }
    }

    public void Fighting()
    {
        iSave();
    }

    public void Save()
    {
        iSave();            
    }

    private void iSave()
    {
        List<M_Hero> heros = new List<M_Hero>();

        // Update index hero in teamL
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.hero.idx != -1)
            {
                teamL[i].content.hero.idx = i;

                heros.Add(teamL[i].content.hero);
            }
        }

        // Update index hero in list
        foreach (C_HeroAvEl el in Objs.Values)
        {
            if (!el.isActive)
            {
                el.hero.idx = -1;

                heros.Add(el.hero);
            }
        }

        //heros.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.idx));        

        UserSendUtil.sendArrange(heros);

        GameManager.instance.listHero = heros;

        if (GameManager.instance.demo) RecArrange();
    }

    public async void RecArrange()
    {
        LoadAnim(false);

        await Task.Delay(1000);
        if (GameManager.instance.isAttack) ScenesManager.instance.ChangeScene("FightingGame");
        else ScenesManager.instance.ChangeScene("HomeGame");
    }
}
