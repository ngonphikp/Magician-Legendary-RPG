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

    private List<M_NhanVat> nhanVats = new List<M_NhanVat>();
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

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            C_HeroAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_HeroAvEl>();
            heroAv.set(GameManager.instance.nhanVats[i]);

            nhanVats.Add(GameManager.instance.nhanVats[i]);

            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                if (countActive >= C_Params.maxActive) break;

                heroAv.Active();

                teamL[GameManager.instance.nhanVats[i].idx].set(GameManager.instance.nhanVats[i], canvas);

                countActive++;
            }

            Objs.Add(GameManager.instance.nhanVats[i].id_nv, heroAv);
        }

        Debug.Log("Count: " + Objs.Keys.Count);

        await Task.Yield();
    }

    public void Active(M_NhanVat nhanVat)
    {
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.nhanVat.idx == -1)
            {
                nhanVat.idx = i;
                teamL[i].set(nhanVat, canvas);
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
        List<M_NhanVat> nhanVats = new List<M_NhanVat>();

        // Update index hero in teamL
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.nhanVat.idx != -1)
            {
                teamL[i].content.nhanVat.idx = i;

                nhanVats.Add(teamL[i].content.nhanVat);
            }
        }

        // Update index hero in list
        foreach (C_HeroAvEl el in Objs.Values)
        {
            if (!el.isActive)
            {
                el.nhanVat.idx = -1;

                nhanVats.Add(el.nhanVat);
            }
        }

        //heros.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.idx));        

        UserSendUtil.sendArrange(nhanVats);

        GameManager.instance.nhanVats = nhanVats;

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
