﻿using Sfs2X.Entities.Data;
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

    public M_Milestone()
    {

    }

    public M_Milestone(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt("id_ml");
        this.star = obj.GetInt("star");
    }

    public M_Milestone(int id, int star)
    {
        this.id = id;
        this.star = star;
    }
}
