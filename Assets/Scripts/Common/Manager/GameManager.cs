using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool demo = false;

    public static GameManager instance = null;

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)

    // Data Config
    public List<M_Hero> heros = new List<M_Hero>();
    public Dictionary<string, M_Hero> herosDic = new Dictionary<string, M_Hero>();

    public List<M_Creep> creeps = new List<M_Creep>();
    public Dictionary<string, M_Creep> creepsDic = new Dictionary<string, M_Creep>();

    public List<M_Milestone> milestones = new List<M_Milestone>();
    public Dictionary<int, M_Milestone> milestonesDic = new Dictionary<int, M_Milestone>();

    // Data User
    public M_TaiKhoan taikhoan = new M_TaiKhoan();
    public List<M_NhanVat> nhanVats = new List<M_NhanVat>();

    // Arrange & Fighting
    public bool isAttack = false;
    public int idxMilestone = 0;

    private void Awake()
    {
        MakeSingleInstance();
        LoadConfigJson();
    }

    private void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LoadConfigJson()
    {
        LoadListHero();
        LoadListCreep();
        LoadListMilestone();
    }

    private void LoadListHero()
    {
        JSonConvert convert = new JSonConvert();
        heros = convert.GetListHero().ToList<M_Hero>();

        herosDic = new Dictionary<string, M_Hero>(heros.Count);
        heros.ForEach(x => herosDic.Add(x.id_cfg, x));
    }

    private void LoadListCreep()
    {
        JSonConvert convert = new JSonConvert();
        creeps = convert.GetListCreep().ToList<M_Creep>();

        creepsDic = new Dictionary<string, M_Creep>(creeps.Count);
        creeps.ForEach(x => creepsDic.Add(x.id_cfg, x));
    }

    private void LoadListMilestone()
    {
        JSonConvert convert = new JSonConvert();
        milestones = convert.GetListMilestone().ToList<M_Milestone>();

        milestonesDic = new Dictionary<int, M_Milestone>(milestones.Count);
        milestones.ForEach(x => milestonesDic.Add(x.id, x));
    }

    private void Start()
    {
        if (demo)
        {
            taikhoan = new M_TaiKhoan();
            taikhoan.id = 99;
            taikhoan.usename = "username99";
            taikhoan.password = "password99";
            taikhoan.name = "name99";

            nhanVats.Clear();

            int[] arrIdx = { 0, 2, 4, 6, 8};

            for(int i = 0; i < 5; i++)
            {
                nhanVats.Add(new M_NhanVat(i, "T100" + UnityEngine.Random.Range(2, 8), 99, UnityEngine.Random.Range(1, 100), arrIdx[i]));
            }

            for (int i = 5; i < 15; i++)
            {
                nhanVats.Add(new M_NhanVat(i, "T100" + UnityEngine.Random.Range(2, 8), 99, UnityEngine.Random.Range(1, 100), -1));
            }

            ScenesManager.instance.ChangeScene("HomeGame");

            return;
        }

        SmartFoxConnection.Connect();
    }

    private void Update()
    {
        if (demo) return;
        SmartFoxConnection.ListenerEvent();
    }
}
