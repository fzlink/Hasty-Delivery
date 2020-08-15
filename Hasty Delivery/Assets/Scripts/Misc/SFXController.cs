using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;
    public AudioClip jumpSFX;
    public AudioClip hitPointSFX;
    public AudioClip throwSFX;
    public AudioClip crashSFX;
    public AudioClip levelWinSFX;
    public AudioClip levelFailSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CargoManager.instance.OnLevelWin += PlayLevelWinSFX;
        CargoManager.instance.OnLevelFail += PlayLevelFailSFX;
        CargoManager.instance.OnPointThrow += PlayHitPointSFX;
    }

    private void PlayHitPointSFX(int pointNum)
    {
        audioSource.PlayOneShot(hitPointSFX,0.4f);
    }

    private void PlayLevelFailSFX()
    {
        audioSource.PlayOneShot(levelFailSFX);
    }

    private void PlayLevelWinSFX()
    {
        audioSource.PlayOneShot(levelWinSFX);
    }
    public void PlayThrowSFX()
    {
        audioSource.PlayOneShot(throwSFX);
    }
    public void PlayCrashSFX()
    {
        audioSource.PlayOneShot(crashSFX,0.4f);
    }

    internal void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX);
    }
}
