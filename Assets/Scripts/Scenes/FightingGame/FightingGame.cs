using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightingGame : MonoBehaviour
{
    public static FightingGame instance = null;

    [Header("Posstion")]
    [SerializeField]
    private Transform[] posTeamL = null;
    [SerializeField]
    private Transform[] posTeamR = null;

    [Header("Data")]
    [SerializeField]
    private List<M_Character> dataTeamL = new List<M_Character>();
    [SerializeField]
    private List<M_Character> dataTeamR = new List<M_Character>();    

    [Header("Turn")]
    [SerializeField]
    private Text txtTime = null;
    [SerializeField]
    private Text txtTurn = null;

    private int currentTurn = 0;

    [Header("Time Scale")]
    [SerializeField]
    private Text txtTimeScale = null;

    public float myTimeScale = 1.0f;
    [SerializeField]
    public float[] arrTimeScale = { 1, 2, 4 };

    [Header("End Game")]
    [SerializeField]
    private GameObject popupEndGame = null;
    [SerializeField]
    private Text txtResult = null;
    [SerializeField]
    private Image[] imgResultStars = new Image[3];
    [SerializeField]
    private Sprite spStar = null;
    [SerializeField]
    private AudioClip acFighting = null;
    [SerializeField]
    private AudioClip acEndGame = null;

    private C_Enum.EndGame isEndGame = C_Enum.EndGame.NOT;
    private int starEndGame = 0;

    private List<C_Character> lstObj = new List<C_Character>();
    public Dictionary<int, C_Character> Objs = new Dictionary<int, C_Character>();

    // Data Combat
    public List<int> idTargets = new List<int>();
    public List<C_Character> targets = new List<C_Character>();
    public int Beaten = 0;

    private List<M_Action> actions = new List<M_Action>();
    private M_Milestone milestone = null;

    // Check Combat
    public bool Turn { get => turn; }
    private bool turn = true;
    private int check = 0;
    private bool getAction = false;

    private bool isSkip = false;    

    private void Awake()
    {
        if (instance == null) instance = this;
    }    

    [Obsolete]
    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Application.runInBackground = true;
        }

        SoundManager.instance.PlayLoop(acFighting);

        LoadData();
        Scenario();
        Combat();
    }

    // Load Data
    private void LoadData()
    {
        Debug.Log("LoadData");
        milestone = GameManager.instance.milestones[GameManager.instance.idxMilestone];
        txtTurn.text = currentTurn + " / " + milestone.maxTurn;

        dataTeamL.Clear();
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                M_Character nhanVat = new M_Character(GameManager.instance.nhanVats[i]);
                nhanVat.team = 0;
                nhanVat.id_nv = i;
                dataTeamL.Add(new M_Character(nhanVat));
            }
        }
        dataTeamR.Clear();
        for (int i = 0; i < milestone.listCreep.Count; i++)
        {
            M_Character nhanVat = new M_Character(milestone.listCreep[i]);
            nhanVat.team = 1;
            nhanVat.id_nv = i + dataTeamL.Count;
            nhanVat.UpdateLevel();
            dataTeamR.Add(new M_Character(nhanVat));
        }

        Init();

        myTimeScale = arrTimeScale[GameManager.instance.IdxTimeScale];
        txtTimeScale.text = "X" + myTimeScale;
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
            GameObject nvAs = Resources.Load("Prefabs/Character/" + datas[i].id_cfg, typeof(GameObject)) as GameObject;

            if (nvAs == null)
            {
                switch (datas[i].type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = Resources.Load("Prefabs/Character/M1000", typeof(GameObject)) as GameObject;
                        break;
                    default:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
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
            if (isEndGame != C_Enum.EndGame.NOT) break;

            Attack(dataTeamL, dataTeamR, C_Enum.EndGame.WIN);

            Attack(dataTeamR, dataTeamL, C_Enum.EndGame.LOSE);
        }        
    }

    [Obsolete]
    private void Attack(List<M_Character> TeamAttack, List<M_Character> TeamAttacked, C_Enum.EndGame rs)
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
                isEndGame = rs;

                // Nếu win thì kiểm tra số sao
                if (rs == C_Enum.EndGame.WIN)
                {
                    bool isFull = true;
                    bool is3Star = true;
                    for (int j = 0; j < TeamAttack.Count; j++)
                    {
                        if (TeamAttack[j].isDie) isFull = false;
                        else
                        {
                            if (TeamAttack[j].Current_hp <= (TeamAttack[j].max_hp * 0.2)) is3Star = false;
                        }
                    }

                    // Nếu còn đủ team
                    if (isFull)
                    {                        
                        starEndGame = (is3Star) ? 3 : 2;
                    }
                    else starEndGame = 1;
                }
                else starEndGame = 0;

                Debug.Log("=========================== Scenario End Game: " + isEndGame.ToString() + ": " + starEndGame + " Star");

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

    [Obsolete]
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
            // Tính dame mặc định
            int dame = ((actor.atk - targets[i].def) > 0) ? (actor.atk - targets[i].def) : 1;

            // Thêm hệ tương sinh tương khắc
            if (C_Params.SystemCorrelation[actor.element] == targets[i].element) dame = (int)(dame * C_Params.ratioSC);

            // Nếu dame có crit
            bool isCrit = (UnityEngine.Random.RandomRange(0.0f, 100.0f) <= actor.crit);
            if(isCrit) dame = (int)(dame * C_Params.ratioCrit);

            // Nếu target né dame
            bool isDodge = (UnityEngine.Random.RandomRange(0.0f, 100.0f) <= targets[i].dodge);

            if (isDodge)
            {
                // target né đòn
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id_nv;
                    actionC.type = C_Enum.ActionType.DODGE;

                    action.actions.Add(actionC);
                }
            }
            else
            {                
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

                // target giảm hp
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id_nv;
                    actionC.type = C_Enum.ActionType.CHANGE_HP;

                    actionC.prop = new M_Prop();

                    actionC.prop.hpChange = -dame;
                    actionC.prop.hpChangeCrit = isCrit;

                    targets[i].Current_hp += -dame;
                    action.actions.Add(actionC);
                }
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
            if (t <= 0 || isSkip) break;
            Debug.Log("Đếm ngược: " + t);
            txtTime.text = t + " ";
            await Task.Delay(TimeSpan.FromSeconds(1 / myTimeScale));                        
            t -= 1;            
        }
        txtTime.gameObject.SetActive(false);
       
        Debug.Log("Combat");

        while (true)
        {
            if (isSkip)
            {
                CheckTurn();

                if (turn)
                {
                    EndGame();
                    break;
                }
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

                if (actions.Count == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(2 / myTimeScale));
                    EndGame();
                    isSkip = true;
                    break;
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(0.1));
        }
    }

    public void SendEndGame()
    {        
        if (GameManager.instance.test) RecEndGame();
        else UserSendUtil.sendEndGame(milestone.id, GameManager.instance.taikhoan.id, starEndGame, (starEndGame > milestone.star));
    }

    public void RecEndGame()
    {
        milestone.star = Math.Max(milestone.star, starEndGame);

        GameManager.instance.tick_milestonesDic[milestone.id].star = milestone.star;

        // Mở khóa ải tiếp theo
        //Debug.Log(GameManager.instance.idxMilestone);
        //C_Util.GetDumpObject(GameManager.instance.tick_milestonesDic.Values);
        if(isEndGame == C_Enum.EndGame.WIN && GameManager.instance.idxMilestone == GameManager.instance.tick_milestones.Count - 1)
        {
            GameManager.instance.tick_milestones.Add(new M_Milestone(GameManager.instance.idxMilestone + 1, 0));

            GameManager.instance.UpdateTickMS();
        }

        SceneManager.LoadScene("CampaignGame");

        SoundManager.instance.PlayLoop();
    }

    private void EndGame()
    {
        SoundManager.instance.PlayOneShotAs(acEndGame);

        popupEndGame.SetActive(true);
        txtResult.text = isEndGame.ToString();        

        for(int i = 0; i < starEndGame; i++)
        {
            imgResultStars[i].sprite = spStar;
        }
    }

    private void CheckTurn()
    {
        for (int i = 0; i < lstObj.Count; i++)
        {
            if (lstObj[i] != null) {
                if (lstObj[i].nhanvat.isDie || !lstObj[i].gameObject.activeSelf || lstObj[i].isAnim1()) check++;
                else break;
            }
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
                    case C_Enum.ActionType.DODGE:
                        target.isHit = false;
                        actionCs.RemoveAt(i--);
                        break;
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

    public void ScaleTime()
    {
        GameManager.instance.IdxTimeScale++;
        if (GameManager.instance.IdxTimeScale >= arrTimeScale.Length) GameManager.instance.IdxTimeScale = 0;

        myTimeScale = arrTimeScale[GameManager.instance.IdxTimeScale];
        txtTimeScale.text = "X" + myTimeScale;
    }

    public void Skip()
    {
        if (isSkip) return;

        isSkip = true;
        EndGame();
    }

    //void OnGUI()
    //{
    //    GUIStyle myStyle = new GUIStyle();
    //    myStyle.fontSize = 40;
    //    myStyle.normal.textColor = Color.white;

    //    GUI.Label(new Rect(20, 20, 100, 20), "Get Action: " + getAction, myStyle);
    //    GUI.Label(new Rect(20, 70, 100, 20), "Turn: " + turn, myStyle);
    //    GUI.Label(new Rect(20, 120, 100, 20), "Beaten: " + Beaten, myStyle);
    //}
}
