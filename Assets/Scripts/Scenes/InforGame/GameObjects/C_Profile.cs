using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Profile : MonoBehaviour
{
    [SerializeField]
    private Image imgEl = null;
    [SerializeField]
    private Text txtName = null;

    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtHp = null;
    [SerializeField]
    private Text txtAtk = null;
    [SerializeField]
    private Text txtDef = null;

    [SerializeField]
    private Text txtHpUp = null;
    [SerializeField]
    private Text txtAtkUp = null;
    [SerializeField]
    private Text txtDefUp = null;

    public void set(M_Character nhanVat)
    {
        imgEl.sprite = Resources.Load<Sprite>("Sprites/Element/" + nhanVat.element);
        txtName.text = nhanVat.name + "";

        txtLv.text = nhanVat.lv + "";
        txtHp.text = nhanVat.hp + "";
        txtAtk.text = nhanVat.atk + "";
        txtDef.text = nhanVat.def + "";

        txtHpUp.text = " + " + Mathf.RoundToInt(nhanVat.hp * (C_Params.coeUpLv - 1));
        txtAtkUp.text = " + " + Mathf.RoundToInt(nhanVat.atk * (C_Params.coeUpLv - 1));
        txtDefUp.text = " + " + Mathf.RoundToInt(nhanVat.def * (C_Params.coeUpLv - 1));
    }
}
