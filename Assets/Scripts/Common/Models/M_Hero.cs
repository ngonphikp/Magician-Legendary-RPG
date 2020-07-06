using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Data;

[System.Serializable]
public class M_Hero : M_Character
{
    public int def = 1;
    public int hp = 1;
    public int atk = 1;
    public float crit = 1.0f;
    public float dodge = 1.0f;    

    public M_Hero()
    {

    }

    public void UpdateById(string id = null)
    {
        if (id == null) id = this.id;
        else this.id = id;
        M_Hero heroInConfig = GameManager.instance.herosDic[id];
        this.name = heroInConfig.name;
        this.element = heroInConfig.element;
        this.def = heroInConfig.def;
        this.hp = heroInConfig.hp;
        this.crit = heroInConfig.crit;
        this.dodge = heroInConfig.dodge;
        this.atk = heroInConfig.atk;
    }
}
