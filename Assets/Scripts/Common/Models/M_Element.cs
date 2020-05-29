using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Element
{
    public string id;
    public string name;
    public Dictionary<string,int> costChange = new Dictionary<string, int>();

    public M_Element()
    {

    }

    public M_Element(string id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
