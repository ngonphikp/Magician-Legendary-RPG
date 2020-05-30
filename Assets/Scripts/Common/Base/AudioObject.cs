using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Giúp các lớp kế thừa dễ dàng truy cập vào thành phần AudioSource
[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
