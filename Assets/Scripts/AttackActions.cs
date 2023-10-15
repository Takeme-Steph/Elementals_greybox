using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif




public class AttackActions : MonoBehaviour
{
    //store the vfx for each attack type
    public ParticleSystem BasicAttack;
    //store the coliders for each attack type
    public  Collider BasicAttackCollider;
    
    // Reference to the UI elements for health bar
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public int HP;
    private int maxHP;
    private bool _isDead;

    //timer to toggle coliders for attacks
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1f);
    //cooldown timer for player's hitbox
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
        maxHP = 50;
        healthText.text = "HP: " + HP; // set health text value
        healthSlider.value = (float)HP / maxHP; // set health slider value
        _isDead = false;
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
            UpdateHealthBar(); // Update the health bar
            // Start the cooldown coroutine
            StartCoroutine(DamageCooldown());
            // If HP is depleted, play the death animation
            if (HP <= 0)
            {
                _animator.SetTrigger(_animIDDie);
                _isDead = true;
                StartCoroutine(EndScene());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false; // Disable taking damage during cooldown
        yield return _hitCooldown; // Wait for 2 seconds
        canTakeDamage = true; // Enable taking damage again
    }

    // Update the health bar UI elements
    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)HP / maxHP;
        }

        if (healthText != null)
        {
            healthText.text = "HP: " + HP;
        }
    }
    private IEnumerator EndScene()
    {
        yield return _waitForSeconds;// Wait a bit
        SceneManager.LoadScene("LoseScene"); // Move to the next scene
    }

}

