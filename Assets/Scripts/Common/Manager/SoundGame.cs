using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGame : AudioObject
{
    public static SoundGame instance = null;

    [SerializeField]
    private AudioClip login = null;
    [SerializeField]
    private AudioClip ingame = null;
    [SerializeField]
    private AudioClip background = null;

    private void Awake()
    {
        MakeSingleInstance();
    }

    public void Login()
    {
        audioSource.clip = login;
        audioSource.Play();
    }

    public void InGame()
    {
        audioSource.clip = ingame;
        audioSource.Play();
    }

    public void BackGround()
    {
        audioSource.clip = background;
        audioSource.Play();
    }

    private void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
