using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterStats
{
    void Start()
    {

        UnitIsAlive = true;
        currentHealth = maxHealth;
    }

    public void EnemyTakeDamage(float damage)
    {
        TakeDamage(damage);
    }
}