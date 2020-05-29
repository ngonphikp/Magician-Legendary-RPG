using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_Hero : MonoBehaviour, C_Control
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
    public M_Hero hero = new M_Hero();

    private List<M_Prop> propHPs = new List<M_Prop>(); // Mảng diễn thay đổi hp
    private List<M_Prop> propEPs = new List<M_Prop>(); // Mảng diễn thay đổi hp
    private List<M_Prop> propEffects = new List<M_Prop>(); // Mảng diễn thay đổi effect

    private Dictionary<string, M_Effect> effect_softs = new Dictionary<string, M_Effect>(); // Thư viện những effect soft đang tồn tại
    private M_Effect effect_hard = null; // effect hard đang tồn tại

    public bool isHit = true;
    public bool isDie = false;

    private void Start()
    {
        preUpdate();

        if (UIHero != null) UIHero.hp = character.current_hp * 1.0f / character.max_hp;
        if (UIHero != null) UIHero.ep = character.current_ep * 1.0f / character.max_ep;
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
            }
            if (isAnim3)
            {
                anim.SetTrigger("anim3");
                EnableFx(Fx3);
                isAnim3 = false;
            }
            if (isAnim4)
            {
                anim.SetTrigger("anim4");
                EnableFx(Fx4);
                isAnim4 = false;
            }
            if (isAnim5)
            {
                anim.SetTrigger("anim5");
                EnableFx(Fx5);
                isAnim5 = false;
            }
            if (isAnim6)
            {
                anim.SetTrigger("anim6");
                EnableFx(Fx6);
                isAnim6 = false;

                CheckAnimDie();
            }
            if (isAnim7)
            {
                anim.SetTrigger("anim7");
                EnableFx(Fx7);
                isAnim7 = false;
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

    public void Play(System.Object par, bool isMiss = false)
    {
        int i = (int)par;
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

        if (isMiss)
        {
            string str = "Miss";
            UIHero.CreateText(str, C_Enum.TypeText.Miss);
        }
    }

    public void Beaten()
    {
        if (isDie)
            Play(6);
        if (!isDie && isHit) Play(7);
        if (!isHit)
        {
            string str = "NÉ";
            UIHero.CreateText(str, C_Enum.TypeText.DG);
        }

        ChangeEp();
        ChangeHp();
        ChangeEffect();

        Debug.Log("Beaten");
        PlayGame.instance.Beaten--;
    }

    public void PushChangeEffect(M_Prop prop)
    {
        //Debug.Log("PushChangeEffect");
        //prop.getDumpObject();
        propEffects.Add(prop);
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
        while (propHPs.Count > 0)
        {
            M_Prop prop = propHPs[0];
            character.current_hp += prop.par7d0;
            if (UIHero != null)
            {
                UIHero.hp = character.current_hp * 1.0f / character.max_hp;
                if (prop.par7d0 >= 0)
                {
                    UIHero.CreateText(("+ " + prop.par7d0.ToString()), C_Enum.TypeText.HP1);
                }
                else
                {
                    UIHero.CreateText(("- " + Mathf.Abs(prop.par7d0).ToString()), C_Enum.TypeText.HP2);
                }
            }
            propHPs.RemoveAt(0);
        }
    }

    public void ChangeEp()
    {
        Debug.Log("ChangeEp: " + this.character.actor_id);
        while (propEPs.Count > 0)
        {
            M_Prop prop = propEPs[0];
            character.current_ep += prop.par8d0;
            if (UIHero != null)
            {
                UIHero.ep = character.current_ep * 1.0f / character.max_ep;
                if (prop.par8d0 >= 0)
                {
                    UIHero.CreateText(("+ " + prop.par8d0.ToString()), C_Enum.TypeText.EP1);
                }
                else
                {
                    UIHero.CreateText(("- " + Mathf.Abs(prop.par8d0).ToString()), C_Enum.TypeText.EP2);
                }
            }
            propEPs.RemoveAt(0);
        }
    }

    public void ChangeEffect()
    {
        Debug.Log("ChangeEffect: " + this.character.actor_id);
        while (propEffects.Count > 0)
        {
            M_Prop prop = propEffects[0];
            bool apply = prop.apply;
            M_Effect effect = C_Params.Effect[prop.id_effect];
            //prop.getDumpObject();
            switch (effect.effectCategory)
            {
                case C_Enum.EffectCategory.HARD:
                    // Nếu thêm effect
                    if (apply)
                    {
                        // Debug.Log("Thêm effect " + effect.id +" cho " + this.character.actor_id + " thành công");
                        if (effect_hard == null)
                        {
                            effect_hard = effect;
                            if (UIHero != null) UIHero.AddEffect(prop.rs_effect);
                        }
                        else
                        {
                            if (UIHero != null) UIHero.RemoveEffect(prop.rs_effect);
                            effect_hard = effect;
                            if (UIHero != null) UIHero.AddEffect(prop.rs_effect);
                        }
                    }
                    // Nếu gỡ effect
                    else
                    {
                        if (effect_hard == null)
                        {
                            Debug.Log("===============================================>" + this.character.actor_id + ": Remove Effect not exit");
                        }
                        else
                        {
                            // Debug.Log("Gỡ effect " + effect.id + " cho " + this.character.actor_id + " thành công");
                            if (UIHero != null) UIHero.RemoveEffect(prop.rs_effect);
                            effect_hard = null;
                        }
                    }
                    break;
                case C_Enum.EffectCategory.SOFT:
                    // Nếu thêm effect
                    if (apply)
                    {
                        // Debug.Log("Thêm effect " + effect.id + " cho " + this.character.actor_id + " thành công");
                        // Kiểm tra tồn tại trong thư viện effect soft
                        // Nếu chưa thì thêm vào
                        if (!effect_softs.ContainsKey(prop.rs_effect))
                        {
                            effect_softs.Add(prop.rs_effect, effect);
                            if (UIHero != null) UIHero.AddEffect(prop.rs_effect);
                        }
                        else
                        {
                            if (UIHero != null) UIHero.RemoveEffect(prop.rs_effect);
                            if (UIHero != null) UIHero.AddEffect(prop.rs_effect);
                        }
                    }
                    // Nếu gỡ effect
                    else
                    {
                        // Kiểm tra tồn tại trong thư viện effect soft
                        // Nếu có thì xóa 
                        if (effect_softs.ContainsKey(prop.rs_effect))
                        {
                            // Debug.Log("Gỡ effect " + effect.id + " cho " + this.character.actor_id + " thành công");
                            effect_softs.Remove(prop.rs_effect);
                            if (UIHero != null) UIHero.RemoveEffect(prop.rs_effect);
                        }
                        else
                        {
                            Debug.Log("===============================================>" + this.character.actor_id + ": Remove Effect not exit");
                        }
                    }
                    break;
                default:
                    Debug.Log("===============================================>" + this.character.actor_id + ": Default");
                    break;
            }
            propEffects.RemoveAt(0);
        }
    }

    public bool isAnim1()
    {
        if (!this.gameObject.activeSelf) return false;
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(m_ClipName);
        return (m_ClipName == "animation1");
    }

    public void RequestHeroSkill()
    {
        Debug.Log("Click Hero Skill");
        // Khi đầy nộ + có khả năng chiến đấu + chưa chết + team 0
        //if(character.current_ep >= character.max_ep && isCombat && !isDie && character.team == 0)
        if (isCombat && !isDie && character.team == 0)
        {
            PlayGame.instance.RequestHeroSkill(character.actor_id);

            // Trừ hết nộ
            character.current_ep = 0;
            if (UIHero != null) UIHero.ep = character.current_ep * 1.0f / character.max_ep;
        }
    }

    public async void CheckAnimDie()
    {
        while (true)
        {
            AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            string m_ClipName = m_CurrentClipInfo[0].clip.name;
            // Debug.Log(m_ClipName);
            if (m_ClipName == "Die")
            {
                // Debug.Log("DIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                await Task.Delay(TimeSpan.FromSeconds(0.2));
                this.gameObject.SetActive(false);
                break;
            }
            await Task.Yield();
        }
    }

    public void SetCharater(M_Character character, bool isCombat)
    {
        this.character = character;

        switch (character.type)
        {
            case C_Enum.CharacterType.Hero:
                character.element = GameManager.instance.herosDic[character.id].element;
                break;
            default:
                break;
        }

        this.isCombat = isCombat;
        UIHero.set(character.team, character.level, character.element, isCombat);
    }

    public void SetHero(M_Character character, M_Hero hero, bool isCombat)
    {
        this.character = character;
        this.hero = hero;
        this.isCombat = isCombat;
        UIHero.set(character.team, character.level, hero.element, isCombat);
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
        await Task.Delay(TimeSpan.FromSeconds(this.delay / GameManager.instance.myTimeScale));
        fx.SetActive(false);
        fx.SetActive(true);

        if (isLoop)
        {
            await Task.Delay(TimeSpan.FromSeconds(this.duration / GameManager.instance.myTimeScale));
            fx.SetActive(false);
        }
    }
}
