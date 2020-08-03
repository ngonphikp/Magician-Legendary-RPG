using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Action
{
    public int idActor;
    public C_Enum.ActionType type = C_Enum.ActionType.SKILL;
    public M_Prop prop = new M_Prop();

    public List<M_Action> actions = new List<M_Action>();
}
