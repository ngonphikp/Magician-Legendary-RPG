using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Milestone : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteStars = null;

    private Image img;
    private M_Milestone milestone = new M_Milestone();

    private void Awake()
    {
        img = this.GetComponent<Image>();
    }

    public void set(M_Milestone milestone)
    {
        this.milestone = milestone;

        this.milestone.star = Random.Range(0, 4);
        UpdateStar();
    }

    public void Click()
    {
        Debug.Log("Click :" + this.milestone.id);

        GameManager.instance.isAttack = true;
        GameManager.instance.idxMilestone = this.milestone.id;

        ScenesManager.instance.ChangeScene("ArrangeGame");
    }

    private void UpdateStar()
    {
        if(this.milestone.star <= spriteStars.Length)
        {
            img.sprite = spriteStars[this.milestone.star];
        }
        else
        {
            Debug.LogWarning("Exits Sprite");
        }
    }
}
