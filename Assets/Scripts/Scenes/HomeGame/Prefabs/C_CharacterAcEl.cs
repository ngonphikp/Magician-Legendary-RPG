using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CharacterAcEl : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;

    private int idx;

    public void set(int idx)
    {
        M_Character nhanVat = new M_Character(GameManager.instance.nhanVats[idx]);
        nhanVat.Current_ep = nhanVat.max_ep = 100;
        nhanVat.Current_hp = nhanVat.max_hp = nhanVat.hp;

        this.idx = idx;

        GameObject heroAs = Resources.Load("Prefabs/Character/" + nhanVat.id_cfg, typeof(GameObject)) as GameObject;

        // Test
        if (heroAs == null) heroAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject obj = Instantiate(heroAs, content);
            C_Character hero = obj.GetComponent<C_Character>();
            hero.Set(nhanVat);
        }
    }

    public void ClickHero()
    {
        GameManager.instance.idxCharacter = idx;
        ScenesManager.instance.ChangeScene("InforGame");
    }
}
