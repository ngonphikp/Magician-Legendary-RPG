using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeGame : MonoBehaviour
{
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;
    [SerializeField]
    private Transform bagHero = null;
    [SerializeField]
    private GameObject bagEl = null;

    public void OutGame()
    {       
        if (GameManager.instance.test)
        {
            SceneManager.LoadScene("LoginGame");
            GameManager.instance.test = false;
        }
        else LoginSendUtil.sendLogout();
    }

    private void Start()
    {
        FilterListHero();
        LoadBagHero();
    }

    private async void FilterListHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                C_CharacterAcEl heroAc = Instantiate(heroAcEl, listHeroAc).GetComponent<C_CharacterAcEl>();               
                heroAc.set(i);
            }                
        }

        await Task.Yield();
    }

    private async void LoadBagHero()
    {
        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            C_BagEl heroBag = Instantiate(bagEl, bagHero).GetComponent<C_BagEl>();
            heroBag.set(i);
        }

        await Task.Yield();
    }
}
