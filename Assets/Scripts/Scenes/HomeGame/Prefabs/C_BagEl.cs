using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_BagEl : MonoBehaviour
{
    [SerializeField]
    private Text txt = null;

    private M_Character nhanVat;

    public void set(M_Character nhanVat)
    {
        this.nhanVat = nhanVat;

        txt.text = nhanVat.id_nv + " / " + nhanVat.id_cfg + " / " + nhanVat.lv + " / " + nhanVat.element;
    }

    public void Click()
    {
        Debug.Log("==================================Click Bag EL: " + nhanVat.id_nv);
    }
}
