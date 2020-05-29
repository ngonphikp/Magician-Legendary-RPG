using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class M_Prop
{
    public System.Object par0d0;
    public List<string> par0d1 = new List<string>();
    public bool par0d2;
    public List<string> par0d3 = new List<string>();

    public bool apply;
    public string id_effect;
    public string rs_effect;

    public int par7d0;

    public int par8d0;

    public M_Prop()
    {

    }

    public M_Prop(string actor, int id, ISFSArray sfsProps)
    {
        switch (id)
        {
            case (int)C_Enum.idAction.SKILLING:
                // Lấy character trong dict
                //C_Enum.CharacterType type = FightingHandle.characterDict[actor].type;
                //if (FightingHandle.characterDict[actor].type == C_Enum.CharacterType.Sage || FightingHandle.characterDict[actor].type == C_Enum.CharacterType.Cel)
                    this.par0d0 = sfsProps.GetUtfString(0);
                //else
                    this.par0d0 = sfsProps.GetInt(0) + 2;

                this.par0d2 = sfsProps.GetBool(2);
                if (!this.par0d2)
                {
                    ISFSArray arrStr1 = sfsProps.GetSFSArray(1);
                    this.par0d1 = new List<string>();
                    for (int i = 0; i < arrStr1.Size(); i++)
                    {
                        this.par0d1.Add(arrStr1.GetUtfString(i));
                    }
                    //ISFSArray arrStr2 = sfsProps.GetSFSArray(2);
                    //this.par0d3 = new List<string>();
                    //for (int i = 0; i < arrStr2.Size(); i++)
                    //{
                    //    this.par0d3.Add(arrStr2.GetUtfString(i));
                    //}
                }
                break;
            case (int)C_Enum.idAction.BEATEN:

                break;
            case (int)C_Enum.idAction.APPLY_EFFECT:
                this.apply = true;                
                this.id_effect = sfsProps.GetText(0);
                this.rs_effect = "SE014_CRIT";
                break;
            case (int)C_Enum.idAction.REMOVED_EFFECT:
                this.apply = false;
                this.id_effect = sfsProps.GetText(0);
                this.rs_effect = "SE014_CRIT";
                break;
            case (int)C_Enum.idAction.DODGE:

                break;
            case (int)C_Enum.idAction.DIE:

                break;
            case (int)C_Enum.idAction.HEALTH_CHANGE:
                this.par7d0 = sfsProps.GetInt(0);
                break;
            case (int)C_Enum.idAction.ENERGY_CHANGE:
                this.par8d0 = sfsProps.GetInt(0);
                break;
            default:
                Debug.Log("=====================================================>Prop default");
                break;
        }
    }
}
