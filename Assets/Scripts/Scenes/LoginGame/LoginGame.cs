using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame : MonoBehaviour
{    
    public static LoginGame instance = null;

    [SerializeField]
    private AudioClip audioClip = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        SoundManager.instance.PlayLoop(audioClip);
    }

    public void RecLogin(M_TaiKhoan tk)
    {
        Debug.Log("====================RecLogin: " + tk.id);

        GameManager.instance.taikhoan = tk;

        if(!GameManager.instance.test) UserSendUtil.sendGetInfo(tk.id);
    }

    public void RecRegister(M_TaiKhoan tk)
    {
        Debug.Log("====================RecRegister: " + tk.id);

        GameManager.instance.taikhoan = tk;

        if (!GameManager.instance.test) UserSendUtil.sendGetInfo(tk.id);
    }

    public void RecInfo(List<M_Character> lstNhanVat, List<M_Milestone> tick_milestones)
    {
        Debug.Log("====================RecInfo");

        //lstNhanVat.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));
        //tick_milestones.ForEach(x => Debug.Log(x.id + " / " + x.star));

        GameManager.instance.tick_milestones = tick_milestones;
        GameManager.instance.UpdateTickMS();

        // Nếu đã Selection
        if (lstNhanVat.Count > 0)
        {
            GameManager.instance.nhanVats = lstNhanVat;
            ScenesManager.instance.ChangeScene("HomeGame");
        }
        // Nếu chưa Selection
        else
        {
            ScenesManager.instance.ChangeScene("SelectionGame");
        }

        SoundManager.instance.PlayLoop();
    }

    public void TestPlay()
    {
        GameManager.instance.TestPlay();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
