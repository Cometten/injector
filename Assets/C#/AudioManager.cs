using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------AudioSource-------")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------AudioClip-------")]
    public AudioClip background;
    public AudioClip point;
    public AudioClip card;
    public AudioClip click;
    public AudioClip gameover;
    public AudioClip mute;
    public AudioClip wrong;

    private void Start()
    {
        audioSource.clip = background;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
