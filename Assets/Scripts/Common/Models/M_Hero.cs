using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Data;

[System.Serializable]
public class M_Hero : M_Character
{
    public string name = "Hero1001";    
    public string Class = "201";
    public string role = "601";
    public string description;
    public int bnStory;
    public Dictionary<string, int> upgrade = new Dictionary<string, int>();
    public int health, strength, intelligence, dexterity, armor, magicResistance, agility, crit, critBonus, armorPenetration, magicPenetration, tenacity, elusivness;
    public bool quickUpgradeAble;
    public int pos = -1;

    //public List<M_Item> equipment = new List<M_Item>();

    //public List<M_Skill> skills = new List<M_Skill>();
    public M_Hero()
    {

    }


    public M_Hero(string id, string element, string kingdom, int level, int star)
    {
        this.id = id;
        this.element = element;
        this.kingdom = kingdom;
        this.level = level;
        this.star = star;
    }

    public M_Hero(string id, int level, int pos)
    {
        this.id = id;

        //UpdateById(id);
        this.element = "40" + Random.Range(1, 8);

        this.level = level;
        this.pos = pos;
    }
    /// <summary>
    /// update lai data hero tranh tinh trang bi tham chieu
    /// </summary>
    /// <param name="id"></param>
    public void UpdateById(string id = null)
    {
        if (id == null) id = this.id;
        else this.id = id;
        M_Hero heroInConfig = GameManager.instance.herosDic[id];
        this.element = heroInConfig.element;
        this.kingdom = heroInConfig.kingdom;
        this.name = heroInConfig.name;
        this.description = heroInConfig.description;
        this.Class = heroInConfig.Class;
        this.role = heroInConfig.role;
    }
}
