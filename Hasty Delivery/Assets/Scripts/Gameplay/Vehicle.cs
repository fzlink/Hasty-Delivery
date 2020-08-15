using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    private float groundYPos;
    private bool onGround = true;

    public Transform cargoDecor;
    public DeliveryGuy deliveryGuy;

    public List<ParticleSystem> exhaustParticles;
    private float heightThreshold;
    private bool crashed;

    private void Start()
    {
        heightThreshold = GetComponent<BoxCollider>().bounds.max.y * 2;
        groundYPos = transform.position.y;
        InputManager.instance.OnInput += OnInput;
    }
    private void Update()
    {
        CheckOnGround();
        ClampPosition();
    }


    private void ClampPosition()
    {
        if (transform.position.y < groundYPos)
            transform.position = new Vector3(transform.position.x,groundYPos,transform.position.z);
    }

    private void CheckOnGround()
    {
        if (transform.position.y > groundYPos + 0.5f)
        {
            StopExhaustParticles();
            onGround = false;
        }
    }

    private void OnInput(InputDirection inputDirection)
    {
        if (onGround && inputDirection == InputDirection.CenterInput)
            Jump();
    }
    private void Jump()
    {
        StopExhaustParticles();
        onGround = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
        deliveryGuy.PlayJumpAnimation();
        if (!DOTween.IsTweening(cargoDecor))
            cargoDecor.DOLocalMoveY(cargoDecor.transform.localPosition.y + 5, 0.3f,true).SetLoops(2, LoopType.Yoyo);
        SFXController.instance.PlayJumpSFX();
    }

    private void StopExhaustParticles()
    {
        for (int i = 0; i < exhaustParticles.Count; i++)
        {
            if(exhaustParticles[i].isPlaying)
                exhaustParticles[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    private void PlayExhaustParticles()
    {
        for (int i = 0; i < exhaustParticles.Count; i++)
        {
            if (!exhaustParticles[i].isPlaying)
                exhaustParticles[i].Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            PlayExhaustParticles();
            onGround = true;
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            StopExhaustParticles();
            Crash();
        }
    }
    private void Crash()
    {
        if (!crashed)
        {
            crashed = true;
            GetComponent<Animator>().SetTrigger("Crash");
            #if UNITY_ANDROID || UNITY_IOS
                            Handheld.Vibrate();
            #endif
            deliveryGuy.PlayDyingAnimation();
            CargoManager.instance.LevelFailed();
            SFXController.instance.PlayCrashSFX();
        }

    }
}

