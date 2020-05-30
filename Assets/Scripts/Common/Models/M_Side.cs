using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Side
{
    public string idSide;
    public string name = "";
    public int flag = 0;
    public int coin = 0;
    public int soul = 0;
    public int star = 0;
    public int idData = 0;
    public List<Character> listChar = new List<Character>();

    public M_Side()
    {

    }

    public M_Side(string name, int idData)
    {
        this.name = name;
        this.idData = idData;
    }

    public M_Side(string idSide, string name, int flag, int coin, int soul, int star, int idData)
    {
        this.idSide = idSide;
        this.name = name;        
        this.flag = flag;
        this.coin = coin;
        this.soul = soul;
        this.star = star;
        this.idData = idData;
    }
}
