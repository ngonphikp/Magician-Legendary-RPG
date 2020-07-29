using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CharacterAcEl : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;

    private M_Character nhanVat;

    public void set(M_Character nhanVat)
    {
        this.nhanVat = nhanVat;

        GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Character/" + nhanVat.id_cfg);

        // Test
        if (heroAs == null) heroAs = QuickFunction.getAssetPref("Prefabs/Character/T1004");
        
        if(heroAs != null)
        {
            GameObject obj = Instantiate(heroAs, content);
            C_Character hero = obj.GetComponent<C_Character>();
            hero.Set(nhanVat);
        }
    }

    public void ClickHero()
    {
        Debug.Log("==================================ClickHero: " + nhanVat.id_nv + " / " + nhanVat.id_cfg);
    }
}
