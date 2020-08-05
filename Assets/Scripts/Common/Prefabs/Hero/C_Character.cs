using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class C_Character : MonoBehaviour
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
    private C_UICharacter UICharacter = null;

    [Header("Khả năng chiến đấu")]
    public bool isCombat = false;
    public bool isHit = true;

    [SerializeField]
    private I_Control ctl = null;
    public M_Character nhanvat = new M_Character();

    private List<M_Prop> propHPs = new List<M_Prop>(); // Mảng diễn thay đổi hp
    private List<M_Prop> propEPs = new List<M_Prop>(); // Mảng diễn thay đổi ep

    private void Start()
    {
        if (UICharacter != null)
        {
            UICharacter.hp = nhanvat.Current_hp * 1.0f / nhanvat.max_hp;
            UICharacter.ep = nhanvat.Current_ep * 1.0f / nhanvat.max_ep;
        }

        ctl = this.GetComponent<I_Control>();

        preUpdate();
    }

    private void Update()
    {
        if (anim != null)
        {
            anim.speed = ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
        }
    }

    private async void preUpdate()
    {
        while (true)
        {
            if (isAnim2)
            {
                anim.SetTrigger("anim2");
                isAnim2 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(2);
            }
            if (isAnim3)
            {
                anim.SetTrigger("anim3");
                isAnim3 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(3);
            }
            if (isAnim4)
            {
                anim.SetTrigger("anim4");
                isAnim4 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(4);
            }
            if (isAnim5)
            {
                anim.SetTrigger("anim5");
                isAnim5 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(5);
            }
            if (isAnim6)
            {
                anim.SetTrigger("anim6");
                isAnim6 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(6);

                AnimDie();
            }
            if (isAnim7)
            {
                anim.SetTrigger("anim7");
                isAnim7 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(7);
            }
            await Task.Yield();
        }
    }

    public void Set(M_Character nhanvat)
    {
        this.nhanvat = nhanvat;

        if (UICharacter != null) UICharacter.set(this);
    }

    public void Play(int i)
    {
        Debug.Log("=========================== " + this.nhanvat.id_nv + " Play:" + i);

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
            Debug.LogWarning("=========================== " + this.nhanvat.id_nv + " Not Anim 1");
        }
    }

    public void Beaten()
    {
        if (nhanvat.isDie)
        {
            Play(6);
        }
        if (!nhanvat.isDie && isHit) Play(7);

        FightingGame.instance.Beaten--;

        ChangeEp();
        ChangeHp();
    }

    public void ChangeHp()
    {
        //Debug.Log("ChangeHp: " + this.nhanvat.id_nv);
        while (propHPs.Count > 0)
        {
            M_Prop prop = propHPs[0];
            nhanvat.Current_hp += prop.hpChange;
            if (UICharacter != null)
            {
                UICharacter.hp = nhanvat.Current_hp * 1.0f / nhanvat.max_hp;

                if (prop.hpChange >= 0)
                {
                    UICharacter.CreateText(C_Enum.TypeText.HP1, "+ " + prop.hpChange.ToString());
                }
                else
                {
                    UICharacter.CreateText(C_Enum.TypeText.HP2, "- " + Mathf.Abs(prop.hpChange).ToString());
                }
            }
            propHPs.RemoveAt(0);
        }
    }

    public void ChangeEp()
    {
        //Debug.Log("ChangeEp: " + this.nhanvat.id_nv);
        while (propEPs.Count > 0)
        {
            M_Prop prop = propEPs[0];
            nhanvat.Current_ep += prop.epChange;
            if (UICharacter != null)
            {
                UICharacter.ep = nhanvat.Current_ep * 1.0f / nhanvat.max_ep;

                if (prop.epChange >= 0)
                {
                    UICharacter.CreateText(C_Enum.TypeText.EP1, "+ " + prop.epChange.ToString());
                }
                else
                {
                    UICharacter.CreateText(C_Enum.TypeText.EP2, "- " + Mathf.Abs(prop.epChange).ToString());
                }
            }
            propEPs.RemoveAt(0);
        }
    }

    public void PushChangeHp(M_Prop prop)
    {
        propHPs.Add(prop);
    }

    public void PushChangeEp(M_Prop prop)
    {
        propEPs.Add(prop);
    }

    public bool isAnim1()
    {
        if (this.GetComponent<RectTransform>().localPosition != new Vector3() && FightingGame.instance) return false;

        return (AnimatorExtensions.GetCurrentStateName(anim, 0) == "Base Layer." + "anim1" && AnimatorExtensions.GetNextStateName(anim, 0) == "");
    }

    private async void AnimDie()
    {
        // Làm mờ
        SpriteRenderer sp = anim.gameObject.GetComponent<SpriteRenderer>();

        float maxTime = 0.6f;
        float timeOp = 0.6f;
        float op = 1.0f;
        while (true)
        {
            if (op <= 0.0f || timeOp <= 0.0f) break;

            float delta = Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);

            timeOp -= delta;
            op = timeOp / maxTime;

            await Task.Delay(TimeSpan.FromSeconds(delta));

            sp.color = new Color(1.0f, 1.0f, 1.0f, op);
        }

        this.gameObject.SetActive(false);
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}