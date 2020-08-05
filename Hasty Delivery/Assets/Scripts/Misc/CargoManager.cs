using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AddressColor 
{ 
    Blue = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Purple = 4,
    Orange = 5,
    Pink = 6,
    Turqouise = 7
}
public class CargoManager : MonoBehaviour
{
    public const int initialColorCount = 3;
    public int currentColorCount;
    public static CargoManager instance;
    public Cargo cargoObject;
    private List<AddressColor> addressColors;

    public List<int> addColorThresholds;

    [SerializeField] private float secondsBeforeDestroy;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if(addressColors == null)
            DetermineAddressColors();
    }

    private void DetermineAddressColors()
    {
        addressColors = new List<AddressColor>();
        currentColorCount = initialColorCount;
        int levelIndex = LoadManager.instance.GetLevelIndex();
        for (int i = 0; i < addColorThresholds.Count; i++)
        {
            if (levelIndex > addColorThresholds[i])
                currentColorCount++;
            else
                break;
        }

        Array values = Enum.GetValues(typeof(AddressColor));
        System.Random random = new System.Random();
        for (int i = 0; i < currentColorCount; i++)
        {
            AddressColor randomAddressColor = (AddressColor)values.GetValue(random.Next(values.Length));
            addressColors.Add(randomAddressColor);
        }

    }
    public AddressColor GetRandomAddressColor()
    {
        if (addressColors == null)
            DetermineAddressColors();
        return PickRandomColorFromAddressList();
    }

    private AddressColor PickRandomColorFromAddressList()
    {
        System.Random random = new System.Random();
        int colorIndex = random.Next(addressColors.Count);
        return addressColors[colorIndex];
    }

    public Cargo GetCargoObject(Transform parent)
    {
        Cargo instantiatedCargo = Instantiate(cargoObject, parent.position + new Vector3(0,0.5f,-0.5f), Quaternion.identity,parent);
        instantiatedCargo.addressColor = PickRandomColorFromAddressList();
        return instantiatedCargo;
    }

    public void DestroyCargo(Cargo cargo)
    {
        StartCoroutine(DestroyWithDelay(cargo.gameObject));
    }

    private IEnumerator DestroyWithDelay(GameObject gameObject)
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(gameObject);
    }

}
