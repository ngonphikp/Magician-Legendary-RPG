using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_HeroAvEl : MonoBehaviour
{
    [SerializeField]
    private Text txtIdCfg = null;
    
    public bool isActive = false;

    public M_NhanVat nhanVat;

    public void set(M_NhanVat nhanVat)
    {
        this.nhanVat = nhanVat;

        txtIdCfg.text = nhanVat.id_nv + " / " + nhanVat.id_cfg;
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
