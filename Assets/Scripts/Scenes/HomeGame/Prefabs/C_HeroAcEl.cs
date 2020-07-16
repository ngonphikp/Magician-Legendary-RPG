using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_HeroAcEl : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;

    private M_Hero hero;

    public void setHero(M_Hero hero)
    {
        this.hero = hero;

        GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Hero/" + hero.id_cfg);

        // Test
        if (heroAs == null) heroAs = QuickFunction.getAssetPref("Prefabs/Hero/T1004");
        
        if(heroAs != null)
        {
            Instantiate(heroAs, content);
        }
    }

    public void ClickHero()
    {
        Debug.Log("==================================ClickHero: " + hero.id_nv + " / " + hero.id_cfg);
    }
}
