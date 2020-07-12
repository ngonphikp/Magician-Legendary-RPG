using Sfs2X.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRoom 
{
    public static void OnRoomJoin(BaseEvent evt)
    {
        Debug.Log("Vào room !");
    }
    public static void OnRoomJoinError(BaseEvent evt)
    {
        Debug.LogWarning("Join Room không thành công!");
    }
    public static void OnUserExitRoom(BaseEvent evt)
    {
        Debug.Log("OnUserExitRoom");
    }
}
