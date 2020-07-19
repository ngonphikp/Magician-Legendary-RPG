using System;
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

        if (GameManager.instance.tick_milestonesDic.ContainsKey(this.milestone.id))
        {
            this.milestone.star = GameManager.instance.tick_milestonesDic[milestone.id].star;
            UpdateStar();

            this.GetComponent<Button>().interactable = true;
        }        
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
