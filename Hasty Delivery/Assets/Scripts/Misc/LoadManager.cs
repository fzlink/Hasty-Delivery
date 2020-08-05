using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public static LoadManager instance;

    private House houseObject;
    private Obstacle obstacleObject;
    private Vehicle vehicleObject;

    private int levelIndex = -1;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        LoadAssets();
        LoadStats();
    }

    private void LoadStats()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 1);
    }
    public int GetLevelIndex()
    {
        if(levelIndex == -1)
            levelIndex = PlayerPrefs.GetInt("Level", 1);
        return levelIndex;
    }

    private void LoadAssets()
    {
        string skin = PlayerPrefs.GetString("Skin", "Default");
        string vehicle = PlayerPrefs.GetString("Vehicle", "Default");

        houseObject = Resources.Load<House>("Skin/" + skin + "/House");
        obstacleObject = Resources.Load<Obstacle>("Skin/" + skin + "/Obstacle");
        vehicleObject = Resources.Load<Vehicle>("Vehicle/" + vehicle + "/Vehicle");
    }

    public House GetHouseObject()
    {
        return houseObject;
    }
    public Obstacle GetObstacleObject()
    {
        return obstacleObject;
    }
    public Vehicle GetVehicleObject()
    {
        return vehicleObject;
    }
}
