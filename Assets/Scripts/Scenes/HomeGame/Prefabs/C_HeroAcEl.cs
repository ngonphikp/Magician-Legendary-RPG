using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_HeroAcEl : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;

    private M_NhanVat nhanVat;

    public void set(M_NhanVat nhanVat)
    {
        this.nhanVat = nhanVat;

        GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Hero/" + nhanVat.id_cfg);

        // Test
        if (heroAs == null) heroAs = QuickFunction.getAssetPref("Prefabs/Hero/T1004");
        
        if(heroAs != null)
        {
            Instantiate(heroAs, content);
        }
    }

    public void ClickHero()
    {
        Debug.Log("==================================ClickHero: " + nhanVat.id_nv + " / " + nhanVat.id_cfg);
    }
}
