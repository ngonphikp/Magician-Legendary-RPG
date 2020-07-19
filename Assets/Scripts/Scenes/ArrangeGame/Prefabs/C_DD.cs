using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_DD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject Trigger = null;

    public M_NhanVat nhanVat = new M_NhanVat();

    private Canvas canvas;
    private C_DD other;
    private bool isActive = false;
    private bool isOnBeginDrag = false;
    private bool isDD = true;

    public void Init(M_NhanVat nhanVat, Canvas canvas, bool isDD = true)
    {
        this.isActive = true;
        this.nhanVat = nhanVat;
        this.canvas = canvas;
        this.isDD = isDD;

        createNhanVat();
    }

    private void setTrigger(bool isActive)
    {
        Trigger.SetActive(isActive);
    }

    private void createNhanVat()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        if (this.isActive)
        {
            GameObject nvAs = QuickFunction.getAssetPref("Prefabs/Hero/" + nhanVat.id_cfg);

            if (nvAs == null)
            {
                switch (nhanVat.type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Hero/T1004");
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Hero/M1000");
                        break;
                    default:
                        nvAs = QuickFunction.getAssetPref("Prefabs/Hero/T1004");
                        break;
                }
                
            }

            if (nvAs != null)
            {
                Instantiate(nvAs, this.gameObject.transform);
            }
        }
    }

    public void ReLoad(M_NhanVat nhanVat, bool isActive, Canvas canvas = null)
    {
        this.isActive = isActive;
        this.nhanVat = nhanVat;        

        if (this.canvas == null) this.canvas = canvas;

        createNhanVat();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnBeginDrag: " + nhanVat.id_nv);

        this.isOnBeginDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        //Debug.Log("OnDrag: " + parent.nhanVat.id_nv);
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnEndDrag: " + nhanVat.id_nv);

        if (other != null)
        {
            Debug.Log("Other: " + other.nhanVat.id_nv);

            M_NhanVat nhanVat = this.nhanVat;
            bool isActive = this.isActive;

            this.ReLoad(other.nhanVat, other.isActive, this.canvas);
            other.ReLoad(nhanVat, isActive, this.canvas);

            this.setTrigger(false);
            other.setTrigger(false);            
        }

        this.transform.localPosition = new Vector3();
        other = null;

        this.isOnBeginDrag = false;
    }

    public void OnClick()
    {
        if (!isActive || isOnBeginDrag || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnClick: " + nhanVat.id_nv);

        ArrangeGame.instance.Objs[nhanVat.id_nv].UnActive();
        ArrangeGame.instance.countActive--;

        this.ReLoad(new M_NhanVat(), false);        
    }

    private C_DD oldOther;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DD") && other.gameObject.GetComponent<C_DD>() != null && this.isOnBeginDrag && ArrangeGame.instance && isDD)
        {
            this.other = other.gameObject.GetComponent<C_DD>();

            if(oldOther != null) oldOther.setTrigger(false);
            this.other.setTrigger(true);

            oldOther = this.other;
        }
    }
}
