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

    public IEnumerable<M_Character> GetListHero()
    {
        TextAsset file = QuickFunction.getAssetJsons("ConfigJSon/Hero");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("listHero");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Character hero = new M_Character(obj, C_Enum.ReadType.CONFIG);

            hero.type = C_Enum.CharacterType.Hero;

            yield return hero;
        }
    }

    public IEnumerable<M_Character> GetListCreep()
    {
        TextAsset file = QuickFunction.getAssetJsons("ConfigJSon/Creep");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("listCreep");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Character creep = new M_Character(obj, C_Enum.ReadType.CONFIG);

            creep.type = C_Enum.CharacterType.Creep;

            yield return creep;
        }
    }

    public IEnumerable<M_Milestone> GetListMilestone()
    {
        TextAsset file = QuickFunction.getAssetJsons("ConfigJSon/Milestone");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("listMilestone");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Milestone milestone = new M_Milestone();
            milestone.id = obj.GetInt("id");
            milestone.name = obj.GetText("name");
            milestone.star = obj.GetInt("star");

            milestone.listCreep.Clear();

            ISFSArray cArr = obj.GetSFSArray("listCreep");
            for (int j = 0; j < cArr.Size(); j++)
            {
                ISFSObject cObj = cArr.GetSFSObject(j);
                //Debug.Log(cObj.GetDump());

                M_Character creep = new M_Character();
                creep.id_cfg = cObj.GetText("id");
                creep.lv = cObj.GetInt("lv");
                creep.idx = cObj.GetInt("idx");

                creep.type = C_Enum.CharacterType.Creep;

                creep.UpdateById();

                milestone.listCreep.Add(creep);
            }

            yield return milestone;
        }
    }
}
