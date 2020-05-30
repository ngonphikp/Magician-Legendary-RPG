using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Turn
{    
    public bool isTeam = true;
    public int hit = -1;
    public int hitted = -1;
    public bool isDodge = false;
    public bool isSkill = false;
    public bool isCrit = false;
    public int hpLost = 0;
    public short manaH = 0;
    public short manaHt = 0;

    public M_Turn()
    {

    }

    public M_Turn(bool isTeam, int hit, int hitted, bool isDodge, bool isSkill, bool isCrit, int hpLost, short manaH, short manaHt)
    {
        this.isTeam = isTeam;
        this.hit = hit;
        this.hitted = hitted;
        this.isDodge = isDodge;
        this.isSkill = isSkill;
        this.isCrit = isCrit;
        this.hpLost = hpLost;
        this.manaH = manaH;
        this.manaHt = manaHt;
    }
}
