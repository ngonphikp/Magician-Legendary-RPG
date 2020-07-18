using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ArrangeGame : MonoBehaviour
{
    public static ArrangeGame instance = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private Transform listHeroAv = null;
    [SerializeField]
    private GameObject heroAvEl = null;

    [SerializeField]
    private C_CellAT[] teamL = new C_CellAT[9];
    [SerializeField]
    private C_CellAT[] teamR = new C_CellAT[9];

    private List<M_Hero> listHero = new List<M_Hero>();
    public int countActive = 0;

    public Dictionary<int, C_HeroAvEl> Objs = new Dictionary<int, C_HeroAvEl>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        LoadListHero();
    }

    private async void LoadListHero()
    {
        Objs.Clear();

        for (int i = 0; i < GameManager.instance.listHero.Count; i++)
        {
            C_HeroAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_HeroAvEl>();
            heroAv.setHero(GameManager.instance.listHero[i]);

            listHero.Add(GameManager.instance.listHero[i]);

            if (GameManager.instance.listHero[i].idx != -1)
            {
                if (countActive >= C_Params.maxActive) break;

                heroAv.Active();

                teamL[GameManager.instance.listHero[i].idx].setHero(GameManager.instance.listHero[i], canvas);

                countActive++;
            }

            Objs.Add(GameManager.instance.listHero[i].id_nv, heroAv);
        }

        Debug.Log("Count: " + Objs.Keys.Count);

        await Task.Yield();
    }

    public void Active(M_Hero hero)
    {        
        for(int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.hero.idx == -1)
            {
                hero.idx = i;
                teamL[i].setHero(hero, canvas);
                countActive++;

                return;
            }
        }
    }

    public void Fightting()
    {
        List<M_Hero> heros = new List<M_Hero>();

        // Update index hero in teamL
        for(int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.hero.idx != -1)
            {
                teamL[i].content.hero.idx = i;

                heros.Add(teamL[i].content.hero);
            }
        }

        // Update index hero in list
        foreach(C_HeroAvEl el in Objs.Values)
        {
            if (!el.isActive)
            {
                el.hero.idx = -1;

                heros.Add(el.hero);
            }
        }

        //heros.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.idx));

        UserSendUtil.sendArrange(heros);

        GameManager.instance.listHero = heros;
    }
}
