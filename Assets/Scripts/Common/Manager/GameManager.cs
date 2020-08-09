﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string host = "192.168.1.46";

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)

    // Data Config
    public List<M_Character> heros = new List<M_Character>();
    public Dictionary<string, M_Character> herosDic = new Dictionary<string, M_Character>();

    public List<M_Character> creeps = new List<M_Character>();
    public Dictionary<string, M_Character> creepsDic = new Dictionary<string, M_Character>();

    public List<M_Milestone> milestones = new List<M_Milestone>();
    public Dictionary<int, M_Milestone> milestonesDic = new Dictionary<int, M_Milestone>();

    public List<M_Skill> skills = new List<M_Skill>();
    public Dictionary<string, M_Skill> skillsDic = new Dictionary<string, M_Skill>();

    // Data User
    public M_TaiKhoan taikhoan = new M_TaiKhoan();
    public List<M_Character> nhanVats = new List<M_Character>();
    public List<M_Milestone> tick_milestones = new List<M_Milestone>();
    public Dictionary<int, M_Milestone> tick_milestonesDic = new Dictionary<int, M_Milestone>();

    // Arrange & Fighting
    public bool isAttack = false;
    public int idxMilestone = 0;

    private int idxTimeScale = 0;
    public int IdxTimeScale { 
        get => idxTimeScale;
        set {
            idxTimeScale = value;
            PlayerPrefs.SetInt("idxTimeScale", idxTimeScale);
        } 
    }

    // Infor
    public int idxCharacter = 0;

    // Test
    public bool test = false;

    private void Awake()
    {
        MakeSingleInstance();
        LoadConfigJson();
        LoadLocalStorage();
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

    private void LoadLocalStorage()
    {
        idxTimeScale = PlayerPrefs.GetInt("idxTimeScale", 0);
    }

    private void LoadConfigJson()
    {
        LoadListHero();
        LoadListCreep();
        LoadListMilestone();
        LoadListSkill();
    }

    private void LoadListSkill()
    {
        JSonConvert convert = new JSonConvert();
        skills = convert.GetListSkill().ToList<M_Skill>();

        skillsDic = new Dictionary<string, M_Skill>(skills.Count);
        skills.ForEach(x => skillsDic.Add(x.id_cfg, x));
    }

    private void LoadListHero()
    {
        JSonConvert convert = new JSonConvert();
        heros = convert.GetListHero().ToList<M_Character>();

        herosDic = new Dictionary<string, M_Character>(heros.Count);
        heros.ForEach(x => herosDic.Add(x.id_cfg, x));
    }

    private void LoadListCreep()
    {
        JSonConvert convert = new JSonConvert();
        creeps = convert.GetListCreep().ToList<M_Character>();

        creepsDic = new Dictionary<string, M_Character>(creeps.Count);
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
        SmartFoxConnection.Connect();
    }

    public void UpdateTickMS()
    {
        tick_milestonesDic = new Dictionary<int, M_Milestone>(tick_milestones.Count);
        tick_milestones.ForEach(x => tick_milestonesDic.Add(x.id, x));
    }

    private void Update()
    {
        if (test) return;
        SmartFoxConnection.ListenerEvent();
    }

    public void TestPlay()
    {
        test = true;
        taikhoan = new M_TaiKhoan();
        taikhoan.id = 99;
        taikhoan.usename = "username99";
        taikhoan.password = "password99";
        taikhoan.name = "name99";

        nhanVats.Clear();

        int[] arrIdx = { 0, 2, 4, 6, 8 };

        for (int i = 0; i < 5; i++)
        {
            M_Character nhanVat = new M_Character(i, "T100" + UnityEngine.Random.Range(2, 8), taikhoan.id, i + 1, arrIdx[i]);
            nhanVat.type = C_Enum.CharacterType.Hero;
            nhanVat.UpdateById();
            nhanVat.UpdateLevel();
            nhanVats.Add(nhanVat);
        }

        for (int i = 5; i < 10; i++)
        {
            M_Character nhanVat = new M_Character(i, "T100" + UnityEngine.Random.Range(2, 8), taikhoan.id, UnityEngine.Random.Range(1, 15), -1);
            nhanVat.type = C_Enum.CharacterType.Hero;
            nhanVat.UpdateById();
            nhanVat.UpdateLevel();
            nhanVats.Add(nhanVat);
        }

        tick_milestones.Clear();

        for (int i = 0; i < 3; i++)
        {
            tick_milestones.Add(new M_Milestone(i, UnityEngine.Random.Range(1, 4)));
        }

        tick_milestones.Add(new M_Milestone(tick_milestones.Count, 0));

        UpdateTickMS();

        ScenesManager.instance.ChangeScene("HomeGame");
        SoundManager.instance.PlayLoop();
    }

    public void OnApplicationQuit()
    {
        LoginSendUtil.sendLogout();
        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
    }

}
