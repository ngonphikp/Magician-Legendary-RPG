using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DisplayObj : MonoBehaviour
{
    [SerializeField]
    private GameObject obj = null;

    public void ShowGameObj()
    {
        obj.SetActive(true);
    }

    public void HideGameObj()
    {
        obj.SetActive(false);
    }
}
