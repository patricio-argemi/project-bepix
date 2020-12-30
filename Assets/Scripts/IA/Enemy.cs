using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;    
    private InputActions inputActions;
    private Animator enemyAnimator;
    private CapsuleCollider2D enemyCollider;

    [SerializeField]
    private int totalHealth;

    private void Awake()
    {
        inputActions = new InputActions();
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemyCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        health = 7;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    } 
    
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();            
        }
        else if (health == 1)
        {
            enemyAnimator.SetTrigger("takeHitCritical");
        }
        else enemyAnimator.SetTrigger("takeHit");
    }

    private void Die()
    {
        enemyAnimator.SetBool("die", true);
        enemyCollider.enabled = false;
    }
}
