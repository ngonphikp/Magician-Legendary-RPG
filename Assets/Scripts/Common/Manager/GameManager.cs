using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)
    public Dictionary<string, M_Hero> herosDic = new Dictionary<string, M_Hero>();

    public float myTimeScale = 1.0f;
    public bool isScaleTime = false;

    private void Awake()
    {
        MakeSingleInstance();
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

    public void ShowNoti(string txt)
    {
        Debug.Log("=======================================================ShowNooti: " + txt);
    }

    public void ScaleTime()
    {
        isScaleTime = !isScaleTime;
        myTimeScale = (isScaleTime) ? 2.0f : 1.0f;
    }
}
