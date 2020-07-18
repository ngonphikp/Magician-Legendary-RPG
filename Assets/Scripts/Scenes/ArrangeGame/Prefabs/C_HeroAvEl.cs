using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_HeroAvEl : MonoBehaviour
{
    [SerializeField]
    private Text txtIdCfg = null;
    
    public bool isActive = false;

    public M_Hero hero;

    public void setHero(M_Hero hero)
    {
        this.hero = hero;

        txtIdCfg.text = hero.id_nv + " / " + hero.id_cfg;
    }

    public void ClickHero()
    {
        Debug.Log("ClickHero: " + hero.id_nv + " => " + isActive);
        if (ArrangeGame.instance.countActive >= C_Params.maxActive)
        {
            Debug.LogWarning("Full Active");
            return;
        }

        ArrangeGame.instance.Active(this.hero);

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
