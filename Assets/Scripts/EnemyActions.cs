using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif


//[RequireComponent(typeof(ParticleSystem))]

public class EnemyActions : MonoBehaviour
{
    
    private int _animIDHit;
    private int _animIDDie;
    private bool _hasAnimator;
    private Animator _animator;
    public int HP;
    public bool dead = false;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(5f);
    
    private void Start()
    {
        AssignAnimationIDs();
        _hasAnimator = TryGetComponent(out _animator);  
        HP = 50;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {        
        //check if hit by a weapon
        if (collision.gameObject.tag == "weapon")
        {   
            //get the controller script for the attack containing it's stats
            AttackVfxCTRL objectCTRL = collision.gameObject.GetComponent<AttackVfxCTRL>();
            //if an attack weapon take damage
            if (objectCTRL.actionType == "attack" && !dead)
            {

                TakeDamage(objectCTRL.hitPoints);
  
            }
        }   
    }

    private void AssignAnimationIDs()
        {
            _animIDHit = Animator.StringToHash("Hit");
            _animIDDie = Animator.StringToHash("Die");
        }

    public void TakeDamage(int hitPoints)
    {
     if (_hasAnimator)
        {   //trigger hit animation
            _animator.SetTrigger(_animIDHit);
            HP = HP - hitPoints;
            if (HP <= 0)
            {
                _animator.SetTrigger(_animIDDie);
                dead = true;
                StartCoroutine(EndScene());
            }
        }
    }

    private IEnumerator EndScene()
    {
        yield return _waitForSeconds;
        SceneManager.LoadScene("EndScene");
    }

}

