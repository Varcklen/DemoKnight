using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : CharacterStats
{
    //public HealthBar HealthBar;
    [HideInInspector] public static Transform playerTransform;
    public static GameObject player;// { get; private set; }
    private HealthBar scriptHealthBar = null;

    void Awake()
    {
        player = gameObject;
        playerTransform = player.transform;
        UnitIsAlive = true;
        currentHealth = maxHealth;
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            scriptHealthBar = HealthBar.healthBar.GetComponent<HealthBar>();
            scriptHealthBar.SetMaxHealth(maxHealth, currentHealth);
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        TakeDamage(damage);
        scriptHealthBar.SetHealth(currentHealth);
    }

}
