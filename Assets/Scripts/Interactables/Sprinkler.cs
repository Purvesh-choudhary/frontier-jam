using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour, IInteractable
{

    [SerializeField] ParticleSystem sprinkleParticle;
    [SerializeField] bool isSprinkleOn = false; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        isSprinkleOn = !isSprinkleOn;
        if (!sprinkleParticle.isPlaying)
        {
            sprinkleParticle.Play();
        }
        else
        {
            sprinkleParticle.Stop();
        }
    }
}
