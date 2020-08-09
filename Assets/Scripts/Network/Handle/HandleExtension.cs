using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleExtension 
{
    public static void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        SFSObject dataObject = (SFSObject)evt.Params["params"];

        //Debug.Log(dataObject.GetDump());

        switch (cmd)
        {
            case ModuleConfig.USER:
                HandleUser.OnMessage(dataObject);
                break;
            default:

                break;
        }        
    }
}
