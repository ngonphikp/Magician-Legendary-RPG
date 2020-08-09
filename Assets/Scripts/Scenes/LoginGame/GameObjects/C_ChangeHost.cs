using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ChangeHost : MonoBehaviour
{
    [SerializeField]
    private InputField ipfHost = null;

    private void Start()
    {
        ipfHost.text = GameManager.instance.host;
    }

    public void Change()
    {
        string host = ipfHost.text;
        GameManager.instance.host = host;

        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
        SmartFoxConnection.Init();
    }
}
