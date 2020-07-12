using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TaiKhoan
{
    public int id;
    public string usename;
    public string password;

    public M_TaiKhoan()
    {

    }

    public M_TaiKhoan(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt("id");
        this.usename = obj.GetUtfString("username");
        this.password = obj.GetUtfString("password");
    }
}
