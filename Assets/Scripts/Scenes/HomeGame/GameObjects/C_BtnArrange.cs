using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BtnArrange : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.instance.isAttack = false;

        ScenesManager.instance.ChangeScene("ArrangeGame");
    }
}
