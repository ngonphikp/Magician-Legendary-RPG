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
    public List<C_Hero> targetBuffs = new List<C_Hero>();
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
}