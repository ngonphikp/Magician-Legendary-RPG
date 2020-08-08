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

    private int idx = 0;

    public void set(int idx)
    {
        this.idx = idx;
        M_Character nhanVat = new M_Character(GameManager.instance.nhanVats[idx]);

        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + nhanVat.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + nhanVat.star);

        Sprite sprite = Resources.Load<Sprite>("Sprites/Avatar/" + nhanVat.id_cfg);

        if (sprite != null) imgAv.sprite = sprite;
        
        txtTen.text = nhanVat.name + "";
        txtLv.text = nhanVat.lv + "";
        txtStar.text = nhanVat.star + "";
        txtEl.text = C_Params.Element[nhanVat.element];

        active.SetActive(nhanVat.idx != -1);
    }

    public void Click()
    {
        GameManager.instance.idxCharacter = idx;
        ScenesManager.instance.ChangeScene("InforGame");
    }
}
