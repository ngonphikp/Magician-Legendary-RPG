using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Login : MonoBehaviour
{
    [SerializeField]
    private InputField ipfUsername = null;
    [SerializeField]
    private InputField ipfPassword = null;
    [SerializeField]
    private Text txtNoti = null;

    [SerializeField]
    private bool isSelection = true;

    private void OnEnable()
    {
        ipfUsername.text = "";
        ipfPassword.text = "";
        txtNoti.text = "";
    }

    public void Login()
    {
        string username = ipfUsername.text;
        string password = ipfPassword.text;

        Debug.Log("=======================================Login: " + username + " + " + password);

        txtNoti.text = "Đăng nhập thành công";

        // Nếu đã chọn pháp sư => HomeGame
        if(isSelection)
        {
            ScenesManager.instance.ChangeScene("HomeGame");
        }
        // Nếu chưa chọn => SelectionGame
        else
        {
            ScenesManager.instance.ChangeScene("SelectionGame");
        }
    }
}
