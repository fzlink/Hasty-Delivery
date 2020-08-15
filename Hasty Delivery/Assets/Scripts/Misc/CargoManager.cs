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
    public event Action OnMissThrow;
    public event Action<int> OnPointThrow;
    public event Action OnLevelWin;
    public event Action OnLevelFail;


    public int currentColorCount;
    public static CargoManager instance;
    public Cargo cargoObject;

    private List<AddressColor> addressColors;
    public List<Color> realColors;

    private int currentPoints;
    private int currentHealth;
    System.Random random = new System.Random();
    [SerializeField] private float secondsBeforeDestroy;
    [SerializeField] private float secondsBeforeSuccess;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentHealth = LevelData.instance.maximumHealth;
        if(addressColors == null)
            DetermineAddressColors();
    }

    private void DetermineAddressColors()
    {
        addressColors = new List<AddressColor>();

        Array values = Enum.GetValues(typeof(AddressColor));
        int ind = 0;
        while(ind < LevelData.instance.colorCount)
        {
            AddressColor randomAddressColor = (AddressColor)values.GetValue(random.Next(values.Length));
            if (!addressColors.Contains(randomAddressColor))
            {
                addressColors.Add(randomAddressColor);
                ind++;
            }
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
        int colorIndex = random.Next(addressColors.Count);
        return addressColors[colorIndex];
    }

    public Cargo GetCargoObject(Transform parent)
    {
        Cargo instantiatedCargo = Instantiate(cargoObject, parent.position + new Vector3(0,0.5f,-0.5f), Quaternion.identity,parent);
        AddressColor addressColor = PickRandomColorFromAddressList();
        instantiatedCargo.PaintCargo(addressColor, realColors[(int)addressColor]);
        return instantiatedCargo;
    }

    public void CheckAndDestroy(Cargo cargo)
    {
        StartCoroutine(WaitForSuccess(cargo));
    }

    private IEnumerator WaitForSuccess(Cargo cargo)
    {
        yield return new WaitForSeconds(secondsBeforeSuccess);
        if (!cargo.isDelivered)
        {
            MissThrow();
        }
        StartCoroutine(DestroyWithDelay(cargo.gameObject));
    }

    private IEnumerator DestroyWithDelay(GameObject gameObject)
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(gameObject);
    }

    public void PointThrow()
    {
        currentPoints++;
        OnPointThrow?.Invoke(currentPoints);
        if(currentPoints >= LevelData.instance.pointsNeeded)
        {
            OnLevelWin?.Invoke();
        }
        print("Point Throw");
    }

    public void MissThrow()
    {
        currentHealth--;
        OnMissThrow?.Invoke();
        if(currentHealth <= 0)
        {
            //LevelFailed();
        }
        print("Miss Throw");
    }

    public void LevelFailed()
    {
        OnLevelFail?.Invoke();
    }
}
