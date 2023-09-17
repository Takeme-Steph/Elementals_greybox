using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


//[RequireComponent(typeof(ParticleSystem))]

public class AttackActions : MonoBehaviour
{
    //store the vfx for each attak type
    public ParticleSystem BasicAttack;
        //public ParticleSystem igniteParticle;
        //public ParticleSystem extinguishParticle;
        //public GameObject pointLight;
    
    //store the coliders for each attack type
    public  Collider BasicAttackCollider;

    //timer to toggle coliders for attacks
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
    
    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnAttack()
    {
        //play attak animations and trigger colliders
        BasicAttack.Play();
        StartCoroutine(EnableColider(BasicAttackCollider));
    }

    private IEnumerator EnableColider(Collider _collider)
    {
        //turn on coliders,wait 1 second to allow hits, then turn it back off.
        _collider.enabled = true;
        yield return _waitForSeconds;
        _collider.enabled = false;
    }

}

