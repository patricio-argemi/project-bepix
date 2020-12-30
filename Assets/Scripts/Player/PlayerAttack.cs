using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float startTimeBetweenAttacks;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int attackDamage;    

    private float timeBetweenAttacks;
    private InputActions inputActions;
    private Animator playerAnimator;
    private bool hasAttackCooldown;
    private PlayerController playerController;

    private void Awake()
    {
        inputActions = new InputActions();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Start()
    {
        inputActions.Player.Attack.performed += context => Attack();
        hasAttackCooldown = false;        
    }

    private void Update()
    {
        //attack cooldown        
        if (timeBetweenAttacks <= 0)
        {
            hasAttackCooldown = false;
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }        
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Attack()
    {
        Debug.Log(playerController.isInMidAir);
        if (!hasAttackCooldown && !playerController.isInMidAir)
        {
            hasAttackCooldown = true;

            playerAnimator.SetTrigger("isAttacking");

            Collider2D[] enemiesToAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);

            for (int i = 0; i < enemiesToAttack.Length; i++)
            {
                enemiesToAttack[i].GetComponent<Enemy>().TakeDamage(attackDamage);
            } 
        }
    }
}
