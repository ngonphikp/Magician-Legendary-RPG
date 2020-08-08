using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSendUtil 
{
    public static void sendUpLevel(int id_nv)
    {
        Debug.Log("=========================== Up Level");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.UPLEVEL);

        isFSObject.PutInt("id_nv", id_nv);
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

    public static void sendGetInfo(int id)
    {
        Debug.Log("=========================== Get Info: " + id);
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
        Debug.Log("=========================== Selection");
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

    public static void sendArrange(List<M_Character> nhanVats)
    {
        Debug.Log("=========================== Arrange");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.ARRANGE);

        ISFSArray nvObjs = new SFSArray();
        for (int i = 0; i < nhanVats.Count; i++)
        {
            nvObjs.AddSFSObject(nhanVats[i].parse());
        }

        isFSObject.PutSFSArray("nhanvats", nvObjs);
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

    public static void sendTavern(C_Enum.CardType type)
    {
        Debug.Log("=========================== Tavern");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.TAVERN);

        isFSObject.PutInt("type_tavern", (int)type);
        isFSObject.PutInt("id", GameManager.instance.taikhoan.id);
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
