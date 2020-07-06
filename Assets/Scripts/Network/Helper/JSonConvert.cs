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
    public List<M_Hero> GetListHero()
    {
        List<M_Hero> list = new List<M_Hero>();
        try
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
                hero.id = obj.GetText("id");
                hero.name = obj.GetText("name");
                hero.element = obj.GetText("element");
                hero.def = obj.GetInt("def");
                hero.hp = obj.GetInt("hp");
                hero.atk = obj.GetInt("atk");
                hero.crit = (float)obj.GetDouble("crit");
                hero.dodge = (float)obj.GetDouble("dodge");
                list.Add(hero);
            }
        }        
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
        return list;
    }
}
