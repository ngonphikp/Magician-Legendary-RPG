using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_UIMagician : MonoBehaviour
{
    [SerializeField]
    private Image Ep = null;
    [SerializeField]
    private Transform texts = null;
    [SerializeField]
    private GameObject textPrbEP1 = null;
    [SerializeField]
    private GameObject textPrbEP2 = null;
    [SerializeField]
    private GameObject textPrbEP2r = null;

    private bool isRight = false;

    [Header("Anim Chang Hp and Ep")]
    [SerializeField]
    private float anim = 0.5f;
    public float ep = 1.0f;

    private float curEp = 0.345f;

    private void Update()
    {
        if (curEp != ep) ChangEp();
    }

    private void ChangEp()
    {
        float dis = ep - curEp;
        if (dis < 0) dis *= -1;
        curEp = Mathf.Lerp(curEp, ep, Time.deltaTime * anim * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1) / dis);
        Ep.fillAmount = curEp;
    }

    public void CreateText(string str, C_Enum.TypeText type)
    {
        GameObject text = null;
        switch (type)
        {
            case C_Enum.TypeText.EP1:
                text = Instantiate(textPrbEP1, texts);
                break;
            case C_Enum.TypeText.EP2:
                text = (!isRight) ? Instantiate(textPrbEP2, texts) : Instantiate(textPrbEP2r, texts);
                break;
            default:
                break;
        }
        if (text != null) text.GetComponent<Text>().text = str;
    }

    public void set(int team)
    {
        this.isRight = (team == 1);
    }
}
