using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TavernGame : MonoBehaviour
{
    public static TavernGame instance = null;

    private Dictionary<C_Enum.CardType, C_TVCard> cardsDic = new Dictionary<C_Enum.CardType, C_TVCard>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [System.Obsolete]
    public void ReqCard(C_TVCard card)
    {
        Debug.Log("ReqCard: " + card.type);

        cardsDic.Add(card.type, card);
        
        if (GameManager.instance.test)
        {
            M_Character nhanvat = new M_Character();
            nhanvat.id_nv = GameManager.instance.nhanVats[GameManager.instance.nhanVats.Count - 1].id_nv + Random.RandomRange(99, 9999);
            nhanvat.id_cfg = "T100" + UnityEngine.Random.Range(2, 8);
            nhanvat.id_tk = GameManager.instance.taikhoan.id;

            nhanvat.idx = -1;

            nhanvat.lv = 1;
            nhanvat.UpdateById();
            nhanvat.Current_ep = nhanvat.max_ep = 100;
            nhanvat.Current_hp = nhanvat.max_hp = nhanvat.hp;
            nhanvat.UpdateLevel();
            nhanvat.type = C_Enum.CharacterType.Hero;

            RecCard(card.type, nhanvat);
        }
        else UserSendUtil.sendTavern(card.type);
    }

    public void RecCard(C_Enum.CardType type, M_Character nhanvat)
    {
        Debug.Log("RecCard: " + type);        

        cardsDic[type].Rec(nhanvat);
        cardsDic.Remove(type);
    }
}
