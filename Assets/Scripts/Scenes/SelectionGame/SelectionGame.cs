﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
    public static SelectionGame instance = null;

    [Header("Vào Game")]
    [SerializeField]
    private InputField ipfTenNhanVat = null;
    [SerializeField]
    private Text txtNoti = null;

    [Header("Chọn Magician")]
    [SerializeField]
    private string[] idHeros = new string[5];
    [SerializeField]
    private Transform posMagician = null;

    private int idxActive = 0;
    private C_Hero hero = null;
    private string tenNhanVat = "";

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    private void Start()
    {
        LoadHero(idHeros[idxActive]);
    }

    private void LoadHero(string id)
    {
        foreach (Transform child in posMagician)
        {
            Destroy(child.gameObject);
        }
        GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Hero/" + id);
        if (heroAs != null)
        {
            GameObject heroObj = Instantiate(heroAs, posMagician);
            hero = heroObj.GetComponent<C_Hero>();
        }
    }


    public void VaoGame()
    {
        tenNhanVat = ipfTenNhanVat.text;

        Debug.Log("====================Vào Game: " + tenNhanVat + " / " + idHeros[idxActive]);

        txtNoti.text = "Vào Game thành công";

        UserSendUtil.sendSelection(tenNhanVat, idHeros[idxActive]);
    }

    public void RecSelection(List<M_NhanVat> lstNhanVat)
    {
        Debug.Log("====================RecSelection");

        //lstNhanVat.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));

        if (lstNhanVat.Count > 0)
        {           
            GameManager.instance.listHero.Clear();
            for (int i = 0; i < lstNhanVat.Count; i++)
            {
                M_Hero hero = new M_Hero(lstNhanVat[i]);
                hero.UpdateById();

                GameManager.instance.listHero.Add(hero);
            }

            GameManager.instance.taikhoan.name = tenNhanVat;

            ScenesManager.instance.ChangeScene("HomeGame");
        }
    }

    public void ChangeHero(int idx)
    {
        if (idx == idxActive) return;
        idxActive = idx;
        LoadHero(idHeros[idxActive]);
    }

    public void SkillHero(int anim)
    {
        hero.Play(anim);
    }
}
