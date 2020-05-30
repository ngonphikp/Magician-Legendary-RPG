using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float timeLife = 0.5f;
    private void Start()
    {
        StartCoroutine(DestroyByTime(timeLife));
    }

    IEnumerator DestroyByTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
