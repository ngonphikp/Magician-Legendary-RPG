using Sfs2X.Core;
using Sfs2X.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleConnect
{
    public static bool isConnected = false;
    public static void OnConnection(BaseEvent evt)
    {
        // Nếu kết nối server thành công
        if ((bool)evt.Params["success"])
        {
            Debug.Log("Kết nối server thành công!");
            isConnected = true;
        }
        // Nếu kết nối server thất bại
        else
        {
            Debug.LogWarning("Kết nối server thất bại!");

            // Kết nối lại
            SmartFoxConnection.Connect(); 
            isConnected = false;
        }
    }
    public static void OnConnectionLost(BaseEvent evt)
    {
        isConnected = false;
        Debug.LogWarning("Mất kết nối server!");

        // Các lý do mất kết nối duwjzah trên reason
        string reason = (string)evt.Params["reason"];
        if (reason != ClientDisconnectionReason.MANUAL)
        {
            if (reason == ClientDisconnectionReason.IDLE)
            {
                Debug.LogWarning("Mất kết nối do INACTIVITY!");
            }
            else if (reason == ClientDisconnectionReason.KICK)
            {
                Debug.LogWarning("Mất kết nối do server KICK!");
            }
            else if (reason == ClientDisconnectionReason.BAN)
            {
                Debug.LogWarning("Mất kết nối do server BAN");
            }
            else if (reason == ClientDisconnectionReason.UNKNOWN)
            {
                Debug.LogWarning("Mất kết nối do server UNKNOWN");
            }
            else
            {
                Debug.LogWarning("Mất kết nối do: " + reason);
            }
        }
        else
        {
            Debug.LogWarning("Mất kết nối do client tự ngắt!");
        }

        // Chuyển về màn Login nếu mất kết nối
        SceneManager.LoadScene("LoginGame"); 
    }
}
