using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
    [SerializeField]
    private InputField ipfTenNhanVat = null;
    [SerializeField]
    private Text txtNoti = null;

    public void VaoGame()
    {
        string tennhanvat = ipfTenNhanVat.text;

        Debug.Log("=======================================Vào Game: " + tennhanvat);

        txtNoti.text = "Vào Game thành công";

        ScenesManager.instance.ChangeScene("HomeGame");
    }
}
