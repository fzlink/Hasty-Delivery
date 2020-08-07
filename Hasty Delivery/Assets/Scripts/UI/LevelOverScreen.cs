using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelOverScreen : MonoBehaviour
{

    public GameObject levelOverScreenObject;

    public TMP_Text levelOverTextComponent;
    public TMP_Text helperTextComponent;

    public Button continueButton;

    private bool isWon;

    void Start()
    {
        ChangeActive(false);
        continueButton.onClick.AddListener(() => ContinueLevel());
        CargoManager.instance.OnLevelWin += LevelWinScreen;
        CargoManager.instance.OnLevelFail += LevelFailScreen;
    }

    private void ChangeActive(bool active)
    {
        levelOverScreenObject.SetActive(active);
        continueButton.gameObject.SetActive(active);
    }

    private void ContinueLevel()
    {
        LevelData.instance.LoadLevel(isWon);
    }

    private void LevelFailScreen()
    {
        ChangeActive(true);
        levelOverTextComponent.text = "LEVEL FAILED";
        helperTextComponent.text = "TAP TO RESTART";
        isWon = false;
    }

    private void LevelWinScreen()
    {
        ChangeActive(true);
        levelOverTextComponent.text = "LEVEL COMPLETE";
        helperTextComponent.text = "TAP TO CONTINUE";
        isWon = true;
    }
}
