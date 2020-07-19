using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Milestone : MonoBehaviour
{
    private int star = 0;

    [SerializeField]
    private Sprite[] spriteStars = null;

    private Image img;

    private void Awake()
    {
        img = this.GetComponent<Image>();
    }

    public void Click()
    {
        Debug.Log("Click :" + this.gameObject.name);

        GameManager.instance.isAttack = true;

        ScenesManager.instance.ChangeScene("ArrangeGame");
    }

    public void SetStar(int star)
    {
        if (this.star == star) return;
        this.star = star;
        if(star <= spriteStars.Length)
        {
            img.sprite = spriteStars[star];
        }
        else
        {
            Debug.LogWarning("Exits Sprite");
        }
    }
}
