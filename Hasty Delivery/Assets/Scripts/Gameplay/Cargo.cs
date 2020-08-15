using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    
    public AddressColor addressColor { get; set; }
    public Color color { get; set; }
    public bool isDelivered;
    public bool isThrown;
    public GameObject sticker;

    public void PaintCargo(AddressColor addressColor, Color color)
    {
        this.addressColor = addressColor;
        this.color = color;
        GetComponent<Renderer>().material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isThrown) return;
        if(!isDelivered && other.transform.parent.tag == "House")  
        {
            if(other.transform.GetComponentInParent<House>().addressColor == this.addressColor)
            {
                other.gameObject.GetComponentInParent<House>().OnCargoDelivered();
                isDelivered = true;
                print(other.gameObject);
                CargoManager.instance.PointThrow();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isThrown) return;
        if (collision.gameObject.tag == "Ground")
        {
            if(transform.parent == null)
            {
                transform.parent = collision.transform;
                CargoManager.instance.CheckAndDestroy(this);
            }
        }
    }

}
