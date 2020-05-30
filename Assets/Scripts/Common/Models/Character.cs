using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public long idUser { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public int clan { get; set; } = 1;
    public int level { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public int hp { get; set; }
    public float critRate { get; set; }
    public float dodge { get; set; }
    public int position { get; set; }
    public int idData { get; set; }

    public Character()
    {

    }

    public Character(string id, int level, int pos)
    {
        this.id = id;
        this.level = level;
        this.position = pos;
    }

    public Character(long idU, string id, int lv, int atk, int def, int hp, float crit, float dodge, int pos)
    {
        this.idUser = idU;
        this.id = id;
        this.level = lv;
        this.atk = atk;
        this.def = def;
        this.hp = hp;
        this.critRate = crit;
        this.dodge = dodge;
        this.position = pos;
    }

    public void SetLevel(int level)
    {
        if (level <= 1)
            return;
        float hs = 1.2f;
        this.level = level;
        this.atk = this.atk * (int)Mathf.Pow(hs, (level - 1));
        this.def = this.def * (int)Mathf.Pow(hs, (level - 1));
        this.hp = this.hp * (int)Mathf.Pow(hs, (level - 1));
        this.critRate = this.critRate * (int)Mathf.Pow(hs, (level - 1));
        this.dodge = this.dodge * (int)Mathf.Pow(hs, (level - 1));
    }

    public void SetData(Character data)
    {
        this.idUser = data.idUser;
        this.id = data.id;
        this.name = data.name;
        this.clan = data.clan;
        this.level = data.level;
        this.atk = data.atk;
        this.def = data.def;
        this.hp = data.hp;
        this.critRate = data.critRate;
        this.position = data.position;
        this.dodge = data.dodge;
        this.idData = data.idData;
    }

    public void SetData2(Character data)
    {
        this.name = data.name;
        this.clan = data.clan;
        this.idData = data.idData;
    }
}
