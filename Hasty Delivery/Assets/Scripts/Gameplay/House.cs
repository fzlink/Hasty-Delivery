using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public AddressColor addressColor { get; set; }
    public Transform mainConcreteTransform;
    public ParticleSystem zoneParticle;
    public ParticleSystem confettiParticle;
    public ParticleSystem explosionParticle;

    private void OnEnable()
    {
        PaintHouse();
    }

    private void PaintHouse()
    {
        AddressColor AC = CargoManager.instance.GetRandomAddressColor();
        addressColor = AC;
        for (int i = 0; i < mainConcreteTransform.childCount; i++)
        {
            mainConcreteTransform.GetChild(i).GetComponent<Renderer>().material.color = CargoManager.instance.realColors[(int)AC];
        }
    }

    private void Update()
    {
        if(transform.position.z < 25)
        {
            if(!zoneParticle.isPlaying)
                zoneParticle.Play();
        }
    }

    public void OnCargoDelivered()
    {
        if (!confettiParticle.isPlaying)
            confettiParticle.Play();
        if (!explosionParticle.isPlaying)
            explosionParticle.Play();
        
    }
}
