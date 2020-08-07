using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBar : MonoBehaviour
{
    private bool canLoad;
    private float loadOffset;
    private float totalLoadTime;

    public void StartBar(float totalLoadTime)
    {
		canLoad = true;
        this.totalLoadTime = totalLoadTime;
        loadOffset = 1;
        gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff", loadOffset);
    }

	void Update()
	{
        if (canLoad)
        {
            if (Game.gameIsOver)
            {
                transform.parent.gameObject.SetActive(false);
            }
            loadOffset -= Time.deltaTime / totalLoadTime;
            if(loadOffset <= 0.01f)
            {
                loadOffset = 0.01f;
                canLoad = false;
            }
		    gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff", loadOffset);
        }
	}
}
