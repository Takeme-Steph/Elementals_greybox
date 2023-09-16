using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


//[RequireComponent(typeof(ParticleSystem))]

public class ToggleAttackParticles : MonoBehaviour
{
    public ParticleSystem AttackParticle;
        //public ParticleSystem igniteParticle;
        //public ParticleSystem extinguishParticle;
        //public GameObject pointLight;
    
    private void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnAttack()
    {
        AttackParticle.Play();
    }

}

