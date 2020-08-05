using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public enum Direction
{
    Left = -1,
    Right = 1
}
public class DeliveryGuy : MonoBehaviour
{
    [SerializeField] private float throwSpeed;
    private Cargo currentCargo;

    private void Start()
    {
        InputManager.instance.OnInput += OnInput;
        GetCargoItem();
    }


    private void GetCargoItem()
    {
        currentCargo = CargoManager.instance.GetCargoObject(transform);
    }

    private void OnInput(InputDirection inputDirection)
    {
        if(inputDirection != InputDirection.CenterInput)
        {
            ChangeSpriteDirection(inputDirection);
            PlayThrowAnimation();
            ThrowCargo(inputDirection);
        }
    }

    private void PlayThrowAnimation()
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Throw");
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
            cargoRB.isKinematic = false;
            cargoRB.AddForce(throwDirection * throwSpeed);
            currentCargo = null;

        }

    }
}
