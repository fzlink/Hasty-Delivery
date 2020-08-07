using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public HorizontalLayoutGroup group;


    void Start()
    {
        group.gameObject.SetActive(true);
        //DetermineGroupSpacing();
        InputManager.instance.OnGameStarted += HideTutorial;
    }

    /*private void DetermineGroupSpacing()
    {

    }*/

    private void HideTutorial()
    {
        group.gameObject.SetActive(false);
    }

}
