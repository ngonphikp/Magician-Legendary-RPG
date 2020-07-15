using Sfs2X.Entities.Data;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
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

    [Header("Hiệu ứng cận chiến")]
    [SerializeField]
    private List<FxHero> Fx2 = new List<FxHero>();
    [SerializeField]
    private List<FxHero> Fx3 = new List<FxHero>();
    [SerializeField]
    private List<FxHero> Fx4 = new List<FxHero>();
    [SerializeField]
    private List<FxHero> Fx5 = new List<FxHero>();
    [SerializeField]
    private List<FxHero> Fx6 = new List<FxHero>();
    [SerializeField]
    private List<FxHero> Fx7 = new List<FxHero>();

    [Header("Khả năng chiến đấu")]
    public bool isCombat = false;

    public M_Character character = new M_Character();

    private List<M_Prop> propHPs = new List<M_Prop>(); // Mảng diễn thay đổi hp
    private List<M_Prop> propEPs = new List<M_Prop>(); // Mảng diễn thay đổi hp

    public bool isHit = true;
    public bool isDie = false;

    private I_Control ctl = null;

    private void Start()
    {
        if (UIHero != null)
        {
            UIHero.hp = character.current_hp * 1.0f / character.max_hp;
            UIHero.ep = character.current_ep * 1.0f / character.max_ep;
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
                EnableFx(Fx2);
                isAnim2 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(2);
            }
            if (isAnim3)
            {
                anim.SetTrigger("anim3");
                EnableFx(Fx3);
                isAnim3 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(3);
            }
            if (isAnim4)
            {
                anim.SetTrigger("anim4");
                EnableFx(Fx4);
                isAnim4 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(4);
            }
            if (isAnim5)
            {
                anim.SetTrigger("anim5");
                EnableFx(Fx5);
                isAnim5 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(5);
            }
            if (isAnim6)
            {
                anim.SetTrigger("anim6");
                EnableFx(Fx6);
                isAnim6 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(6);

                CheckAnimDie();
            }
            if (isAnim7)
            {
                anim.SetTrigger("anim7");
                EnableFx(Fx7);
                isAnim7 = false;

                if (isCombat && (PlayGame.instance.targets.Count > 0 || PlayGame.instance.targetBuffs.Count > 0)) ctl.Play(7);
            }
            await Task.Yield();
        }
    }

    private void EnableFx(List<FxHero> Fx)
    {
        for (int i = 0; i < Fx.Count; i++)
        {
            Fx[i].PlayAsync();
        }
    }

    public void Play(System.Object par)
    {
        int i = (int)par;
        Debug.Log("===========================" + this.character.actor_id + " Play:" + i);

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
            Debug.LogWarning("===========================" + this.character.actor_id + " Not Anim 1");
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

    public void PushChangeHp(M_Prop prop)
    {
        //Debug.Log("PushChangeHp");
        //prop.getDumpObject();
        propHPs.Add(prop);
    }

    public void PushChangeEp(M_Prop prop)
    {
        //Debug.Log("PushChangeEp");
        //prop.getDumpObject();
        propEPs.Add(prop);
    }

    public void ChangeHp()
    {
        Debug.Log("ChangeHp: " + this.character.actor_id);        
    }

    public void ChangeEp()
    {
        Debug.Log("ChangeEp: " + this.character.actor_id);        
    }

    public string getAnim()
    {
        if (!this.gameObject.activeSelf) return "null";
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        return m_CurrentClipInfo[0].clip.name;
    }

    public bool isAnim1()
    {
        if (!this.gameObject.activeSelf) return false;
        if (this.GetComponent<RectTransform>().localPosition != new Vector3() && PlayGame.instance) return false;
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo.Length < 1) return false;
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(m_ClipName);
        return (m_ClipName == "anim1");
    }

    public async void CheckAnimDie()
    {
        while (true)
        {
            AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);

            if (m_CurrentClipInfo.Length < 1) break;
            string m_ClipName = m_CurrentClipInfo[0].clip.name;
            // Debug.Log(m_ClipName);
            if (m_ClipName == "Die")
            {
                this.gameObject.SetActive(false);
                break;
            }
            await Task.Yield();
        }
    }
}


[System.Serializable]
class FxHero
{
    public GameObject fx;
    public float duration;
    public float delay;
    public bool isLoop;

    public FxHero()
    {

    }

    public FxHero(GameObject fx, float duration, float delay, bool isLoop)
    {
        this.fx = fx;
        this.duration = duration;
        this.delay = delay;
        this.isLoop = isLoop;
    }

    public async void PlayAsync()
    {
        //Debug.Log(fx.name + " Play");
        await Task.Delay(TimeSpan.FromSeconds(this.delay / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
        fx.SetActive(false);
        fx.SetActive(true);

        if (isLoop)
        {
            await Task.Delay(TimeSpan.FromSeconds(this.duration / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
            fx.SetActive(false);
        }
    }
}
