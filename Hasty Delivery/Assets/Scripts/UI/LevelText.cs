using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    private TMP_Text textComponent;


    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Start()
    {
        UpdateLevelText(LevelData.instance.levelNo);
    }

    private void UpdateLevelText(int levelNo)
    {
        textComponent.text = "LEVEL " + levelNo;
    }


}
