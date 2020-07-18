using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSendUtil 
{
    public static void GetInfo(int id)
    {
        Debug.Log("----------------------->GetInfo: " + id);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.GETINFO);
        isFSObject.PutInt("id", id);

        var packet = new ExtensionRequest(ModuleConfig.USER, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void sendSelection(string tennhanvat, string id_cfg)
    {
        Debug.Log("----------------------->Selection");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.SELECTION);
        isFSObject.PutInt("id", GameManager.instance.taikhoan.id);
        isFSObject.PutUtfString("name", tennhanvat);
        isFSObject.PutUtfString("id_cfg", id_cfg);
        var packet = new ExtensionRequest(ModuleConfig.USER, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void sendArrange(List<M_Hero> heros)
    {
        Debug.Log("----------------------->Arrange");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.ARRANGE);

        ISFSArray heroObjs = new SFSArray();
        for (int i = 0; i < heros.Count; i++)
        {
            heroObjs.AddSFSObject(heros[i].parse());
        }

        isFSObject.PutSFSArray("nhanvats", heroObjs);
        var packet = new ExtensionRequest(ModuleConfig.USER, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }
}
