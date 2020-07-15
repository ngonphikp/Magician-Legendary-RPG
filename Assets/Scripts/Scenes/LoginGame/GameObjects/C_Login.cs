﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Login : MonoBehaviour
{
    public static C_Login instance = null;

    [SerializeField]
    private InputField ipfUsername = null;
    [SerializeField]
    private InputField ipfPassword = null;
    [SerializeField]
    private Text txtNoti = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

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

        Debug.Log("=======================Login: " + username + " + " + password);

        LoginSendUtil.sendLogin(username, password);
    }

    public void setNoti(string str)
    {
        txtNoti.text = str;
    }
}
