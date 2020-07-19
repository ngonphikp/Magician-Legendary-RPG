using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class HomeGame : MonoBehaviour
{
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;

    public void OutGame()
    {
        LoginSendUtil.sendLogout();
    }

    private void Start()
    {
        FilterListHero();        
    }

    private async void FilterListHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                C_HeroAcEl heroAc = Instantiate(heroAcEl, listHeroAc).GetComponent<C_HeroAcEl>();
                heroAc.set(GameManager.instance.nhanVats[i]);
            }                
        }

        await Task.Yield();
    }
}
