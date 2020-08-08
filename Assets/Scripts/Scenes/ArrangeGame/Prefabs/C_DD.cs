using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_DD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject Trigger = null;

    public M_Character nhanVat = new M_Character();

    private Canvas canvas;
    private C_DD other;
    private bool isActive = false;
    private bool isOnBeginDrag = false;
    private bool isDD = true;

    public void Init(M_Character nhanVat, Canvas canvas, bool isDD = true)
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
            GameObject nvAs = Resources.Load("Prefabs/Character/" + nhanVat.id_cfg, typeof(GameObject)) as GameObject;

            if (nvAs == null)
            {
                switch (nhanVat.type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = Resources.Load("Prefabs/Character/M1000", typeof(GameObject)) as GameObject;
                        break;
                    default:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                }
                
            }

            if (nvAs != null)
            {
                GameObject obj = Instantiate(nvAs, this.gameObject.transform);
                C_Character hero = obj.GetComponent<C_Character>();
                hero.Set(nhanVat);
            }
        }
    }

    public void ReLoad(M_Character nhanVat, bool isActive, Canvas canvas = null)
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

            M_Character nhanVat = this.nhanVat;
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

        this.ReLoad(new M_Character(), false);        
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
