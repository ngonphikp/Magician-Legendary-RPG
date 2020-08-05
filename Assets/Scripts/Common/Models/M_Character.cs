using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Character
{
    public int id_nv = -1;
    public string id_cfg;
    public int id_tk;

    public int def = 1;
    public int hp = 1;
    public int atk = 1;
    public float crit = 1.0f;
    public float dodge = 1.0f;

    public int lv;
    public string name;
    public string element;
    public int star;

    public int idx = -1;

    public bool isDie = false;

    [SerializeField]
    private int current_ep = 0;
    public int max_ep = 1;
    [SerializeField]
    private int current_hp = 0;
    public int max_hp = 1;

    public int Current_ep
    {
        get => current_ep; set
        {
            if (value < 0) value = 0; if (value > max_ep) value = max_ep;
            current_ep = value;
        }
    }
    public int Current_hp
    {
        get => current_hp; set
        { 
            if (value < 0) value = 0; if (value > max_hp) value = max_hp; 
            current_hp = value;
        }
    }

    public C_Enum.CharacterType type = C_Enum.CharacterType.Hero;

    public int team = 0;

    public M_Character()
    {

    }

    public M_Character(M_Character nhanVat)
    {
        this.id_nv = nhanVat.id_nv;
        this.idx = nhanVat.idx;
        this.id_cfg = nhanVat.id_cfg;
        this.name = nhanVat.name;
        this.element = nhanVat.element;
        this.star = nhanVat.star;
        this.def = nhanVat.def;
        this.hp = nhanVat.hp;
        this.atk = nhanVat.atk;
        this.crit = nhanVat.crit;
        this.dodge = nhanVat.dodge;
        this.team = nhanVat.team;

        this.current_hp = this.max_hp = this.hp;
        this.current_ep = 0;
        this.max_ep = 100;
    }

    public M_Character(int id_nv, string id_cfg, int id_tk, int lv, int idx)
    {
        this.id_nv = id_nv;
        this.id_cfg = id_cfg;
        this.id_tk = id_tk;
        this.lv = lv;
        this.idx = idx;
    }

    public M_Character(ISFSObject obj, C_Enum.ReadType type)
    {
        if (obj == null) return;

        switch (type)
        {
            case C_Enum.ReadType.SERVER:
                this.id_nv = obj.GetInt("id_nv");
                this.id_cfg = obj.GetUtfString("id_cfg");
                this.id_tk = obj.GetInt("id_tk");
                this.lv = obj.GetInt("lv");
                this.idx = obj.GetInt("idx");
                break;
            case C_Enum.ReadType.CONFIG:
                this.id_cfg = obj.GetText("id");
                this.name = obj.GetText("name");
                this.element = obj.GetText("element");
                this.star = obj.GetInt("star");
                this.def = obj.GetInt("def");
                this.hp = obj.GetInt("hp");
                this.atk = obj.GetInt("atk");
                this.crit = (float)obj.GetDouble("crit");
                this.dodge = (float)obj.GetDouble("dodge");
                break;
            default:
                break;
        }
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

    public void UpdateById(string id_cfg = null)
    {
        if (id_cfg == null) id_cfg = this.id_cfg;
        else this.id_cfg = id_cfg;

        M_Character nvInCfg = new M_Character();

        switch (type)
        {
            case C_Enum.CharacterType.Hero:
                nvInCfg = GameManager.instance.herosDic[id_cfg];
                break;
            case C_Enum.CharacterType.Creep:
                nvInCfg = GameManager.instance.creepsDic[id_cfg];
                break;
        }

        this.name = nvInCfg.name;
        this.element = nvInCfg.element;
        this.def = nvInCfg.def;
        this.hp = nvInCfg.hp;
        this.crit = nvInCfg.crit;
        this.dodge = nvInCfg.dodge;
        this.atk = nvInCfg.atk;
    }
}
