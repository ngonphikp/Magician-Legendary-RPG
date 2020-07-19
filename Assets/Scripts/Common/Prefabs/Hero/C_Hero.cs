using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_Hero : MonoBehaviour
{
    [Header("Điều khiển hành động")]
    [SerializeField]
    private Animator anim = null;

    public bool isAnim2 = false;
    public bool isAnim3 = false;
    public bool isAnim4 = false;
    public bool isAnim5 = false;
    public bool isAnim6 = false;
    public bool isAnim7 = false;

    [Header("Thành phần giao diện")]
    [SerializeField]
    private C_UIHero UIHero = null;

    [Header("Khả năng chiến đấu")]
    public bool isCombat = false;
    public bool isHit = true;
    public bool isDie = false;

    private I_Control ctl = null;
    private M_NhanVat nhanvat;

    private void Start()
    {
        if (UIHero != null)
        {
            UIHero.hp = nhanvat.current_hp * 1.0f / nhanvat.max_hp;
            UIHero.ep = nhanvat.current_ep * 1.0f / nhanvat.max_ep;
        }

        ctl = this.GetComponent<I_Control>();

        preUpdate();
    }

    private async void preUpdate()
    {
        while (true)
        {
            if (isAnim2)
            {
                anim.SetTrigger("anim2");
                isAnim2 = false;

                if (isCombat) ctl.Play(2);
            }
            if (isAnim3)
            {
                anim.SetTrigger("anim3");
                isAnim3 = false;

                if (isCombat) ctl.Play(3);
            }
            if (isAnim4)
            {
                anim.SetTrigger("anim4");
                isAnim4 = false;

                if (isCombat) ctl.Play(4);
            }
            if (isAnim5)
            {
                anim.SetTrigger("anim5");
                isAnim5 = false;

                if (isCombat) ctl.Play(5);
            }
            if (isAnim6)
            {
                anim.SetTrigger("anim6");
                isAnim6 = false;

                if (isCombat) ctl.Play(6);
            }
            if (isAnim7)
            {
                anim.SetTrigger("anim7");
                isAnim7 = false;

                if (isCombat) ctl.Play(7);
            }
            await Task.Yield();
        }
    }

    public void Play(System.Object par)
    {
        int i = (int)par;
        Debug.Log("===========================" + this.nhanvat.id_nv + " Play:" + i);

        if (isAnim1())
        {
            switch (i)
            {
                case 2:
                    isAnim2 = true;
                    break;
                case 3:
                    isAnim3 = true;
                    break;
                case 4:
                    isAnim4 = true;
                    break;
                case 5:
                    isAnim5 = true;
                    break;
                case 6:
                    isAnim6 = true;
                    break;
                case 7:
                    isAnim7 = true;
                    break;
            }
        }
        else
        {
            Debug.LogWarning("===========================" + this.nhanvat.id_nv + " Not Anim 1");
        }
    }

    public void Beaten()
    {
        if (isDie)
        {
            Play(6);
        }
        if (!isDie && isHit) Play(7);

        ChangeEp();
        ChangeHp();
    }

    public void ChangeHp()
    {
        Debug.Log("ChangeHp: " + this.nhanvat.id_nv);        
    }

    public void ChangeEp()
    {
        Debug.Log("ChangeEp: " + this.nhanvat.id_nv);        
    }

    public bool isAnim1()
    {
        if (!this.gameObject.activeSelf) return false;
        if (this.GetComponent<RectTransform>().localPosition != new Vector3()) return false;
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo.Length < 1) return false;
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(m_ClipName);
        return (m_ClipName == "anim1");
    }
}