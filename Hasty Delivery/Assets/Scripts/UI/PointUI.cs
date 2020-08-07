using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{

    public Slider slider;
    public TMP_Text pointTextComponent;

    // Start is called before the first frame update
    void Start()
    {
        SetMaximumPoints(LevelData.instance.pointsNeeded);
        CargoManager.instance.OnPointThrow += SetPointValue;
    }

    private void SetMaximumPoints(int maxPoints)
    {
        slider.maxValue = maxPoints;
        pointTextComponent.text = "0/" + maxPoints;
    }

    private void SetPointValue(int pointValue)
    {
        slider.value = pointValue;
        pointTextComponent.text = pointValue + "/" + slider.maxValue;
    }
}
