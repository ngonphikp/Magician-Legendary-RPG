using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)

    public List<M_Hero> heros = new List<M_Hero>();
    public Dictionary<string, M_Hero> herosDic = new Dictionary<string, M_Hero>();

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
        heros = convert.GetListHero();

        //list.ForEach(x => Debug.Log(x.id + " / " + x.name + " / " + x.star + " / " + x.description + x.element + " / " + x.kingdom + " / " + x.Class));

        herosDic = new Dictionary<string, M_Hero>(heros.Count);
        heros.ForEach(x => herosDic.Add(x.id, x));
    }
}
