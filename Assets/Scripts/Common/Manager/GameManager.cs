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
    public static GameManager instance = null;

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)

    // Data Config
    public List<M_Hero> heros = new List<M_Hero>();
    public Dictionary<string, M_Hero> herosDic = new Dictionary<string, M_Hero>();

    // Data User
    public M_TaiKhoan taikhoan = new M_TaiKhoan();
    public List<M_Hero> listHero = new List<M_Hero>();

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
    }

    private void LoadListHero()
    {
        JSonConvert convert = new JSonConvert();
        heros = convert.GetListHero().ToList<M_Hero>();

        herosDic = new Dictionary<string, M_Hero>(heros.Count);
        heros.ForEach(x => herosDic.Add(x.id_cfg, x));
    }

    private void Start()
    {
        SmartFoxConnection.Connect();
    }

    private void Update()
    {
        SmartFoxConnection.ListenerEvent();
    }
}
