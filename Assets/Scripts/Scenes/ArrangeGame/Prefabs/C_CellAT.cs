﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CellAT : MonoBehaviour
{
    public C_DD content = null;

    public void set(M_NhanVat nhanVat, Canvas canvas = null)
    {
        content.Init(nhanVat, canvas);        
    }
}
