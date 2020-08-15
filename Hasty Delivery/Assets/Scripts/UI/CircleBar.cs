using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBar : MonoBehaviour
{
    private bool canLoad;
    private float loadOffset;
    private float totalLoadTime;
    public SpriteRenderer boxSprite;

    public void StartBar(float totalLoadTime)
    {
        boxSprite.gameObject.SetActive(false);
        canLoad = true;
        this.totalLoadTime = totalLoadTime;
        loadOffset = 1;
        gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff", loadOffset);
    }

    public void ShowCargoSprite(Color color)
    {
        boxSprite.gameObject.SetActive(true);
        boxSprite.color = color;

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
            if(loadOffset <= 0.001f)
            {
                loadOffset = 0.001f;
                canLoad = false;
            }
		    gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff", loadOffset);
        }
	}
}
