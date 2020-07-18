using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_DD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject Trigger = null;

    public M_Hero hero = new M_Hero();

    private Canvas canvas;
    private C_DD other;
    private bool isActive = false;
    private bool isOnBeginDrag = false;

    public void Init(M_Hero hero, Canvas canvas)
    {
        this.isActive = true;
        this.hero = hero;
        this.canvas = canvas;

        createHero();
    }

    private void setTrigger(bool isActive)
    {
        Trigger.SetActive(isActive);
    }

    private void createHero()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        if (this.isActive)
        {
            GameObject heroAs = QuickFunction.getAssetPref("Prefabs/Hero/" + hero.id_cfg);

            if (heroAs == null) heroAs = QuickFunction.getAssetPref("Prefabs/Hero/T1004");

            if (heroAs != null)
            {
                Instantiate(heroAs, this.gameObject.transform);
            }
        }
    }

    public void ReLoad(M_Hero hero, bool isActive, Canvas canvas = null)
    {
        this.isActive = isActive;
        this.hero = hero;        

        if (this.canvas == null) this.canvas = canvas;

        createHero();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive) return;
        Debug.Log("OnBeginDrag: " + hero.id_nv);

        this.isOnBeginDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isActive) return;
        //Debug.Log("OnDrag: " + parent.hero.id_nv);
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isActive) return;
        Debug.Log("OnEndDrag: " + hero.id_nv);

        if (other != null)
        {
            Debug.Log("Other: " + other.hero.id_nv);

            M_Hero hero = this.hero;
            bool isActive = this.isActive;

            this.ReLoad(other.hero, other.isActive, this.canvas);
            other.ReLoad(hero, isActive, this.canvas);

            this.setTrigger(false);
            other.setTrigger(false);            
        }

        this.transform.localPosition = new Vector3();
        other = null;

        this.isOnBeginDrag = false;
    }

    public void OnClick()
    {
        if (!isActive || isOnBeginDrag) return;
        Debug.Log("OnClick: " + hero.id_nv);

        ArrangeGame.instance.Objs[hero.id_nv].UnActive();
        ArrangeGame.instance.countActive--;

        this.ReLoad(new M_Hero(), false);        
    }

    private C_DD oldOther;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DD") && other.gameObject.GetComponent<C_DD>() != null && this.isOnBeginDrag)
        {
            this.other = other.gameObject.GetComponent<C_DD>();

            if(oldOther != null) oldOther.setTrigger(false);
            this.other.setTrigger(true);

            oldOther = this.other;
        }
    }
}
