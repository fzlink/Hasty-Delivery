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

    public event Action OnCrash;
    public DeliveryGuy deliveryGuy;


    private void Start()
    {
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
        onGround = false;
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            Crash();
        }
    }
    private void Crash()
    {
        GetComponent<Animator>().SetTrigger("Crash");
        OnCrash?.Invoke();
        deliveryGuy.PlayDyingAnimation();
        CargoManager.instance.LevelFailed();
    }
}

