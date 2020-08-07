using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_CharacterAvEl : MonoBehaviour
{
    [SerializeField]
    private Text txtIdCfg = null;
    
    public bool isActive = false;

    public M_Character nhanVat;

    public void set(M_Character nhanVat)
    {
        this.nhanVat = nhanVat;

        txtIdCfg.text = nhanVat.id_nv + " / " + nhanVat.id_cfg + " / " + nhanVat.lv + " / " + nhanVat.element;
    }

    public void ClickHero()
    {
        Debug.Log("ClickHero: " + nhanVat.id_nv + " => " + isActive);
        if (ArrangeGame.instance.countActive >= C_Params.maxActive)
        {
            Debug.LogWarning("Full Active");
            return;
        }

        ArrangeGame.instance.Active(this.nhanVat);

        if (!isActive)
        {
            Active();
        }
    }

    public void Active()
    {
        isActive = true;
        this.GetComponent<Button>().interactable = false;
    }

    public void UnActive()
    {
        isActive = false;
        this.GetComponent<Button>().interactable = true;
    }
}
