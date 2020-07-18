using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Data;

[System.Serializable]
public class M_Hero : M_NhanVat
{
    public int def = 1;
    public int hp = 1;
    public int atk = 1;
    public float crit = 1.0f;
    public float dodge = 1.0f;    

    public M_Hero()
    {

    }

    public M_Hero(M_NhanVat nhanvat)
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
        M_Hero heroInConfig = GameManager.instance.herosDic[id_cfg];
        this.name = heroInConfig.name;
        this.element = heroInConfig.element;
        this.def = heroInConfig.def;
        this.hp = heroInConfig.hp;
        this.crit = heroInConfig.crit;
        this.dodge = heroInConfig.dodge;
        this.atk = heroInConfig.atk;
    }

    public ISFSObject parse()
    {
        ISFSObject obj = new SFSObject();

        obj.PutInt("id_nv", this.id_nv);
        obj.PutUtfString("id_cfg", this.id_cfg);
        obj.PutInt("id_tk", this.id_tk);
        obj.PutInt("lv", this.lv);
        obj.PutInt("idx", this.idx);

        return obj;
    }
}
