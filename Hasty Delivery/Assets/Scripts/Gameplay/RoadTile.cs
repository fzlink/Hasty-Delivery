using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadTile : MonoBehaviour
{
    public bool houseOnLeft;
    public bool houseOnRight;
    public bool obstacleOnMiddle;

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform middlePoint;

    private void Start()
    {
        if(houseOnLeft || houseOnRight)
        {
            House house = LoadManager.instance.GetHouseObject();
            if (houseOnLeft)
            {
                Instantiate(house, leftPoint);
            }
            if (houseOnRight)
            {
                Instantiate(house, rightPoint);
            }
        }
        if (obstacleOnMiddle)
        {
            Obstacle obstacle = LoadManager.instance.GetObstacleObject();
            Instantiate(obstacle, middlePoint);
        }
    }

}
