﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class HomeGame : MonoBehaviour
{
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;
    [SerializeField]
    private Transform bagHero = null;
    [SerializeField]
    private GameObject bagEl = null;

    public void OutGame()
    {
        LoginSendUtil.sendLogout();
    }

    private void Start()
    {
        FilterListHero();
        LoadBagHero();
    }

    private async void FilterListHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                C_CharacterAcEl heroAc = Instantiate(heroAcEl, listHeroAc).GetComponent<C_CharacterAcEl>();

                M_Character nhanVat = GameManager.instance.nhanVats[i];
                nhanVat.max_hp = 1;
                nhanVat.Current_hp = 1;
                nhanVat.max_ep = 1;
                nhanVat.Current_ep = 1;

                heroAc.set(nhanVat);
            }                
        }

        await Task.Yield();
    }

    private async void LoadBagHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            C_BagEl heroBag = Instantiate(bagEl, bagHero).GetComponent<C_BagEl>();
            heroBag.set(GameManager.instance.nhanVats[i]);
        }

        await Task.Yield();
    }
}
