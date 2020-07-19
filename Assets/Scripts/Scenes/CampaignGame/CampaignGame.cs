using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignGame : MonoBehaviour
{
    [SerializeField]
    private C_Milestone[] milestones = null;

    private void Start()
    {
        LoadMilestones();
    }

    private void LoadMilestones()
    {
        for(int i = 0; i < milestones.Length; i++)
        {
            milestones[i].set(GameManager.instance.milestones[i]);
        }
    }
}
