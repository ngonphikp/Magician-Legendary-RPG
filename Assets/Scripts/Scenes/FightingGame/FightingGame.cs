﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FightingGame : MonoBehaviour
{
    public static FightingGame instance = null;

    [SerializeField]
    private Transform[] posTeamL = null;
    [SerializeField]
    private Transform[] posTeamR = null;

    // Data
    [SerializeField]
    private List<M_Character> dataTeamL = new List<M_Character>();
    [SerializeField]
    private List<M_Character> dataTeamR = new List<M_Character>();

    private bool isEndGame = false;

    [SerializeField]
    private Text txtTime = null;

    private List<C_Character> lstObj = new List<C_Character>();
    public Dictionary<int, C_Character> Objs = new Dictionary<int, C_Character>();

    public List<int> idTargets = new List<int>();
    public List<C_Character> targets = new List<C_Character>();
    public int Beaten = 0;

    private List<M_Action> actions = new List<M_Action>();

    public float myTimeScale = 1.0f;
    public bool isScaleTime = false;
    
    // Check anim1 tất cả
    public bool Turn { get => turn; }
    private bool turn = true;
    private int check = 0;
    private bool getAction = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ScaleTime()
    {
        isScaleTime = !isScaleTime;
        myTimeScale = (isScaleTime) ? 2.0f : 1.0f;
    }

    [Obsolete]
    private void Start()
    {
        LoadData();
        Scenario();
        Combat();
    }

    // Load Data
    private void LoadData()
    {
        Debug.Log("LoadData");
        dataTeamL.Clear();
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                M_Character nhanVat = new M_Character(GameManager.instance.nhanVats[i]);
                nhanVat.team = 0;
                nhanVat.id_nv = i;
                dataTeamL.Add(nhanVat);
            }
        }
        dataTeamR.Clear();
        for (int i = 0; i < GameManager.instance.milestones[GameManager.instance.idxMilestone].listCreep.Count; i++)
        {
            M_Character nhanVat = new M_Character(GameManager.instance.milestones[GameManager.instance.idxMilestone].listCreep[i]);
            nhanVat.team = 1;
            nhanVat.id_nv = i + dataTeamL.Count;
            dataTeamR.Add(nhanVat);
        }

        Init();
    }

    private void Init()
    {
        InitTeam(dataTeamL, posTeamL);
        InitTeam(dataTeamR, posTeamR);
    }

    private void InitTeam(List<M_Character> datas, Transform[] poses)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject nvAs = QuickFunction.getAssetPref("Prefabs/Character/" + datas[i].id_cfg);

            if (nvAs == null)
            {
                switch (datas[i].type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Character/T1004");
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Character/M1000");
                        break;
                    default:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Character/T1004");
                        break;
                }
            }

            if (nvAs != null)
            {
                GameObject obj = Instantiate(nvAs, poses[datas[i].idx]);
                C_Character c_obj = obj.GetComponent<C_Character>();
                c_obj.Set(new M_Character(datas[i]));
                c_obj.isCombat = true;
                c_obj.nhanvat.isDie = false;

                Objs.Add(datas[i].id_nv, c_obj);
                lstObj.Add(c_obj);
            }
        }
    }

    // Scenario

    [Obsolete]
    private void Scenario()
    {
        Debug.Log("Scenario");

        while (true)
        {
            if (isEndGame) break;

            Attack(dataTeamL, dataTeamR);

            Attack(dataTeamR, dataTeamL);
        }        
    }

    [Obsolete]
    private void Attack(List<M_Character> TeamAttack, List<M_Character> TeamAttacked)
    {
        for (int i = 0; i < TeamAttack.Count; i++)
        {
            M_Character actor = TeamAttack[i];

            if (actor.isDie) continue;

            int idSkill = (actor.Current_ep >= actor.max_ep) ? 5 : 3;

            int find = 1;

            List<int> idxs = FindTargetNotDie(TeamAttacked);
            if(idxs.Count == 0)
            {
                isEndGame = true;
                break;
            }
            else if (idxs.Count > find)
            {                
                ShuffleArray(ref idxs);                
            }

            for (int j = 0; j < find && j < idxs.Count; j++)
            {
                List<M_Character> targets = new List<M_Character>();

                targets.Add(TeamAttacked[idxs[j]]);

                actions.Add(Action(actor, idSkill, targets));
            }
        }
    }

    [Obsolete]
    private void ShuffleArray(ref List<int> idxs)
    {
        for(int i = 0; i < idxs.Count - 1; i++)
        {
            int random = UnityEngine.Random.RandomRange(i, idxs.Count);
            Swap(ref idxs, i, random);
        }
    }

    private void Swap(ref List<int> idxs, int a, int b)
    {
        int temp = idxs[a];
        idxs[a] = idxs[b];
        idxs[b] = temp;
    }

    private List<int> FindTargetNotDie(List<M_Character> TeamAttacked)
    {
        List<int> rs = new List<int>();
        for (int i = 0; i < TeamAttacked.Count; i++)
        {
            if (!TeamAttacked[i].isDie) rs.Add(i);
        }
        return rs;
    }

    private M_Action Action(M_Character actor, int idSkill, List<M_Character> targets)
    {
        M_Action action = new M_Action();

        action.idActor = actor.id_nv;
        action.type = C_Enum.ActionType.SKILL;
        action.prop = new M_Prop();
        action.prop.idSkill = idSkill;

        action.prop.idTargets.Clear();
        for (int i = 0; i < targets.Count; i++)
        {
            action.prop.idTargets.Add(targets[i].id_nv);
        }

        action.actions = new List<M_Action>();

        // Nếu dùng ulti
        if (idSkill == 5)
        {
            // Actor tăng -100 ep
            {
                M_Action actionC = new M_Action();
                actionC.idActor = actor.id_nv;
                actionC.type = C_Enum.ActionType.CHANGE_EP;

                actionC.prop = new M_Prop();
                actionC.prop.epChange = -100;

                actor.Current_ep += -100;
                action.actions.Add(actionC);
            }
        }
        else
        {
            // Actor tăng 20 ep
            {
                M_Action actionC = new M_Action();
                actionC.idActor = actor.id_nv;
                actionC.type = C_Enum.ActionType.CHANGE_EP;

                actionC.prop = new M_Prop();
                actionC.prop.epChange = 20;

                actor.Current_ep += 20;
                action.actions.Add(actionC);
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            // Tính dame trước
            int dame = ((actor.atk * 2 - targets[i].def) > 0) ? (actor.atk * 2 - targets[i].def) : 1;

            // Nếu dame chết
            if (dame >= targets[i].Current_hp)
            {
                // target chết
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id_nv;
                    actionC.type = C_Enum.ActionType.DIE;

                    targets[i].isDie = true;
                    action.actions.Add(actionC);
                }
            }
            else
            {
                // target trúng đòn
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id_nv;
                    actionC.type = C_Enum.ActionType.BEATEN;

                    action.actions.Add(actionC);
                }

                // target tăng 10 ep        
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id_nv;
                    actionC.type = C_Enum.ActionType.CHANGE_EP;

                    actionC.prop = new M_Prop();
                    actionC.prop.epChange = 10;

                    targets[i].Current_ep += 10;
                    action.actions.Add(actionC);
                }                
            }

            // target giảm x hp: CT dame = ((atk - def) > 0) ? (atk - def) : 1
            {
                M_Action actionC = new M_Action();
                actionC.idActor = targets[i].id_nv;
                actionC.type = C_Enum.ActionType.CHANGE_HP;

                actionC.prop = new M_Prop();

                actionC.prop.hpChange = -dame;

                targets[i].Current_hp += -dame;
                action.actions.Add(actionC);
            }
        }

        C_Util.GetDumpObject(action);
        return action;
    }

    // Combat

    private async void Combat()
    {
        int t = 3;
        txtTime.gameObject.SetActive(true);
        while (true)
        {
            if (t <= 0) break;
            Debug.Log("Đếm ngược: " + t);
            txtTime.text = t + " ";
            await Task.Delay(TimeSpan.FromSeconds(1));                        
            t -= 1;            
        }
        txtTime.gameObject.SetActive(false);
       
        Debug.Log("Combat");

        while (true)
        {
            if(actions.Count == 0)
            {
                txtTime.text = "End Game";
                txtTime.gameObject.SetActive(true);
                break;
            }

            if(!getAction && Beaten == 0)
            {
                CheckTurn();

                if (turn)
                {
                    M_Action action = actions[0];
                    getAction = true;

                    await Play(action);

                    actions.RemoveAt(0);
                    getAction = false;
                }                
            }

            await Task.Delay(TimeSpan.FromSeconds(0.1));
        }
    }

    private void CheckTurn()
    {
        for (int i = 0; i < lstObj.Count; i++)
        {
            if (lstObj[i].nhanvat.isDie || !lstObj[i].gameObject.activeSelf || lstObj[i].isAnim1()) check++;
            else break;
        }

        turn = (check >= lstObj.Count);
        check = 0;
    }

    private async Task Play(M_Action action)
    {
        int idActor = action.idActor;
        C_Character actor = Objs[idActor];

        switch (action.type)
        {
            case C_Enum.ActionType.SKILL:
                // Debug.Log("=========================== SKILLING: " + idActor);
                SKILLING(actor, action);
                break;
            case C_Enum.ActionType.CHANGE_HP:
                Debug.LogWarning("=========================== CHANGE_HP: " + idActor);
                actor.PushChangeHp(action.prop);
                actor.ChangeHp();
                break;
            case C_Enum.ActionType.CHANGE_EP:
                Debug.LogWarning("=========================== CHANGE_EP: " + idActor);
                actor.PushChangeEp(action.prop);
                actor.ChangeEp();
                break;
            case C_Enum.ActionType.DIE:
                Debug.LogWarning("=========================== DIE: " + idActor);
                actor.nhanvat.isDie = true;

                while (true)
                {
                    if (actor.isAnim1()) break;
                    Debug.Log("=========================== Loop " + actor.nhanvat.id_nv + " Anim1");
                    await Task.Delay(TimeSpan.FromSeconds(0.01));
                }
                actor.Play(6);
                break;
        }

        await Task.Delay(TimeSpan.FromSeconds(0.01));

        if (action.actions.Count > 0)
        {
            Debug.LogWarning("=========================== Chưa diễn hết");
            action.actions.ForEach(x => C_Util.GetDumpObject(x));
        }
    }

    private async void SKILLING(C_Character actor, M_Action action)
    {       
        this.idTargets.Clear();
        this.targets.Clear();

        this.idTargets = action.prop.idTargets;
        this.Beaten = idTargets.Count;

        // Tìm Action của target
        FindActionOfTaget(action.actions);

        // Tìm Action của actor
        FindActionOfActor(actor, action.actions);

        // Diễn Skill khi có target
        if (this.targets.Count > 0)
        {
            while (true)
            {
                if (actor.isAnim1()) break;
                Debug.Log("=========================== Loop " + actor.nhanvat.id_nv + " Anim1");
                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
            actor.Play(action.prop.idSkill);
        }
        actor.ChangeEp();
        actor.ChangeHp();

        if (actor.nhanvat.isDie)
        {
            while (true)
            {
                if (actor.isAnim1()) break;
                Debug.Log("=========================== Loop " + actor.nhanvat.id_nv + " Anim1");
                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }
            actor.Play(6);
        }
    }

    private void FindActionOfTaget(List<M_Action> actionCs)
    {
        for (int i = 0; i < actionCs.Count; i++)
        {
            M_Action actionC = actionCs[i];
            int idTargetC = actionC.idActor;

            if (this.idTargets.Contains(idTargetC))
            {
                C_Character target = Objs[idTargetC];

                switch (actionC.type)
                {
                    case C_Enum.ActionType.BEATEN:
                        target.isHit = true;
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_HP:
                        target.PushChangeHp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_EP:
                        target.PushChangeEp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.DIE:
                        target.nhanvat.isDie = true;
                        actionCs.RemoveAt(i--);
                        break;
                }

                // Nếu tồn tại trong mảng targets
                if (idxTarget(idTargetC) != -1)
                {
                    this.targets[idxTarget(idTargetC)] = target;
                }
                else
                {
                    this.targets.Add(target);
                }
            }            
        }
    }

    private int idxTarget(int id_target)
    {
        for (int i = 0; i < this.targets.Count; i++)
        {
            if (this.targets[i].nhanvat.id_nv == id_target) return i;
        }
        return -1;
    }

    private void FindActionOfActor(C_Character actor, List<M_Action> actionCs)
    {
        for (int i = 0; i < actionCs.Count; i++)
        {
            M_Action actionC = actionCs[i];
            int idActorC = actionC.idActor;

            if (actor.nhanvat.id_nv == idActorC)
            {
                switch (actionC.type)
                {
                    case C_Enum.ActionType.CHANGE_HP:
                        actor.PushChangeHp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_EP:
                        actor.PushChangeEp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.DIE:
                        actor.nhanvat.isDie = true;
                        actionCs.RemoveAt(i--);
                        break;
                }
            }
        }
    }    

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 40;
        myStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 100, 20), "Get Action: " + getAction, myStyle);
        GUI.Label(new Rect(20, 70, 100, 20), "Turn: " + turn, myStyle);
        GUI.Label(new Rect(20, 120, 100, 20), "Beaten: " + Beaten, myStyle);
    }
}
