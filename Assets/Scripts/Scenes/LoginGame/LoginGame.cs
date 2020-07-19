﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame : MonoBehaviour
{    
    public static LoginGame instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RecLogin(M_TaiKhoan tk)
    {
        Debug.Log("====================RecLogin: " + tk.id);

        GameManager.instance.taikhoan = tk;

        UserSendUtil.GetInfo(tk.id);
    }

    public void RecRegister(M_TaiKhoan tk)
    {
        Debug.Log("====================RecRegister: " + tk.id);

        GameManager.instance.taikhoan = tk;

        UserSendUtil.GetInfo(tk.id);
    }

    public void RecInfo(List<M_NhanVat> lstNhanVat)
    {
        Debug.Log("====================RecInfo");

        //lstNhanVat.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));

        // Nếu đã Selection
        if(lstNhanVat.Count > 0)
        {
            GameManager.instance.nhanVats = lstNhanVat;
            ScenesManager.instance.ChangeScene("HomeGame");
        }
        // Nếu chưa Selection
        else
        {
            ScenesManager.instance.ChangeScene("SelectionGame");
        }
    }
}
