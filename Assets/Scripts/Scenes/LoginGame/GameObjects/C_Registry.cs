using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Registry : MonoBehaviour
{
    [SerializeField]
    private InputField ipfUsername = null;
    [SerializeField]
    private InputField ipfPassword = null;
    [SerializeField]
    private Text txtNoti = null;

    private void OnEnable()
    {
        ipfUsername.text = "";
        ipfPassword.text = "";
        txtNoti.text = "";
    }

    public void Registry()
    {
        string username = ipfUsername.text;
        string password = ipfPassword.text;

        Debug.Log("====================Registry: " + username + " + " + password);

        LoginSendUtil.sendRegister(username, password);
    }
}
