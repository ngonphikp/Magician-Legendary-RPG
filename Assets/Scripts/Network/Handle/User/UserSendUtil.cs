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
}
