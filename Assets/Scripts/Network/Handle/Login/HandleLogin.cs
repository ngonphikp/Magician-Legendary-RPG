using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLogin 
{
    public static string notificationError;

    public static void OnLoginSuccess(BaseEvent evt)
    {
        Debug.Log("Login server thành công!");
        try
        {
            SFSObject packet = (SFSObject)evt.Params["data"];

            Debug.Log(packet.GetDump());
            
            ISFSObject data = packet.GetSFSObject("loginOutData");

            short cmdid = (short)data.GetInt(CmdDefine.CMDID);

            switch (cmdid)
            {
                case CmdDefine.LOGIN:
                    handleLogin(data);
                    break;
                case CmdDefine.REGISTER:
                    handleRegister(data);
                    break;
                default:

                    break;
            }            
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void handleLogin(ISFSObject data)
    {
        Debug.Log("______________HANDLE LOGIN_____________\n" + data.GetDump());
        short ec = data.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {           
            LoginGame.instance.RecLogin(new M_TaiKhoan(data.GetSFSObject("taikhoan")));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }        
    }

    public static void handleRegister(ISFSObject data)
    {
        Debug.Log("______________HANDLE REGISTER_____________\n" + data.GetDump());
        short ec = data.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            LoginGame.instance.RecRegister(new M_TaiKhoan(data.GetSFSObject("taikhoan")));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void OnLoginError(BaseEvent evt)
    {
        short ec = (short)evt.Params["errorCode"];
        var message = evt.Params["errorMessage"];
        try
        {
            Debug.Log("ErrorCode: " + ec);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public static void OnLogOut(BaseEvent evt)
    {
        SceneManager.LoadScene("LoginGame");
    }
}
