using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_UIHero : MonoBehaviour
{
    [SerializeField]
    private Sprite enemyHp = null;
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
    private GameObject btnHeroSkill = null;
    [SerializeField]
    private GameObject textPrbHP1 = null;
    [SerializeField]
    private GameObject textPrbHP2 = null;
    [SerializeField]
    private GameObject textPrbHP2r = null;
    [SerializeField]
    private GameObject textPrbEP1 = null;
    [SerializeField]
    private GameObject textPrbEP2 = null;
    [SerializeField]
    private GameObject textPrbEP2r = null;
    [SerializeField]
    private GameObject textPrbDG = null;
    [SerializeField]
    private GameObject textPrbDGr = null;
    [SerializeField]
    private GameObject textPrbMiss = null;
    [SerializeField]
    private GameObject textPrbMissr = null;

    [SerializeField]
    private GameObject SoftEffectPrb = null;
    [SerializeField]
    private Transform posSE = null;

    private Dictionary<string, C_SoftEffect> softEffects = new Dictionary<string, C_SoftEffect>();

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
        curHp = Mathf.Lerp(curHp, hp, Time.deltaTime * anim * GameManager.instance.myTimeScale / dis);
        Hp.fillAmount = curHp;
    }
    private void ChangEp()
    {
        float dis = ep - curEp;
        if (dis < 0) dis *= -1;
        curEp = Mathf.Lerp(curEp, ep, Time.deltaTime * anim * GameManager.instance.myTimeScale / dis);
        Ep.fillAmount = curEp;
    }

    public void CreateText(string str, C_Enum.TypeText type)
    {
        GameObject text = null;
        switch (type)
        {
            case C_Enum.TypeText.HP1:
                text = Instantiate(textPrbHP1, texts);
                break;
            case C_Enum.TypeText.HP2:
                text = (!isRight) ? Instantiate(textPrbHP2, texts) : Instantiate(textPrbHP2r, texts);
                break;
            case C_Enum.TypeText.EP1:
                text = Instantiate(textPrbEP1, texts);
                break;
            case C_Enum.TypeText.EP2:
                text = (!isRight) ? Instantiate(textPrbEP2, texts) : Instantiate(textPrbEP2r, texts);
                break;
            case C_Enum.TypeText.DG:
                text = (!isRight) ? Instantiate(textPrbDG, texts) : Instantiate(textPrbDGr, texts);
                break;
            case C_Enum.TypeText.Miss:
                text = (!isRight) ? Instantiate(textPrbMiss, texts) : Instantiate(textPrbMissr, texts);
                break;
            default:
                break;
        }
        if (text != null) text.GetComponent<Text>().text = str;
    }

    public void AddEffect(string rs_effect)
    {
        // Debug.Log("========================================================>Add Effect: " + id_effect);
        GameObject se = Instantiate(SoftEffectPrb, posSE);
        C_SoftEffect cse = se.GetComponent<C_SoftEffect>();
        cse.set(rs_effect);

        if (!softEffects.ContainsKey(rs_effect)) softEffects.Add(rs_effect, cse);
    }

    public void RemoveEffect(string rs_effect)
    {
        // Debug.Log("========================================================>Remove Effect: " + id_effect);
        if (softEffects.ContainsKey(rs_effect))
        {
            C_SoftEffect cse = softEffects[rs_effect];
            cse.DestroySE();
            softEffects.Remove(rs_effect);
        }
    }

    public void set(int team, int lv, string el, bool isCombat)
    {
        gameObject.SetActive(true);
        this.isRight = (team == 1);
        if (isRight) UIRight();
        lvTxt.text = lv.ToString();
        Sprite elSp = QuickFunction.getAssetImages("Sprites/ElementSmall/" + el);
        if (elSp != null) elImg.sprite = elSp;
        btnHeroSkill.SetActive(isCombat);
    }

    private void UIRight()
    {
        Vector3 scl = gameObject.transform.localScale;
        scl.x *= -1;
        gameObject.transform.localScale = scl;

        Hp.sprite = enemyHp;
    }
}
