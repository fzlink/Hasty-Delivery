using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;

    public int levelNo;
    public int colorCount { get; set; }
    public int pointsNeeded { get; set; }
    public float roadSpeed { get; set; }
    public int maximumHealth;

    public const int initialColorCount = 3;
    public const int initialPointsNeeded = 3;
    public const int pointIncrementFrequency = 5;
    public const int initialRoadSpeed = 15;
    public const int maxRoadSpeed = 35;
    public const int roadSpeedIncrementFrequency = 8;
    public List<int> addColorThresholds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        LoadLevelNumber();
        DetermineColorCount();
        DeterminePointsNeeded();
        DetermineRoadSpeed();

    }

    public void LoadLevel(bool isWon)
    {
        if (isWon)
            SaveForNextLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveForNextLevel()
    {
        PlayerPrefs.SetInt("Level", levelNo + 1);
    }

    private void DetermineRoadSpeed()
    {
        roadSpeed = (levelNo / roadSpeedIncrementFrequency) * 5;
        roadSpeed += initialRoadSpeed ;
        if (roadSpeed > maxRoadSpeed)
            roadSpeed = maxRoadSpeed;
    }

    private void DeterminePointsNeeded()
    {
        pointsNeeded = levelNo / pointIncrementFrequency;
        pointsNeeded += initialPointsNeeded;
    }

    private void DetermineColorCount()
    {
        colorCount = initialColorCount;
        for (int i = 0; i < addColorThresholds.Count; i++)
        {
            if (levelNo > addColorThresholds[i])
                colorCount++;
            else
                break;
        }
    }
    private void LoadLevelNumber()
    {
        levelNo = LoadManager.instance.GetLevelIndex();
    }
}
