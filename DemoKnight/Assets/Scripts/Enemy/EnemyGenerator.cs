using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private int rand;

    public GameObject enemyPrefab;
    public float respawnTime = 5f;
    public int enemyLimit = 5;
    public float speedAnimation = 1.25f;
    public float powerUpTime = 20f;
    //private Vector2 screenBounds;
    public LayerMask enemyMask;

    private Vector3[] positionSummon = new Vector3[4];
    public string circleTag;

    public GameObject playArea;

    void Start()
    {

        int i = 0;
        GameObject[] sc = GameObject.FindGameObjectsWithTag(circleTag);
        foreach (GameObject circle in sc)
        {
            positionSummon[i] = circle.transform.position;
            i++;
            if (positionSummon.Length < i){
                break;
            }
        }
    }

    public void StartGeneration( GameObject player )
    {
        //scriptPlayer = player.GetComponent<Player>();
        if (enabled)
        {
            StartCoroutine(EnemyCreation());
            if (powerUpTime != 0)
                StartCoroutine(EnemyPowerUp());
        }   
    }

    void SpawnEnemy()
    {
        int i = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //LayerMask.GetMask("Hitbox")
        foreach (GameObject e in enemies)
        {
            i++;
        }

        if (i < enemyLimit)
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        rand = Random.Range(0, positionSummon.Length);
        GameObject enemy = Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position = positionSummon[rand];
        enemy.GetComponent<Animator>().SetFloat("speedAttack", speedAnimation);

        rand = Random.Range(1, 16);
        if (rand != 1 && rand != 4)
            enemy.GetComponent<EnemyAI>().speed = enemy.GetComponent<EnemyAI>().speed * (Random.Range(0.7f, 1.3f));
        switch (rand)
        {
            case 1://Быстрый
                enemy.GetComponent<Animator>().SetFloat("speedAttack", speedAnimation * 1.5f);
                enemy.GetComponent<EnemyAI>().speed = enemy.GetComponent<EnemyAI>().speed * 1.5f;
                enemy.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
                break;
            case 2://Сильный
                enemy.GetComponent<EnemyAI>().attackDamage = 2f;
                enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                break;
            case 3://+ХП
                enemy.GetComponent<CharacterStats>().maxHealth = enemy.GetComponent<CharacterStats>().maxHealth + 6;
                enemy.GetComponent<CharacterStats>().currentHealth = enemy.GetComponent<CharacterStats>().maxHealth;
                enemy.GetComponent<SpriteRenderer>().color = new Color(0.7f, 1f, 0f, 1f);
                break;
            case 4://Больше, медленнее, больше урона, +ХП немного
                enemy.GetComponent<Animator>().SetFloat("speedAttack", speedAnimation * 0.75f);
                enemy.GetComponent<EnemyAI>().speed = enemy.GetComponent<EnemyAI>().speed * 0.5f;
                enemy.GetComponent<EnemyAI>().attackDamage = 3f;
                enemy.GetComponent<EnemyAI>().attackRange = enemy.GetComponent<EnemyAI>().attackRange + (enemy.GetComponent<EnemyAI>().attackRange * 0.4f);
                enemy.GetComponent<CharacterStats>().maxHealth = enemy.GetComponent<CharacterStats>().maxHealth + 4;
                enemy.GetComponent<CharacterStats>().currentHealth = enemy.GetComponent<CharacterStats>().maxHealth;
                enemy.transform.localScale = enemy.transform.localScale + new Vector3(1.5f, 1.5f, 0f);
                enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 1f);
                break;
            case 5: //Резист к стану
                enemy.GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", 0.004f);
                enemy.GetComponent<CharacterStats>().hasStunResist = true;
                break;
            case 6: //Резист к урону после получения урона
                enemy.GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", 0.002f);
                enemy.GetComponent<SpriteRenderer>().material.SetColor("_Color", new Color(0, 255, 255));
                enemy.GetComponent<CharacterStats>().hasStunResist = true;
                enemy.GetComponent<CharacterStats>().damageDelay = 2f;
                break;
        }
    }

    IEnumerator EnemyCreation()
    {
        while (!CharacterStats.isEnded)
        {
            yield return new WaitForSeconds(respawnTime);
            if (!CharacterStats.isEnded)
                SpawnEnemy();
        }
    }

    IEnumerator EnemyPowerUp()
    {
        while (!CharacterStats.isEnded)
        {
            yield return new WaitForSeconds(powerUpTime);
            speedAnimation = speedAnimation + 0.1f;
            enemyLimit = enemyLimit + 1;
            respawnTime = respawnTime - 0.2f;
            if (respawnTime < 1f)
                respawnTime = 1f;
        }
    }

}
