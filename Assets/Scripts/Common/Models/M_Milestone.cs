using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Milestone
{
    public int id;
    public string name;
    public int star = 0;
    public List<M_Creep> listCreep = new List<M_Creep>();
}
