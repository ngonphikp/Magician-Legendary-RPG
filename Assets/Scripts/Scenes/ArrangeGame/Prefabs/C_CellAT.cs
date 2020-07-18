using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CellAT : MonoBehaviour
{
    public C_DD content = null;

    public void setHero(M_Hero hero, Canvas canvas = null)
    {
        content.Init(hero, canvas);        
    }
}
