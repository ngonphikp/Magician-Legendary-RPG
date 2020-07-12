using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGame : MonoBehaviour
{
    public void OutGame()
    {
        LoginSendUtil.sendLogout();
    }
}
