using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
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
        string tennhanvat = ipfTenNhanVat.text;

        Debug.Log("====================Vào Game: " + tennhanvat + " / " + idHeros[idxActive]);

        txtNoti.text = "Vào Game thành công";

        ScenesManager.instance.ChangeScene("HomeGame");
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
