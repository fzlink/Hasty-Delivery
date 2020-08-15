using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public HorizontalLayoutGroup group;
    public TMP_Text helperText;

    void Start()
    {
        ShowTutorial();
        InputManager.instance.OnGameStarted += HideTutorial;
    }


    private void ShowTutorial()
    {
        group.gameObject.SetActive(true);
        helperText.gameObject.SetActive(true);
    }

    private void HideTutorial()
    {
        group.gameObject.SetActive(false);
        helperText.gameObject.SetActive(false);
    }

}
