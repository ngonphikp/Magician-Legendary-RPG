using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_TVCard : MonoBehaviour
{
    public C_Enum.CardType type = C_Enum.CardType.CT;

    [SerializeField]
    private Button btnConfirm = null;

    [SerializeField]
    private Transform content = null;

    public void OnClick()
    {
        Debug.Log("OnClick");
        btnConfirm.interactable = false;

        TavernGame.instance.ReqCard(this);
    }

    public void Rec(M_Character nhanvat)
    {
        btnConfirm.interactable = true;

        Debug.Log("Nhan vat moi: " + nhanvat.id_nv + " / " + nhanvat.id_cfg);

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        GameObject nvAs = QuickFunction.getAssetPref("Prefabs/Character/" + nhanvat.id_cfg);

        if (nvAs == null)
        {
            nvAs = QuickFunction.getAssetPref("Prefabs/Character/T1004");
        }

        if (nvAs != null)
        {
            GameObject obj = Instantiate(nvAs, content);
            C_Character character = obj.GetComponent<C_Character>();
            character.Set(nhanvat);
        }

        GameManager.instance.nhanVats.Add(nhanvat);
    }

}
