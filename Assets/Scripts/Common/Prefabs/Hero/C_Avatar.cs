using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Avatar : MonoBehaviour
{
    [SerializeField]
    private Image imgBg = null;
    [SerializeField]
    private Image imgFr = null;
    [SerializeField]
    private Image imgAv = null;
    [SerializeField]
    private Image imgEl = null;
    [SerializeField]
    private Text txtLv = null;

    public void set(M_Character nhanVat)
    {
        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + nhanVat.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + nhanVat.star);
        imgAv.sprite = Resources.Load<Sprite>("Sprites/Avatar/" + nhanVat.id_cfg);
        imgEl.sprite = Resources.Load<Sprite>("Sprites/Element/" + nhanVat.element);
        txtLv.text = nhanVat.lv + "";
    }
}
