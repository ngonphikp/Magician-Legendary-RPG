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

    public void ReqCard(C_TVCard card)
    {
        Debug.Log("ReqCard: " + card.type);

        cardsDic.Add(card.type, card);
        UserSendUtil.sendTavern(card.type);
    }

    public void RecCard(C_Enum.CardType type)
    {
        Debug.Log("RecCard: " + type);

        cardsDic[type].Rec();
        cardsDic.Remove(type);
    }
}
