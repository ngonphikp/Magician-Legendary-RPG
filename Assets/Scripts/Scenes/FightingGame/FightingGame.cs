using System;
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

    [SerializeField]
    private Text txtTime = null;

    List<C_Character> lstObj = new List<C_Character>();
    public Dictionary<int, C_Character> Objs = new Dictionary<int, C_Character>();

    public List<int> idTargets = new List<int>();
    public List<C_Character> targets = new List<C_Character>();
    public int Beaten = 0;

    private List<M_Action> actions = new List<M_Action>();

    public float myTimeScale = 1.0f;
    public bool isScaleTime = false;

    private int check = 0;
    // Check anim1 tất cả
    public bool Turn { get => turn; }
    private bool turn = true;
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

    private void Start()
    {
        LoadData();
        Scenario();
        ChangeTurn();
        Combat();
    }

    private async void ChangeTurn()
    {
        while (true)
        {
            // Đếm số Hero ở trạng thái Anim1
            for (int i = 0; i < lstObj.Count; i++)
                if (lstObj[i] != null) if (lstObj[i].isAnim1() || lstObj[i].isDie || !lstObj[i].gameObject.activeSelf) check++; else break;

            turn = (check >= lstObj.Count);
            check = 0;

            await Task.Yield();
        }
    }

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
            if(actions.Count > 0 && turn && !getAction && Beaten == 0)
            {
                M_Action action = actions[0];
                getAction = true;

                await Play(action);
                
                actions.RemoveAt(0);
                getAction = false;
            }
            await Task.Yield();
        }
    }

    private async Task Play(M_Action action)
    {
        int idActor = action.idActor;
        C_Character actor = Objs[idActor];

        switch (action.type)
        {
            case C_Enum.ActionType.SKILL:
                SKILLING(actor, action);
                break;
            case C_Enum.ActionType.CHANGE_HP:
                actor.PushChangeHp(action.prop);
                actor.ChangeHp();
                break;
            case C_Enum.ActionType.CHANGE_EP:
                actor.PushChangeEp(action.prop);
                actor.ChangeEp();
                break;
        }

        await Task.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime / 10));

        if (action.actions.Count > 0)
        {
            Debug.LogWarning("===========================Chưa diễn hết");
            action.actions.ForEach(x => C_Util.getDumpObject(x));
        }
    }

    private void SKILLING(C_Character actor, M_Action action)
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
            actor.Play(action.prop.idSkill);
        }
        actor.ChangeEp();
        actor.ChangeHp();
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
                }
            }
        }
    }

    private void Scenario()
    {
        Debug.Log("Scenario");

        {
            M_Character actor = dataTeamL[0];
            M_Character target = dataTeamR[0];
            actions.Add(Action11(actor, 3, target));
        }
        {
            M_Character actor = dataTeamL[1];
            M_Character target = dataTeamR[1];
            actions.Add(Action11(actor, 3, target));
        }

        {
            M_Character actor = dataTeamL[0];
            M_Character target = dataTeamR[0];
            actions.Add(Action11(target, 3, actor));
        }
        {
            M_Character actor = dataTeamL[1];
            M_Character target = dataTeamR[1];
            actions.Add(Action11(target, 3, actor));
        }
    }

    private M_Action Action11(M_Character actor, int idSkill, M_Character target)
    {
        M_Action action = new M_Action();

        action.idActor = actor.id_nv;
        action.type = C_Enum.ActionType.SKILL;
        action.prop = new M_Prop();
        action.prop.idSkill = idSkill;

        action.prop.idTargets.Clear();
        action.prop.idTargets.Add(target.id_nv);

        action.actions = new List<M_Action>();

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

        // target trúng đòn
        {
            M_Action actionC = new M_Action();
            actionC.idActor = target.id_nv;
            actionC.type = C_Enum.ActionType.BEATEN;

            action.actions.Add(actionC);
        }

        // target tăng 10 ep        
        {
            M_Action actionC = new M_Action();
            actionC.idActor = target.id_nv;
            actionC.type = C_Enum.ActionType.CHANGE_EP;

            actionC.prop = new M_Prop();
            actionC.prop.epChange = 10;

            target.Current_ep += 10;
            action.actions.Add(actionC);
        }

        // target giảm x hp: CT dame = ((atk - def) > 0) ? (atk - def) : 1
        {
            M_Action actionC = new M_Action();
            actionC.idActor = target.id_nv;
            actionC.type = C_Enum.ActionType.CHANGE_HP;

            actionC.prop = new M_Prop();

            int dame = ((actor.atk * 2 - target.def) > 0) ? (actor.atk * 2 - target.def) : 1;

            actionC.prop.hpChange = -dame;

            actor.Current_hp += -dame;
            action.actions.Add(actionC);
        }

        C_Util.getDumpObject(action);
        return action;
    }

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
                c_obj.isDie = false;

                Objs.Add(datas[i].id_nv, c_obj);
                lstObj.Add(c_obj);
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
