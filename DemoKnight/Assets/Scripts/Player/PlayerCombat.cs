using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask enemyLayers;
    public Animator anim;

    public float attackRange = 0.5f;
    public int AttackDamage = 2;
    public float attackRate = 0.35f;

    private bool isAttacking = false;

    //[SerializeField]
    //private Collider2D _attackTrigger;

    void Update()
    {
        if (isAttacking == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAttacking = true;
                
            }
            anim.SetBool("isAttacking", isAttacking);
        }
        
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers); //LayerMask.GetMask("Hitbox")

        //ContactFilter2D.minDepth;
        foreach (Collider2D enemy in hitEnemies)
        {
            //if (enemy is CircleCollider2d)
            if (enemy.tag == "EnemyHitBox")
                enemy.GetComponentInParent<CharacterStats>().TakeDamage(AttackDamage); 
            //Debug.Log("We hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    { 
        if (AttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    public void PlaySoundSlash()
    {
        //Debug.Log("Slash");
        FindObjectOfType<AudioManager>().Play("Sword Slash");
    } 

    public void EndAnimation()
    {
        isAttacking = false;
    }
}
