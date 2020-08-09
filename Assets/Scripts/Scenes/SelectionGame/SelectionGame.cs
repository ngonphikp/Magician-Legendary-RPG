using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
    public static SelectionGame instance = null;

    [Header("Vào Game")]
    [SerializeField]
    private InputField ipfTenNhanVat = null;
    [SerializeField]
    private Text txtNoti = null;

    [Header("Chọn Magician")]
    [SerializeField]
    private string[] idHeros = new string[5];
    [SerializeField]
    private Transform posMagician = null;
    [SerializeField]
    private Image[] imgSkills = null;

    private int idxActive = 0;
    private C_Character hero = null;
    private string tenNhanVat = "";

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    private void Start()
    {
        LoadHero(idHeros[idxActive]);
    }

    private void LoadHero(string id)
    {
        foreach (Transform child in posMagician)
        {
            Destroy(child.gameObject);
        }
        GameObject heroAs = Resources.Load("Prefabs/Character/" + id, typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject heroObj = Instantiate(heroAs, posMagician);
            hero = heroObj.GetComponent<C_Character>();
            M_Character nhanVat = new M_Character();

            nhanVat.Current_ep = nhanVat.max_ep = 100;
            nhanVat.Current_hp = nhanVat.max_hp = nhanVat.hp;
            nhanVat.id_cfg = id;
            nhanVat.UpdateById();
            nhanVat.lv = 1;
            hero.Set(nhanVat);
        }

        for(int i = 0; i < GameManager.instance.herosDic[id].skills.Count; i++)
        {
            imgSkills[i].sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[id].skills[i]);
        }
    }


    public void VaoGame()
    {
        tenNhanVat = ipfTenNhanVat.text;

        Debug.Log("====================Vào Game: " + tenNhanVat + " / " + idHeros[idxActive]);

        txtNoti.text = "Vào Game thành công";

        if (!GameManager.instance.test) UserSendUtil.sendSelection(tenNhanVat, idHeros[idxActive]);
    }

    public void RecSelection(List<M_Character> lstNhanVat)
    {
        Debug.Log("====================RecSelection");

        //lstNhanVat.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));

        if (lstNhanVat.Count > 0)
        {
            GameManager.instance.nhanVats = lstNhanVat;

            GameManager.instance.taikhoan.name = tenNhanVat;

            ScenesManager.instance.ChangeScene("HomeGame");
        }
    }

    public void ChangeHero(int idx)
    {
        if (idx == idxActive) return;
        idxActive = idx;
        LoadHero(idHeros[idxActive]);
    }

    public void SkillHero(int anim)
    {
        hero.Play(anim);
    }
}
