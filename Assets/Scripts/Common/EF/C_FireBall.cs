using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FireBall : MonoBehaviour
{
    [SerializeField]
    private GameObject beam1 = null;
    [SerializeField]
    private GameObject beam2 = null;

    private Transform target = null;
    private float speed = 5.0f;

    public void Inverted()
    {
        this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        beam1.transform.rotation = Quaternion.Inverse(beam1.transform.rotation);
        beam2.transform.rotation = Quaternion.Inverse(beam2.transform.rotation);
    }    

    public IEnumerator Move(GameObject targ, float time)
    {
        Debug.Log("Move");
        yield return new WaitForSeconds(0.5f);
        this.target = targ.GetComponent<RectTransform>().transform;
        speed = Mathf.Abs(target.position.x - transform.position.x) / time;

        yield return new WaitForSeconds(time - 0.5f);
        this.target = null;
        Destroy(gameObject);
    }

    void Update()
    {
        if(target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }        
    }

}
