using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class M_Action
{
    public string actor;
    public int id;
    public List<M_Action> actions = new List<M_Action>();
    public M_Prop prop = new M_Prop();

    public void ReadActionObj(SFSObject objAction)
    {
        PlayGame.instance.sumPlay++;
        this.actor = objAction.GetUtfString("actor");
        this.id = objAction.GetInt("id");

        ISFSArray sfsProps = objAction.GetSFSArray("props");
        this.prop = new M_Prop(actor, this.id, sfsProps);

        //this.actions = new List<M_Action>();
        SFSArray sfsActions = (SFSArray)objAction.GetSFSArray("actions");
        if (sfsActions.Size() > 0)
        {
            for (int i = 0; i < sfsActions.Size(); i++)
            {
                SFSObject obj = (SFSObject)sfsActions.GetSFSObject(i);
                M_Action action = new M_Action();
                action.ReadActionObj(obj);
                this.actions.Add(action);
            }
        }
    }
}
