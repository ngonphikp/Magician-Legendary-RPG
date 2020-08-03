using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_UICharacter : MonoBehaviour
{
    [SerializeField]
    private Image Hp = null;
    [SerializeField]
    private Image Ep = null;
    [SerializeField]
    private Text lvTxt = null;
    [SerializeField]
    private Image elImg = null;

    [SerializeField]
    private Transform texts = null;
    [SerializeField]
    private GameObject textPrbHP1 = null;
    [SerializeField]
    private GameObject textPrbHP2 = null;
    [SerializeField]
    private GameObject textPrbEP1 = null;
    [SerializeField]
    private GameObject textPrbEP2 = null;
    [SerializeField]
    private GameObject textPrbDG = null;

    private bool isRight = false;

    [Header("Anim Chang Hp and Ep")]
    [SerializeField]
    private float anim = 0.5f;
    public float hp = 1.0f;
    public float ep = 1.0f;

    private float curHp = 0.345f;
    private float curEp = 0.345f;

    private void Update()
    {
        if (curHp != hp) ChangHp();
        if (curEp != ep) ChangEp();
    }

    private void ChangHp()
    {
        float dis = hp - curHp;
        if (dis < 0) dis *= -1;
        curHp = Mathf.Lerp(curHp, hp, Time.deltaTime * anim / dis);
        Hp.fillAmount = curHp;
    }
    private void ChangEp()
    {
        float dis = ep - curEp;
        if (dis < 0) dis *= -1;
        curEp = Mathf.Lerp(curEp, ep, Time.deltaTime * anim / dis);
        Ep.fillAmount = curEp;
    }

    public void set(C_Character ctl)
    {
        gameObject.SetActive(true);
        lvTxt.text = ctl.nhanvat.lv.ToString();
        this.isRight = (ctl.nhanvat.team == 1);
        if (isRight) UIRight();
    }

    private void UIRight()
    {
        Vector3 scl = gameObject.transform.localScale;
        scl.x *= -1;
        gameObject.transform.localScale = scl;
    }

    public void CreateText(C_Enum.TypeText type, string str = "")
    {
        GameObject text = null;
        switch (type)
        {
            case C_Enum.TypeText.HP1:
                text = Instantiate(textPrbHP1, texts);
                break;
            case C_Enum.TypeText.HP2:
                text = Instantiate(textPrbHP2, texts);
                break;
            case C_Enum.TypeText.EP1:
                text = Instantiate(textPrbEP1, texts);
                break;
            case C_Enum.TypeText.EP2:
                text = Instantiate(textPrbEP2, texts);
                break;
            case C_Enum.TypeText.DG:
                text = Instantiate(textPrbDG, texts);
                break;
            default:
                break;
        }
        if (text != null && text.GetComponent<Text>() != null) text.GetComponent<Text>().text = str;
    }
}
