using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
    [Header("Đăng nhập")]
    [SerializeField]
    private InputField ipfTenNhanVat = null;
    [SerializeField]
    private Text txtNoti = null;

    [Header("Chọn Magician")]
    [SerializeField]
    private string[] idMagicians = new string[5];
    [SerializeField]
    private Transform posMagician = null;

    private int idxActive = 0;
    private C_Magician magician = null;


    private void Start()
    {
        LoadMagician(idMagicians[idxActive]);
    }

    private void LoadMagician(string id)
    {
        foreach (Transform child in posMagician)
        {
            Destroy(child.gameObject);
        }
        GameObject magicianAs = QuickFunction.getAssetPref("Prefabs/Magician/" + id);
        if (magicianAs != null)
        {
            GameObject magicianObj = Instantiate(magicianAs, posMagician);
            //heroObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
            magician = magicianObj.GetComponent<C_Magician>();
        }
    }


    public void VaoGame()
    {
        string tennhanvat = ipfTenNhanVat.text;

        Debug.Log("=======================================Vào Game: " + tennhanvat + " / " + idMagicians[idxActive]);

        txtNoti.text = "Vào Game thành công";

        ScenesManager.instance.ChangeScene("HomeGame");
    }

    public void ChangMagician(int idx)
    {
        if (idx == idxActive) return;
        idxActive = idx;
        LoadMagician(idMagicians[idxActive]);
    }

    public void SkillMagician(int anim)
    {
        magician.Play(anim);
    }
}
