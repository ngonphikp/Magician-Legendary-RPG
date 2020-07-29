using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Enum
{
    public enum MoneyType
    {
        
    }

    public enum ReadType
    {
        SERVER,
        CONFIG,
    }

    public enum CharacterType
    {
        Hero = 0,
        Creep = 1
    }

    public enum CardType
    {
        CT = 0, // Star 1, 2
        DT = 1, // Star 3, 4
        TT = 2, // Star 5, 6
    }
}
