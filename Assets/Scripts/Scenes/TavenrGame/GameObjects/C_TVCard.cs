using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_TVCard : MonoBehaviour
{
    public C_Enum.CardType type = C_Enum.CardType.CT;

    [SerializeField]
    private Button btnConfirm = null;

    public void OnClick()
    {
        Debug.Log("OnClick");
        btnConfirm.interactable = false;

        TavernGame.instance.ReqCard(this);
    }

    public void Rec()
    {
        btnConfirm.interactable = true;
    }

}
