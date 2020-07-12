using Crossdata;
using Newtonsoft.Json.Linq;
using Sfs2X.Entities.Data;
using SFSLitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class JSonConvert
{      
    private string jsonString;
    private JsonData itemData;

    public void GetConfig_ConnectSFS()
    {
        try
        {
            TextAsset file = QuickFunction.getAssetJsons("ConfigJSon/config_connect");
            jsonString = file.ToString();
            itemData = JsonMapper.ToObject(jsonString);
            ConfigConnection.TCPPort.Value = (int)itemData["TCPPort"];
            ConfigConnection.WsPort.Value = (int)itemData["WsPort"];
            ConfigConnection.Zone.Value = (string)itemData["Zone"];
            ConfigConnection.Host.Value = (string)itemData["host"];

            //Debug.Log(ConfigConnection.TCPPort);
            //Debug.Log(ConfigConnection.WsPort);
            //Debug.Log(ConfigConnection.host);
            //Debug.Log(ConfigConnection.Zone);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public IEnumerable<M_Hero> GetListHero()
    {
        TextAsset file = QuickFunction.getAssetJsons("ConfigJSon/Hero");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("listHero");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Hero hero = new M_Hero();
            hero.id_cfg = obj.GetText("id");
            hero.name = obj.GetText("name");
            hero.element = obj.GetText("element");
            hero.def = obj.GetInt("def");
            hero.hp = obj.GetInt("hp");
            hero.atk = obj.GetInt("atk");
            hero.crit = (float)obj.GetDouble("crit");
            hero.dodge = (float)obj.GetDouble("dodge");

            yield return hero;
        }
    }
}
