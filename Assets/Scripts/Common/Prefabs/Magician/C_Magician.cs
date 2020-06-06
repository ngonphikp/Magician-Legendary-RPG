using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_Magician : MonoBehaviour
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
        Debug.Log("Beaten");
    }

    public bool isAnim1()
    {
        if (!this.gameObject.activeSelf) return false;
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(m_ClipName);
        return (m_ClipName == "anim1");
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
}
