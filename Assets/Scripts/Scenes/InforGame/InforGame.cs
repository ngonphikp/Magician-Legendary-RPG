using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InforGame : MonoBehaviour
{
    [SerializeField]
    private Transform posCharacter = null;
    [SerializeField]
    private Image[] imgSkills = null;
    [SerializeField]
    private C_Profile profile = null;

    private int index = 0;
    private C_Character hero;
    private M_Character nhanVat;

    private void Start()
    {
        index = GameManager.instance.idxCharacter;
        LoadCharacter();
    }

    private void LoadCharacter()
    {
        nhanVat = GameManager.instance.nhanVats[index];

        foreach (Transform child in posCharacter)
        {
            Destroy(child.gameObject);
        }

        GameObject heroAs = Resources.Load("Prefabs/Character/" + nhanVat.id_cfg, typeof(GameObject)) as GameObject;

        // Test
        if (heroAs == null) heroAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject heroObj = Instantiate(heroAs, posCharacter);
            hero = heroObj.GetComponent<C_Character>();

            nhanVat.Current_ep = nhanVat.max_ep = 100;
            nhanVat.Current_hp = nhanVat.max_hp = nhanVat.hp;

            hero.Set(nhanVat, false);
        }

        for (int i = 0; i < GameManager.instance.herosDic[nhanVat.id_cfg].skills.Count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[nhanVat.id_cfg].skills[i]);

            if(sprite != null) imgSkills[i].sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[nhanVat.id_cfg].skills[i]);
        }

        profile.set(nhanVat);
    }

    public void SkillHero(int anim)
    {
        hero.Play(anim);
    }

    public void Next()
    {
        index++;
        if (index >= GameManager.instance.nhanVats.Count) index = 0;
        LoadCharacter();
    }

    public void Previous()
    {
        index--;
        if (index <= -1) index = GameManager.instance.nhanVats.Count - 1;
        LoadCharacter();
    }

    public void UpLevel()
    {
        C_Util.GetDumpObject(nhanVat);
    }
}
