using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_BagEl : MonoBehaviour
{
    [SerializeField]
    private Image imgAv = null;
    [SerializeField]
    private Image imgFr = null;
    [SerializeField]
    private Image imgBg = null;
    [SerializeField]
    private Text txtTen = null;
    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtEl = null;
    [SerializeField]
    private Text txtStar = null;

    [SerializeField]
    private GameObject active = null;

    private M_Character nhanVat;

    public void set(M_Character nhanVat)
    {
        this.nhanVat = nhanVat;

        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + nhanVat.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + nhanVat.star);
        imgAv.sprite = Resources.Load<Sprite>("Sprites/Avatar/" + nhanVat.id_cfg);
        
        txtTen.text = nhanVat.name + "";
        txtLv.text = nhanVat.lv + "";
        txtStar.text = nhanVat.star + "";
        txtEl.text = C_Params.Element[nhanVat.element];

        active.SetActive(nhanVat.idx != -1);
    }

    public void Click()
    {
        Debug.Log("==================================Click Bag EL: " + nhanVat.id_nv);
    }
}
