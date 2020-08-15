using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelObject;


    void Start()
    {
        pausePanelObject.SetActive(false);
    }

    public void Pause()
    {
        pausePanelObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanelObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }


}
