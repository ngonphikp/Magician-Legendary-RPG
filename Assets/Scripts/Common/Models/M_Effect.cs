using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Effect
{
    public string id;
    public C_Enum.EffectCategory effectCategory;
    public string name;

    public M_Effect()
    {

    }

    public M_Effect(string id, C_Enum.EffectCategory effectCategory, string name)
    {
        this.id = id;
        this.effectCategory = effectCategory;
        this.name = name;
    }
}
