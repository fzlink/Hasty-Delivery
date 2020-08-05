using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private AddressColor addressColor;
    
    private void OnEnable()
    {
        PaintHouses();
    }

    private void PaintHouses()
    {
        addressColor = CargoManager.instance.GetRandomAddressColor();
    }
}
