using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private InputActions inputActions;

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        inputActions = new InputActions();        
    }

    void Start()
    {
        inputActions.Player.SpawnEnemy.performed += context => SpawnEnemy();        
    }

    private void SpawnEnemy()
    {                
        Instantiate(enemy);
    }
}
