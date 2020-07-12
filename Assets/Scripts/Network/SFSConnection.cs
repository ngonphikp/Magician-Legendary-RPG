using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFoxConnection : MonoBehaviour
{

    private static SmartFox sfs;

    public static SmartFox Sfs { get => sfs; }

    #region Kiểm tra kết nối và khởi tạo một object trong Unity nếu rỗng
    private static SmartFoxConnection mInstance;
    public static SmartFox Connection
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("SmartFoxConnection").AddComponent(typeof(SmartFoxConnection)) as SmartFoxConnection;
            }
            return sfs;
        }
        set
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("SmartFoxConnection").AddComponent(typeof(SmartFoxConnection)) as SmartFoxConnection;
            }
            sfs = value;
        }
    }

    public static bool IsInitialized
    {
        get
        {
            return (sfs != null);
        }
    }
    #endregion

    // Handle disconnection automagically
    // ** Important for Windows users - can cause crashes otherwise
    public static void OnApplicationQuit()
    {
        if (sfs.IsConnected)
        {
            sfs.Disconnect();
        }
    }
    public static void ListenerEvent()
    {
        if (isAlready())
        {
            sfs.ProcessEvents();
        }
    }

    public static void setNull()
    {
        sfs = null;
    }

    /// <summary>
    /// Main Constractor
    /// </summary>
    public static void Init()
    {
        if (SmartFoxConnection.IsInitialized)
        {
            sfs = SmartFoxConnection.Connection;

        }
        else
        {
            JSonConvert convert = new JSonConvert();
            convert.GetConfig_ConnectSFS();
            ConfigData cfg = new ConfigData();
            cfg.Host = ConfigConnection.Host;
            cfg.Port = ConfigConnection.TCPPort;
            cfg.Zone = ConfigConnection.Zone;

            // Tạo đối tượng sfs
            sfs = new SmartFox();

            // =================Lắng nghe sự kiện từ sfs================
           
            // Sự kiện kết nối và mất kết nối
            sfs.AddEventListener(SFSEvent.CONNECTION, HandleConnect.OnConnection);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, HandleConnect.OnConnectionLost);

            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, HandleExtension.OnExtensionResponse);

            sfs.AddEventListener(SFSEvent.LOGIN, HandleLogin.OnLoginSuccess);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, HandleLogin.OnLoginError);
            sfs.AddEventListener(SFSEvent.LOGOUT, HandleLogin.OnLogOut);


            sfs.AddEventListener(SFSEvent.ROOM_JOIN, HandleRoom.OnRoomJoin);
            sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, HandleRoom.OnRoomJoinError);
            sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, HandleRoom.OnUserExitRoom);

            // Gán cfg cho sfs
            sfs.Connect(cfg);

            Debug.Log("Khởi tạo kết nối SFS");
        }
    }

    public static void Connect()
    {
        if (!isAlready())
        {
            Init();
        }
        else
        {
            HandleConnect.isConnected = true;
            Debug.Log("Đã kết nối server");
        }
    }
    public static bool isAlready()
    {
        if (sfs == null)
        {
            return false;
        }
        else
        {
            return sfs.IsConnected;
        }
    }

    public static void send(LoginRequest request)
    {
        sfs.Send(request);
    }
    public static void send(ExtensionRequest request)
    {
        sfs.Send(request);
    }
    public static void send(LogoutRequest request)
    {
        sfs.Send(request);
    }
}
