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
    private int _animIDAttack;
    private bool _hasAnimator;
    private Animator _animator;
    public int HP;
    public bool dead = false;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(5f); // Countdown to move to next scene after dragon's death
    private WaitForSeconds _attackCooldown = new WaitForSeconds(5f); // Time between attacks
    private bool _canAttack = true; // Check if enemy can attack or not
    private bool IsAttacking = false; // track the character's current state
     public int hitPoints = 5; // Attack power of character
    
    
    private void Start()
    {
        AssignAnimationIDs();
        _hasAnimator = TryGetComponent(out _animator);  
        HP = 50; // Set health points
        // Start the coroutine to attack periodically
        StartCoroutine(AttackPeriodically());
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
        // check if hit a player while attacking
        if (collision.gameObject.tag == "Player" && IsAttacking)
        {
            attackChar(collision);
        }
    }
    
    void OnTriggerStay(Collider collision)   
    {
         if (collision.gameObject.tag == "Player" && IsAttacking)
        {
            attackChar(collision);
        }
        //Debug.Log(collision.gameObject.name);
    }

    private void AssignAnimationIDs()
        {
            _animIDHit = Animator.StringToHash("Hit");
            _animIDDie = Animator.StringToHash("Die");
            _animIDAttack = Animator.StringToHash("Attack");
        }

    public void TakeDamage(int hitPoints)
    {
     if (_hasAnimator)
        {   //trigger hit animation
            _animator.SetTrigger(_animIDHit);
            HP = HP - hitPoints;
            // If HP is depleted, play the death animation
            if (HP <= 0)
            {
                _animator.SetTrigger(_animIDDie);
                dead = true;
                StartCoroutine(EndScene());
            }
        }
    }

     private IEnumerator AttackPeriodically()
    {
        while (!_canAttack)
        {
            yield return null; // Wait until _canAttack is true
            IsAttacking = false;
        }

        while (!dead)
        {
            // Play the attack animation here
            if (_hasAnimator)
            {
                _animator.SetTrigger(_animIDAttack);
                WaitForSeconds _anim = new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length/2);
                yield return _anim; // wait halfway through the attack animation
                IsAttacking = true;
                yield return _anim; // Wait for the attack animation to finish
                IsAttacking = false;
            }
            
            // Wait for the attack cooldown
            yield return (_attackCooldown);
        }
    }

    private IEnumerator EndScene()
    {
        yield return _waitForSeconds;// Wait a bit
        SceneManager.LoadScene("EndScene"); // Move to the next scene
    }

    private void attackChar(Collider collision)
    { 
        AttackActions playerCTRL = collision.gameObject.GetComponent<AttackActions>();
        playerCTRL.TakeDamage(hitPoints);
        
    }

}

