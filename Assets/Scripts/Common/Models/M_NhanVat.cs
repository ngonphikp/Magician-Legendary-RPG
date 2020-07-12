using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_NhanVat
{
    public int id_nv;
    public string id_cfg;
    public int id_tk;

    public int lv;
    public string name;
    public string element;

    public C_Enum.CharacterType type = C_Enum.CharacterType.Hero;

    public M_NhanVat()
    {

    }

    public M_NhanVat(ISFSObject obj)
    {
        if (obj == null) return;

        this.id_nv = obj.GetInt("id_nv");
        this.id_cfg = obj.GetUtfString("id_cfg");
        this.id_tk = obj.GetInt("id_tk");
        this.lv = obj.GetInt("lv");
    }
}
