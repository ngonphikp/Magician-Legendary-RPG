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
    public string actor_id = "";

    public string id = "";
    public string name = "";
    public string element = "";
    public int level = 1;        
    public int power;

    public int idx = 0;

    public int current_ep = 0, max_ep = 1;
    public int current_hp = 0, max_hp = 1;

    public void setCharacter(M_Character character)
    {
        this.id = character.id;
        this.level = character.level;

        this.current_ep = character.current_ep;
        this.max_ep = character.max_ep;
        this.current_hp = character.current_hp;
        this.max_hp = character.max_hp;
        this.actor_id = character.actor_id;
    }

    public M_Character()
    {

    }
}
