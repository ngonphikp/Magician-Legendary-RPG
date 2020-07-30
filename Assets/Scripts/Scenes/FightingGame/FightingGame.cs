using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    public Dictionary<int, C_Character> Objs = new Dictionary<int, C_Character>();

    public float myTimeScale = 1.0f;
    public bool isScaleTime = false;

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
    }

    private void LoadData()
    {
        dataTeamL.Clear();
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if(GameManager.instance.nhanVats[i].idx != -1)
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
        Debug.Log("Init");
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
                c_obj.Set(datas[i]);

                Objs.Add(datas[i].id_nv, c_obj);
            }
        }
    }
}
