using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_NhanVat
{
    public int id_nv = -1;
    public string id_cfg;
    public int id_tk;

    public int lv;
    public string name;
    public string element;

    public int idx = -1;

    public int current_ep = 0, max_ep = 1;
    public int current_hp = 0, max_hp = 1;

    public C_Enum.CharacterType type = C_Enum.CharacterType.Hero;

    public M_NhanVat()
    {

    }

    public M_NhanVat(int id_nv, string id_cfg, int id_tk, int lv, int idx)
    {
        this.id_nv = id_nv;
        this.id_cfg = id_cfg;
        this.id_tk = id_tk;
        this.lv = lv;
        this.idx = idx;
    }

    public M_NhanVat(ISFSObject obj)
    {
        if (obj == null) return;

        this.id_nv = obj.GetInt("id_nv");
        this.id_cfg = obj.GetUtfString("id_cfg");
        this.id_tk = obj.GetInt("id_tk");
        this.lv = obj.GetInt("lv");
        this.idx = obj.GetInt("idx");
    }
}
