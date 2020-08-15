using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public ParticleSystem winParticle;
    void Start()
    {
        CargoManager.instance.OnPointThrow += ShakeCamera;
        CargoManager.instance.OnLevelWin += PlayWinParticles;
    }

    private void PlayWinParticles()
    {
        winParticle.Play();
    }
    private void ShakeCamera(int obj)
    {
        transform.DOShakePosition(0.2f);
    }


}
