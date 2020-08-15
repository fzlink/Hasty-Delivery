using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left = -1,
    Right = 1
}
public class DeliveryGuy : MonoBehaviour
{
    [SerializeField] private float throwSpeed;
    [SerializeField] private float getCargoTime;
    public SpriteRenderer characterSprite;
    private Cargo currentCargo;
    private Animator animator;
    public CircleBar circleBar;
    private bool isStopped;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        InputManager.instance.OnInput += OnInput;
        CargoManager.instance.OnLevelWin += Stop;
        CargoManager.instance.OnLevelFail += Stop;
        GetCargoItem();
    }

    private void Stop()
    {
        isStopped = true;
    }

    private void GetCargoItem()
    {
        if (!isStopped)
        {
            currentCargo = CargoManager.instance.GetCargoObject(transform);
            currentCargo.GetComponent<Rigidbody>().detectCollisions = false;
            circleBar.ShowCargoSprite(currentCargo.color);
            currentCargo.gameObject.SetActive(false);
        }
    }

    private void OnInput(InputDirection inputDirection)
    {
        if (isStopped) return;
        if(inputDirection != InputDirection.CenterInput)
        {
            ChangeSpriteDirection(inputDirection);
            PlayThrowAnimation();
            ThrowCargo(inputDirection);
        }
    }

    private void PlayThrowAnimation()
    {
        animator.SetTrigger("Throw");
    }

    public void PlayDyingAnimation()
    {
        animator.SetTrigger("Dying");
    }
    public void PlayJumpAnimation()
    {
        if(!DOTween.IsTweening(characterSprite.transform))
            characterSprite.transform.DOLocalMoveY(characterSprite.transform.localPosition.y + 5, 0.3f,true).SetLoops(2, LoopType.Yoyo);
        animator.SetTrigger("Jump");
    }
    private void ChangeSpriteDirection(InputDirection inputDirection)
    {
        SpriteRenderer SR = GetComponentInChildren<SpriteRenderer>();
        if (inputDirection == InputDirection.LeftInput)
            SR.flipX = true;
        if (inputDirection == InputDirection.RightInput)
            SR.flipX = false;
    }

    private void ThrowCargo(InputDirection inputDirection )
    {
        if(currentCargo != null)
        {
            currentCargo.gameObject.SetActive(true);
            Vector3 throwDirection = Vector3.up;
            if (inputDirection == InputDirection.LeftInput)
            {
                throwDirection.x = -1;
            }
            else if (inputDirection == InputDirection.RightInput)
            {
                throwDirection.x = 1;
            }
            currentCargo.transform.parent = null;
            Rigidbody cargoRB = currentCargo.GetComponent<Rigidbody>();
            cargoRB.detectCollisions = true;
            cargoRB.isKinematic = false;
            cargoRB.AddForce(throwDirection * throwSpeed);
            currentCargo.isThrown = true;
            currentCargo = null;
            StartCoroutine(DelayForGetCargo());
            SFXController.instance.PlayThrowSFX();
        }
    }

    private IEnumerator DelayForGetCargo()
    {
        //circleBar.gameObject.SetActive(true);
        circleBar.StartBar(getCargoTime);
        yield return new WaitForSeconds(getCargoTime);
        //circleBarObject.gameObject.SetActive(false);
        GetCargoItem();
    }

}
