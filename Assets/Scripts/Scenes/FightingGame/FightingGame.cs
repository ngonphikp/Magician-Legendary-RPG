using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FightingGame : MonoBehaviour
{
    public static FightingGame instance = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private C_CellAT[] teamL = null;
    [SerializeField]
    private C_CellAT[] teamR = null;

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
        LoadListHero();

        LoadListCreep();
    }

    private async void LoadListHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                teamL[GameManager.instance.nhanVats[i].idx].set(GameManager.instance.nhanVats[i], canvas);
            }
        }

        await Task.Yield();
    }

    private async void LoadListCreep()
    {
        List<M_Creep> creeps = GameManager.instance.milestones[GameManager.instance.idxMilestone].listCreep;

        for (int i = 0; i < creeps.Count; i++)
        {
            if (creeps[i].idx != -1)
            {
                teamR[creeps[i].idx].set(creeps[i], canvas);
            }
        }

        await Task.Yield();
    }
}
