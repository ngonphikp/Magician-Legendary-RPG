using Sfs2X.Entities.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public static PlayGame instance = null;

    private const int size = 5;

    [Header("Position Hero")]
    [SerializeField]
    private GameObject positionHero = null;

    [Header("Position Team L")]
    [SerializeField]
    private Transform[] posL = new Transform[size];
    public Transform pointML = null;

    [Header("Position Team R")]
    [SerializeField]
    private Transform[] posR = new Transform[size];
    public Transform pointMR = null;

    [Header("Data")]
    [SerializeField]
    private M_Character[] teamL = new M_Character[size];
    [SerializeField]
    private M_Character[] teamR = new M_Character[size];

    [Header("Object")]
    [SerializeField]
    private C_Hero[] teamLObj = new C_Hero[size];
    [SerializeField]
    private C_Hero[] teamRObj = new C_Hero[size];

    //[Header("Sage")]
    //[SerializeField]
    //private C_Sage sageLObj = null;
    //[SerializeField]
    //private C_Sage sageRObj = null;

    //[Header("Celestial")]
    //[SerializeField]
    //private C_Sage celLObj = null;
    //[SerializeField]
    //private C_Sage celRObj = null;

    [Header("Targets")]
    public List<C_Hero> targets = new List<C_Hero>();
    public List<C_Control> dfTargets = new List<C_Control>();

    private bool turn = true;
    private int teamLEx = 0;
    private int teamREx = 0;
    private int check = 0;

    public Dictionary<string, C_Control> Objs = new Dictionary<string, C_Control>();
    private bool Turn { get => turn; set { turn = (value != turn) ? value : turn; } }
    public int Beaten = 0;
    [SerializeField]
    private bool getAction = false;
    public bool isEndGame = false;
    private List<List<M_Action>> queueActions = new List<List<M_Action>>();
    [SerializeField]
    private bool isPlay = true;
    private ISFSObject sfsEndGame = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        ChangeTurn();
    }

    private void Update()
    {
        if (queueActions.Count > 0 && isPlay)
        {
            isPlay = false;
            List<M_Action> actions = queueActions[0];
            Play(actions);
            queueActions.RemoveAt(0);
        }
    }

    private async void ChangeTurn()
    {
        while (positionHero.activeSelf)
        {
            // Đếm số Hero ở trạng thái Anim1
            for (int i = 0; i < teamLObj.Length; i++)
                if (teamLObj[i] != null) if (teamLObj[i].isAnim1() || teamLObj[i].isDie) check++; else break;
            for (int i = 0; i < teamRObj.Length; i++)
                if (teamRObj[i] != null) if (teamRObj[i].isAnim1() || teamRObj[i].isDie) check++; else break;

            turn = (check >= (teamREx + teamLEx));
            check = 0;

            await Task.Yield();
        }
    }

    private async void Play(List<M_Action> m_Actions)
    {
        while (true)
        {
            //Nếu hàng đợi khác rỗng lấy action đầu hàng đợi ra chạy
            //actor thực hiện action đang ở trạng thái anim1
            if (m_Actions.Count > 0 && Turn && !getAction)// && FightingUI.instance.isEndTurn
            {
                M_Action m_Action = m_Actions[0];
                getAction = true;                

                string id_actor = m_Action.actor;
                int id = m_Action.id;
                C_Control obj = Objs[id_actor];
                switch (id)
                {
                    case (int)C_Enum.idAction.SKILLING:
                        SKILLING(id_actor, m_Action, obj, m_Actions);
                        break;
                    case (int)C_Enum.idAction.BEATEN:
                        Debug.Log("===========================================>" + id_actor + ": BEATEN");
                        //m_Action.getDumpObject();
                        break;
                    case (int)C_Enum.idAction.APPLY_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": APPLY_EFFECT");
                        // m_Action.getDumpObject();
                        EFFECT_CHANGE(id_actor, m_Action, (C_Hero)obj);
                        break;
                    case (int)C_Enum.idAction.REMOVED_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": REMOVED_EFFECT");
                        // m_Action.getDumpObject();
                        EFFECT_CHANGE(id_actor, m_Action, (C_Hero)obj);
                        break;
                    case (int)C_Enum.idAction.DODGE:
                        Debug.Log("===========================================>" + id_actor + ": DODGE");
                        //m_Action.getDumpObject();
                        break;
                    case (int)C_Enum.idAction.DIE:
                        Debug.Log("===========================================>" + id_actor + ": DIE");
                        ((C_Hero)obj).isDie = true;
                        ((C_Hero)obj).Play(6);
                        break;
                    case (int)C_Enum.idAction.HEALTH_CHANGE:
                        HEALTH_CHANGE(id_actor, m_Action, (C_Hero)obj);
                        break;
                    case (int)C_Enum.idAction.ENERGY_CHANGE:
                        ENERGY_CHANGE(id_actor, m_Action, obj);
                        break;
                    default:
                        Debug.Log("===========================================>" + id_actor + ": Default");
                        //m_Action.getDumpObject();
                        break;
                }

                await Task.Delay(System.TimeSpan.FromSeconds(Time.fixedDeltaTime / 10));

                if (m_Action.actions.Count > 0) Debug.Log("abc");// m_Action.actions.ForEach(x => x.getDumpObject());
                else
                {
                    // Nếu là action cuối cùng                    
                    if (m_Actions.Count == 1)
                    {
                        Debug.Log("Danh turn cuoi cung");
                        EndFight();
                    }
                }
                m_Actions.RemoveAt(0); countPlay++;
                getAction = false;
            }
            await Task.Yield();
        }
    }

    private async void EndFight()
    {
        while (true)
        {
            if (Beaten == 0)
            {                
                //FightingUI.instance.ShowBoard(true);

                // Nếu End Game
                if (isEndGame)// && RewardEndGame.instance
                {
                    Debug.Log("====================================End Game ================================================");
                    //sfsEndGame.getDumpObject();
                    //RewardEndGame.instance.SetDataReward(sfsEndGame);
                }
                isPlay = true;
                break;
            }
            await Task.Yield();
        }
        
    }

    private void SKILLING(string id_actor, M_Action m_Action, C_Control obj, List<M_Action> m_Actions)
    {
        Debug.Log("_____________________________Diễn Action Skill___________________________");

        List<string> idTargets = m_Action.prop.par0d1;
        Beaten = idTargets.Count;
        this.targets.Clear();
        this.dfTargets.Clear();

        // Tìm và xóa 1 số action của actor cùng cấp
        Debug.Log("Tìm và xóa 1 số action của actor cùng cấp: " + id_actor);
        M_Action rs = FindActionOfActor(id_actor, m_Actions, obj);

        // Tìm và xóa 1 số action của actor cấp con
        Debug.Log("Tìm và xóa 1 số action của actor cấp con: " + id_actor);
        if (rs == null) rs = FindActionOfActor(id_actor, m_Action.actions, obj);

        // Tìm và xóa 1 số action của DFtarget cấp con
        Debug.Log("Tìm và xóa 1 số action của !target cấp con");
        FindActionOfDFTarget(m_Action.actions, idTargets);

        // Tìm và xóa 1 số action của target cấp con thêm vào this.targets
        Debug.Log("Tìm và xóa 1 số action target cấp con");
        FindActionOfTarget(m_Action.actions, idTargets);

        // Diễn Skill khi có target
        if (this.targets.Count > 0 || m_Action.prop.par0d2)
        {
            Debug.Log("Diễn skill");
            obj.Play(m_Action.prop.par0d0, m_Action.prop.par0d2);
        }
        // Diễn thay đổi HP nếu có
        // Debug.Log("Diễn thay đổi HP nếu có");
        if(obj is C_Hero)((C_Hero)obj).ChangeHp();
        // Diễn thay đổi EP nếu có
        // Debug.Log("Diễn thay đổi EP nếu có");
        obj.ChangeEp();
        // Diễn thay đổi Effect nếu có
        // Debug.Log("Diễn thay đổi Effect nếu có");
        if (obj is C_Hero) ((C_Hero)obj).ChangeEffect();
        // Diễn die nếu true
        if (obj is C_Hero && ((C_Hero)obj).isDie && obj != null)
        {
            Debug.Log("===============================================================> Actor Die: " + ((C_Hero)obj).character.actor_id);
            // Nếu có diễn Skill
            if (this.targets.Count > 0 || m_Action.prop.par0d2)
            {
                Debug.Log("===============================================================> Actor Skill == > Die: " + ((C_Hero)obj).character.actor_id);
                // Đẩy action Die lên vị trí số 1
                if (rs != null) m_Actions.Insert(1, rs);
            }
            // Nếu không
            else
            {
                ((C_Hero)obj).Play(6);
            }
        }

        // Diễn dfTarget
        for (int i = 0; i < dfTargets.Count; i++)
        {
            // Diễn thay đổi HP nếu có
            // Debug.Log("Diễn thay đổi HP nếu có");
            if (obj is C_Hero) ((C_Hero)dfTargets[i]).ChangeHp();
            // Diễn thay đổi EP nếu có
            // Debug.Log("Diễn thay đổi EP nếu có");
            dfTargets[i].ChangeEp();
            // Diễn thay đổi Effect nếu có
            // Debug.Log("Diễn thay đổi Effect nếu có");
            if (obj is C_Hero) ((C_Hero)dfTargets[i]).ChangeEffect();

            // Diễn die nếu true
            if (obj is C_Hero && ((C_Hero)dfTargets[i]).isDie)
            {
                Debug.Log("===============================================================> dfTarget Die: " + ((C_Hero)dfTargets[i]).character.actor_id);
                dfTargets[i].Play(6);
            }
        }

        // Tìm và xóa action cấp con còn lại ==> Còn: skill and die bằng cách diễn mảng con
        Debug.Log("Tìm và xóa action cấp con còn lại");
        FindActionOther(m_Action.actions);

        // Diễn mảng con
        if (m_Action.actions.Count > 0)
        {
            Debug.Log("Diễn mảng con");
            Play(m_Action.actions);
        }
    }

    private void EFFECT_CHANGE(string id_actor, M_Action m_Action, C_Hero actor)
    {
        // Thêm thay đổi effect
        Debug.Log("Thêm thay đổi effect " + id_actor + " cùng cấp");
        actor.PushChangeEffect(m_Action.prop);
        // Diễn thay đổi effect nếu có
        // Debug.Log("Diễn thay đổi effect nếu có");
        actor.ChangeEffect();
    }

    private void HEALTH_CHANGE(string id_actor, M_Action m_Action, C_Hero actor)
    {
        // Thêm thay đổi hp
        Debug.Log("Thêm thay đổi hp " + id_actor + " cùng cấp");
        actor.PushChangeHp(m_Action.prop);
        // Diễn thay đổi HP nếu có
        // Debug.Log("Diễn thay đổi HP nếu có");
        actor.ChangeHp();
    }

    private void ENERGY_CHANGE(string id_actor, M_Action m_Action, C_Control obj)
    {
        // Thêm thay đổi ep
        Debug.Log("Thêm thay đổi ep " + id_actor + " cùng cấp");
        obj.PushChangeEp(m_Action.prop);
        // Diễn thay đổi EP nếu có
        // Debug.Log("Diễn thay đổi EP nếu có");
        obj.ChangeEp();
    }

    private void FindActionOfTarget(List<M_Action> m_Actions, List<string> idTargets)
    {
        for (int i = 0; i < m_Actions.Count; i++)
        {
            M_Action m_Action = m_Actions[i];
            string id_actor = m_Action.actor;
            // Nếu nó thuộc idTargets
            if (idTargets.Contains(id_actor))
            {
                C_Control target = Objs[id_actor];
                switch (m_Action.id)
                {
                    case (int)C_Enum.idAction.BEATEN:
                        // Thay đổi => trúng đòn
                        Debug.Log("Trúng đòn " + id_actor + " cấp con");
                        ((C_Hero)target).isHit = true;

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.APPLY_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": APPLY_EFFECT IN SKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor + " cấp con");
                        ((C_Hero)target).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.REMOVED_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": REMOVED_EFFECT IN SKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor + " cấp con");
                        ((C_Hero)target).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.DODGE:
                        // Thay đổi => né đòn
                        Debug.Log("Né đòn " + id_actor + " cấp con");
                        ((C_Hero)target).isHit = false;

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.DIE:
                        Debug.Log("===========================================>" + id_actor + ": DIE IN SKILLING");
                        //m_Action.getDumpObject();
                        // Kiểm tra xem actor có dùng skill cùng cấp hay cấp con 
                        // Nếu không có
                        if (!CheckIsSkill(m_Action, m_Actions))
                        {
                            // Thêm thay đổi die
                            Debug.Log("Die " + id_actor + " cấp con");
                            if (target != null) ((C_Hero)target).isDie = true;

                            //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                            m_Actions.RemoveAt(i--); countPlay++;
                        }
                        break;
                    case (int)C_Enum.idAction.HEALTH_CHANGE:
                        // Thêm thay đổi hp
                        Debug.Log("Thêm thay đổi hp " + id_actor + " cấp con");
                        ((C_Hero)target).PushChangeHp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.ENERGY_CHANGE:
                        // Thêm thay đổi ep
                        Debug.Log("Thêm thay đổi ep " + id_actor + " cấp con");
                        target.PushChangeEp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                }

                // Nếu tồn tại target trong mảng
                int idx = idxTarget(id_actor);
                if (idx != -1)
                {
                    this.targets[idx] = (C_Hero)target;
                }
                // Nếu chưa tồn tại
                else
                {
                    this.targets.Add((C_Hero)target);
                }
            }
        }
    }

    private void FindActionOfDFTarget(List<M_Action> m_Actions, List<string> idTargets)
    {
        for (int i = 0; i < m_Actions.Count; i++)
        {
            M_Action m_Action = m_Actions[i];
            string id_actor = m_Action.actor;
            // Nếu nó không thuộc idTargets ~ không phải target
            if (!idTargets.Contains(id_actor))
            {
                C_Control dfTarget = Objs[id_actor];
                switch (m_Action.id)
                {
                    case (int)C_Enum.idAction.APPLY_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": APPLY_EFFECT IN SKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor + " cấp con");
                        ((C_Hero)dfTarget).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.REMOVED_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": REMOVED_EFFECT IN SKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor + " cấp con");
                        ((C_Hero)dfTarget).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.DIE:
                        Debug.Log("===========================================>" + id_actor + ": DIE IN SKILLING");
                        //m_Action.getDumpObject();
                        // Kiểm tra xem actor có dùng skill cùng cấp hay cấp con 
                        // Nếu không có
                        if (!CheckIsSkill(m_Action, m_Actions))
                        {
                            // Thêm thay đổi die
                            Debug.Log("Die " + id_actor + " cấp con");
                            ((C_Hero)dfTarget).isDie = true;

                            //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                            m_Actions.RemoveAt(i--); countPlay++;
                        }
                        break;
                    case (int)C_Enum.idAction.HEALTH_CHANGE:
                        // Thêm thay đổi hp
                        Debug.Log("Thêm thay đổi hp " + id_actor + " cấp con");
                        ((C_Hero)dfTarget).PushChangeHp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                    case (int)C_Enum.idAction.ENERGY_CHANGE:
                        // Thêm thay đổi ep
                        Debug.Log("Thêm thay đổi ep " + id_actor + " cấp con");
                        if (dfTarget != null) dfTarget.PushChangeEp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i--); countPlay++;
                        break;
                }
                // Nếu tồn tại dfTarget trong mảng
                int idx = idxDfTarget(id_actor);
                if (idx != -1)
                {
                    this.dfTargets[idx] = dfTarget;
                }
                // Nếu chưa tồn tại
                else
                {
                    this.dfTargets.Add(dfTarget);
                }
            }
        }
    }

    private void FindActionOther(List<M_Action> m_Actions)
    {
        for (int i = 0; i < m_Actions.Count; i++)
        {
            M_Action m_Action = m_Actions[i];
            string id_actor = m_Action.actor;
            Debug.Log("===========================================>ActionOther");
            //m_Action.getDumpObject();

            //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
            //m_Actions.RemoveAt(i--);
        }
    }

    private M_Action FindActionOfActor(string id_actor, List<M_Action> m_Actions, C_Control obj)
    {
        M_Action rs = null;
        for (int i = 0; i < m_Actions.Count; i++)
        {
            M_Action m_Action = m_Actions[i];
            if (m_Action.actor == id_actor && m_Action.id != (int)C_Enum.idAction.SKILLING)
            {
                switch (m_Action.id)
                {
                    case (int)C_Enum.idAction.APPLY_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": APPLY_EFFECT IN FindObjOfSKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor);
                        ((C_Hero)obj).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i); countPlay++;
                        i--;
                        break;
                    case (int)C_Enum.idAction.REMOVED_EFFECT:
                        // Debug.Log("===========================================>" + id_actor + ": REMOVED_EFFECT IN FindObjOfSKILLING");
                        // m_Action.getDumpObject();
                        // Thêm thay đổi effect
                        Debug.Log("Thêm thay đổi effect " + id_actor);
                        ((C_Hero)obj).PushChangeEffect(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i); countPlay++;
                        i--;
                        break;
                    case (int)C_Enum.idAction.DIE:
                        Debug.Log("===========================================>" + id_actor + ": DIE IN FindObjOfSKILLING");
                        //m_Action.getDumpObject();
                        // Thêm thay đổi die
                        Debug.Log("Die " + id_actor);
                        ((C_Hero)obj).isDie = true;

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i); countPlay++;
                        i--;
                        rs = m_Action;
                        break;
                    case (int)C_Enum.idAction.HEALTH_CHANGE:
                        // Thêm 1 thay đổi HP
                        Debug.Log("Thêm 1 thay đổi HP " + id_actor);
                        ((C_Hero)obj).PushChangeHp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i); countPlay++;
                        i--;
                        break;
                    case (int)C_Enum.idAction.ENERGY_CHANGE:
                        // Thêm 1 thay đổi EP
                        Debug.Log("Thêm 1 thay đổi EP " + id_actor);
                        obj.PushChangeEp(m_Action.prop);

                        //if (m_Action.actions.Count > 0) m_Action.actions.ForEach(x => x.getDumpObject());
                        m_Actions.RemoveAt(i); countPlay++;
                        i--;
                        break;
                }
            }
        }
        return rs;
    }

    private bool CheckIsSkill(M_Action action, List<M_Action> actions)
    {
        if (iCheckIsSkill(action.actor, actions)) return true;
        if (iCheckIsSkill(action.actor, action.actions)) return true;
        return false;
    }

    private bool iCheckIsSkill(string id_actor, List<M_Action> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].actor == id_actor && actions[i].id == (int)C_Enum.idAction.SKILLING) return true;
        }
        return false;
    }

    private int idxTarget(string id_target)
    {
        for (int i = 0; i < this.targets.Count; i++)
        {
            if (((C_Hero)this.targets[i]).character.actor_id == id_target) return i;
        }
        return -1;
    }

    private int idxDfTarget(string id_dfTarget)
    {
        for (int i = 0; i < this.dfTargets.Count; i++)
        {
            if (((C_Hero)this.dfTargets[i]).character.actor_id == id_dfTarget) return i;
        }
        return -1;
    }

    public void RecEndGame(ISFSObject sFsEndGame)
    {
        Debug.Log("---------------------------RecEndGame-----------------------------");

        this.sfsEndGame = sFsEndGame;
        isEndGame = true;
    }

    public void RecFightting(List<M_Action> actions)
    {
        Debug.Log("---------------------------RecFightting-----------------------------");
        if (actions.Count > 0) queueActions.Add(actions);
        //actions.getDumpObject();
        Debug.Log("---------------------------EndFightting-----------------------------");
    }

    public void RecTeam(M_Character[] teamL, M_Character[] teamR)
    {
        this.teamL = teamL;
        this.teamR = teamR;

        Init();
    }

    //public void RecSage(M_Sage sageL, M_Sage sageR)
    //{
    //    Debug.Log("---------------------------RecSage-----------------------------");
    //    sageLObj.set(sageL);
    //    Objs.Add(sageL.actor_id, sageLObj);
    //    Objs.Keys.getDumpObject();

    //    sageRObj.set(sageR);
    //}

    //public void RecCel(M_Sage celL, M_Sage celR)
    //{
    //    Debug.Log("---------------------------RecCel-----------------------------");
    //    celL.getDumpObject();
    //    celLObj.set(celL);
    //    Objs.Add(celL.actor_id, celLObj);
    //    Objs.Keys.getDumpObject();

    //    celRObj.set(celR);
    //}

    public void RequestSageSkill(string SSid)
    {
        //FightingSendUtil.RequestSageSkillFighting(SSid);
        //FightingUI.instance.ShowBoard(false);
    }

    public void RequestCelSkill()
    {
        //FightingSendUtil.RequestCelSkillFighting();
        //FightingUI.instance.ShowBoard(false);
    }

    public void RequestHeroSkill(string actor_id)
    {
        //FightingSendUtil.RequestHeroSkillFighting(actor_id);
        //FightingUI.instance.ShowBoard(false);
    }

    private void Init()
    {
        Debug.Log("Init");
        InitTeam(teamL, ref teamLObj, posL);
        InitTeam(teamR, ref teamRObj, posR);
    }

    private void InitTeam(M_Character[] datas, ref C_Hero[] objs, Transform[] pos)
    {
        objs = new C_Hero[size];
        for (int i = 0; i < datas.Length; i++)
        {
            M_Character objBattle = datas[i];
            if (objBattle.isEx)
            {
                if (objBattle.team == 0) teamLEx++;
                if (objBattle.team == 1) teamREx++;
                InitHeroObj(objBattle, pos[i], ref objs[i]);
                Objs.Add(objBattle.actor_id, objs[i]);
            }
        }
        //Objs.Keys.getDumpObject();
    }

    private void InitHeroObj(M_Character character, Transform pos, ref C_Hero obj)
    {
        //GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Hero/" + character.id);

        // Test
        //if (heroAs == null) heroAs = QuickFunction.getAssetPref("Prefabs/Hero/T1011");

        //if (heroAs != null)
        //{
        //    obj = Instantiate(heroAs, pos).GetComponent<C_Hero>();
        //    obj.SetCharater(character, true);
        //}
    }

    // Test check play
    public int countPlay = 0;
    public int sumPlay = 0;
    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 40;

        GUI.Label(new Rect(20, 20, 100, 20), "Play Action: " + countPlay, myStyle);
        GUI.Label(new Rect(20, 70, 100, 20), "Sum Action: " + sumPlay, myStyle);
        GUI.Label(new Rect(20, 110, 100, 20), "Play: " + isPlay, myStyle);
        GUI.Label(new Rect(20, 150, 100, 20), "Get Action: " + getAction, myStyle);
        GUI.Label(new Rect(20, 190, 100, 20), "Turn: " + Turn, myStyle);
        GUI.Label(new Rect(20, 230, 100, 20), "Beaten: " + Beaten, myStyle);
    }
}