using Sfs2X.Entities.Data;
using SFSLitJson;
using System;
using System.Collections.Generic;
using UnityEngine;
public class JSonConvert
{      
    private string jsonString;
    private JsonData itemData;

    public void GetConfig_ConnectSFS()
    {
        try
        {
            TextAsset file = Resources.Load<TextAsset>("ConfigJSon/config_connect");
            jsonString = file.ToString();
            itemData = JsonMapper.ToObject(jsonString);

            ConfigConnection.Host.Value = GameManager.instance.host;

            ConfigConnection.TCPPort.Value = (int)itemData["TCPPort"];
            ConfigConnection.WsPort.Value = (int)itemData["WsPort"];
            ConfigConnection.Zone.Value = (string)itemData["Zone"];
            //ConfigConnection.Host.Value = (string)itemData["host"];
           
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

    public IEnumerable<M_Skill> GetListSkill()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Skill");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Skill skill = new M_Skill();

            skill.id_cfg = obj.GetUtfString("id");
            skill.name = obj.GetUtfString("name");
            skill.describe = obj.GetUtfString("describe");
            skill.type = obj.GetInt("type");

            yield return skill;
        }
    }

    public IEnumerable<M_Character> GetListHero()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Hero");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
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
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Creep");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
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
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Milestone");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
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
