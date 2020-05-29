using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Character
{
    public string hash;
    public bool isEx = false;
    public int team = 0;
    public C_Enum.CharacterType type = C_Enum.CharacterType.Hero;
    public int star = 1;
    public int level = 1;
    public string id = "";
    public string actor_id = "";
    public string kingdom = "1";
    public string element = "401";
    public int power;

    public int idx = 0;

    public int current_ep = 0, max_ep = 1;
    public int current_hp = 0, max_hp = 1;

    public void setCharacter(M_Character character)
    {
        this.id = character.id;
        this.level = character.level;
        this.star = character.star;

        this.current_ep = character.current_ep;
        this.max_ep = character.max_ep;
        this.current_hp = character.current_hp;
        this.max_hp = character.max_hp;
        this.actor_id = character.actor_id;
    }

    public M_Character()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="idx">vi tri trong team</param>
    /// <param name="isEx">ton tai trong team</param>
    /// <param name="team">team left = 0; right = 1</param>
    /// <param name="type">loai charactor , hero, creep, boss, Mboss</param>
    /// <param name="star"></param>
    /// <param name="level"></param>
    /// <param name="id"></param>
    /// <param name="current_ep"> nangw luong hien tai</param>
    /// <param name="max_ep">max nang luong</param>
    /// <param name="current_hp">hp hien tai</param>
    /// <param name="max_hp">max hp</param>
    public M_Character(int idx, string actor_id, bool isEx, int team, C_Enum.CharacterType type, int star, int level, string id, int current_ep, int max_ep, int current_hp, int max_hp)
    {
        this.idx = idx;
        this.actor_id = actor_id;
        this.isEx = isEx;
        this.team = team;
        this.type = type;
        this.star = star;
        this.level = level;
        this.id = id;
        this.current_ep = current_ep;
        this.max_ep = max_ep;
        this.current_hp = current_hp;
        this.max_hp = max_hp;
    }

    public M_Character(int idx, bool isEx, int team, C_Enum.CharacterType type, int star, int level, string id, int current_ep, int max_ep, int current_hp, int max_hp)
    {
        this.idx = idx;
        this.isEx = isEx;
        this.team = team;
        this.type = type;
        this.star = star;
        this.level = level;
        this.id = id;
        this.current_ep = current_ep;
        this.max_ep = max_ep;
        this.current_hp = current_hp;
        this.max_hp = max_hp;
    }


}
