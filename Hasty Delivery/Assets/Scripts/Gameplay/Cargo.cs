using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public AddressColor addressColor { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if(transform.parent == null)
            {
                transform.parent = collision.transform;
                CargoManager.instance.DestroyCargo(this);
            }
        }
    }

}
