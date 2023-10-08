using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif




public class AttackActions : MonoBehaviour
{
    //store the vfx for each attack type
    public ParticleSystem BasicAttack;
    public int HP;
    
    //store the coliders for each attack type
    public  Collider BasicAttackCollider;

    //timer to toggle coliders for attacks
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
     private WaitForSeconds _hitCooldown = new WaitForSeconds(2f);
    //private bool IsAttacking = false;
    //private bool IsDefending = false;
    private Animator _animator; // Reference to the player's Animator component
    private bool _hasAnimator;
    private int _animIDHit;
    private int _animIDDie;
    private CharacterController controller;
    private bool canTakeDamage = true; // control the players ability to get hit

    
    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator); 
        AssignAnimationIDs();
        controller = GetComponent<CharacterController>();
        HP = 50; // Set health point
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


    private void AssignAnimationIDs()
    {
        _animIDHit = Animator.StringToHash("Hit");
        _animIDDie = Animator.StringToHash("Die");
    }

    void OnTriggerEnter(Collider collision)   
    {
        //Debug.Log(collision.gameObject.name);
    }

    void OnTriggerStay(Collider collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    public void TakeDamage(int hitPoints)
    {
     if (_hasAnimator && canTakeDamage)
        {   //trigger hit animation
            _animator.SetTrigger(_animIDHit);
            HP = HP - hitPoints;
            // If HP is depleted, play the death animation
            if (HP <= 0)
            {
               // _animator.SetTrigger(_animIDDie);
                //dead = true;
                //StartCoroutine(EndScene());
            }
             // Start the cooldown coroutine
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Disable taking damage during cooldown
        yield return _hitCooldown; // Wait for 2 seconds
        canTakeDamage = true; // Enable taking damage again
    }


}

