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
}
