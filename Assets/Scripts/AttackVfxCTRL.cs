using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


//[RequireComponent(typeof(ParticleSystem))]

public class AttackVfxCTRL: MonoBehaviour
{
    public string actionType = "attack";
    public int hitPoints = 5;


    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {

    }

}

