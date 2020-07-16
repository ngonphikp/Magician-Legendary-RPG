using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ArrangeGame : MonoBehaviour
{
    [SerializeField]
    private Transform listHeroAv = null;
    [SerializeField]
    private GameObject heroAvEl = null;

    [SerializeField]
    private C_CellAT[] teamL = new C_CellAT[9];
    [SerializeField]
    private C_CellAT[] teamR = new C_CellAT[9];

    private List<M_Hero> listHero = new List<M_Hero>();

    private void Start()
    {
        LoadListHero();
    }

    private async void LoadListHero()
    {
        for (int i = 0; i < GameManager.instance.listHero.Count; i++)
        {
            C_HeroAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_HeroAvEl>();
            heroAv.setHero(GameManager.instance.listHero[i]);

            listHero.Add(GameManager.instance.listHero[i]);

            if (GameManager.instance.listHero[i].idx != -1)
            {
                heroAv.Active();

                teamL[GameManager.instance.listHero[i].idx].setHero(GameManager.instance.listHero[i]);
            }
        }

        await Task.Yield();
    }
}
