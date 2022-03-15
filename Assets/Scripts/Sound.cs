using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource playAudio;
    public AudioClip shootAudio;
    public AudioClip crashAudio;
    public AudioClip explosion1;
    public AudioClip explosion2;
    public AudioClip explosion3;
    public static Sound soundInstance;
    private void Awake()
    {
        playAudio = GetComponent<AudioSource>();
        soundInstance = this;
    }
    public void PlayAudio(AudioClip audioClip, float volum)
    {
        playAudio.PlayOneShot(audioClip, volum);
    }

}
