using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public bool isMute = false;
    public float volume = 1;

    [SerializeField]
    private AudioClip acBackground = null;
    private AudioSource audioSource = null;

    private AudioSource audioSource2 = null;

    private void Awake()
    {
        MakeSingleInstance();

        audioSource = GetComponent<AudioSource>();
        LoadSystem();
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

    public void PlayOneShotAs2(AudioClip audioClip, float speed = 1)
    {
        if (audioSource2 == null)
        {
            audioSource2 = this.gameObject.AddComponent<AudioSource>();
            audioSource2.volume = volume;
            audioSource2.mute = isMute;            
        }

        audioSource2.pitch = speed;
        audioSource2.PlayOneShot(audioClip, volume);
    }

    public void PlayOneShot(AudioClip audioClip = null)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public async void PlayOneShotAs(AudioClip audioClip = null)
    {
        AudioSource audioSource2 = this.gameObject.AddComponent<AudioSource>();
        audioSource2.mute = isMute;
        audioSource2.volume = volume;
        audioSource2.PlayOneShot(audioClip, volume);

        audioSource.volume = 0;
        await Task.Delay(TimeSpan.FromSeconds(audioClip.length));

        audioSource.volume = volume;
        if(audioSource2 != null) Destroy(audioSource2);
    }

    public void PlayLoop(AudioClip audioClip = null)
    {
        audioSource.clip = (audioClip != null) ? audioClip : acBackground;

        audioSource.Play();
    }

    public void LoadSystem()
    {
        audioSource.mute = isMute;
        audioSource.volume = volume;

        LoadAllElement();
    }

    public async void LoadAllElement()
    {
        GameObject[] elements;
        elements = GameObject.FindGameObjectsWithTag("SoundElement");

        foreach (GameObject el in elements)
        {
            AudioSource asEl = el.GetComponent<AudioSource>();

            if(asEl != null)
            {
                asEl.mute = isMute;
                asEl.volume *= volume;
            }            
        }

        await Task.Yield();
    }

    public void ChangeMute()
    {
        isMute = !isMute;
        audioSource.mute = isMute;
        if (audioSource2 != null) audioSource2.mute = isMute;        
    }

    public void ChangeVolume(float value)
    {
        volume = value;
        audioSource.volume = volume;
        if(audioSource2 != null) audioSource2.volume = volume;
    }
}
