using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-- Audio Source --")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFX;

    [Header("-- Audio Clip --")]
    public AudioClip background;
    public AudioClip Burb;
    public AudioClip fire;
    private void Start()
    {
        MusicSource.clip = background;
        MusicSource.Play();

        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

}
