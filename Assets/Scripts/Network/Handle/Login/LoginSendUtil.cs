using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSendUtil 
{
   public static void sendLogin(string username, string password)
    {
        Debug.Log("----------------------->Login");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.LOGIN);
        isFSObject.PutUtfString("username", username);
        isFSObject.PutUtfString("password", password);
        var packet = new LoginRequest("", "", ConfigConnection.Zone, isFSObject);
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

    public static void sendRegister(string username, string password)
    {
        Debug.Log("----------------------->Registry");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.REGISTER);
        isFSObject.PutUtfString("username", username);
        isFSObject.PutUtfString("password", password);
        var packet = new LoginRequest("", "", ConfigConnection.Zone, isFSObject);
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

    public static void sendLogout()
    {
        Debug.Log("----------------------->Logout");
        SmartFoxConnection.send(new LogoutRequest());
    }
}
