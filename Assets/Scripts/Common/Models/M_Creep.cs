using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Data;

[System.Serializable]
public class M_Creep : M_NhanVat
{
    public int def = 1;
    public int hp = 1;
    public int atk = 1;
    public float crit = 1.0f;
    public float dodge = 1.0f;    

    public M_Creep()
    {

    }

    public M_Creep(M_NhanVat nhanvat)
    {
        this.id_cfg = nhanvat.id_cfg;
        this.id_nv = nhanvat.id_nv;
        this.id_tk = nhanvat.id_tk;
        this.lv = nhanvat.lv;
        this.idx = nhanvat.idx;
    }

    public void UpdateById(string id_cfg = null)
    {
        if (id_cfg == null) id_cfg = this.id_cfg;
        else this.id_cfg = id_cfg;
        M_Creep creepInConfig = GameManager.instance.creepsDic[id_cfg];
        this.name = creepInConfig.name;
        this.element = creepInConfig.element;
        this.def = creepInConfig.def;
        this.hp = creepInConfig.hp;
        this.crit = creepInConfig.crit;
        this.dodge = creepInConfig.dodge;
        this.atk = creepInConfig.atk;
    }
}
