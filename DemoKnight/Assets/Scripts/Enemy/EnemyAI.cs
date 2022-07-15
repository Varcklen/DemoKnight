using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Enemy enemy;

    public Transform AttackPoint;
    public float attackDamage = 1f;
    public Vector2 attackRange;
    public float attackSwingTime = 1.5f;
    public LayerMask Layers;

    public string DamageSound = "";
    public float speed = 2f;
    public float nextWaypointDistance = 3f;
    public bool inRange; //{ get; private set; }
    private bool inCharge = false;

    public Transform enemyGFX;
    private float scale;

    public Animator anim;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start() {
        //enemy = GameObject.Find("Rhombus").GetComponent<Enemy>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        target = Player.playerTransform;
        if (target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);

        scale = transform.lossyScale.x;
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target != null && !CharacterStats.isEnded)
            seeker.StartPath(new Vector2(rb.position.x, rb.position.y - (transform.localScale.y / 3.5f)), target.position, OnPathComplete);
       else
            anim.SetFloat("Speed", 0);

    }

    //private void OnCollisionStay2D(Collision2D other)
    //{
        //Debug.Log("Tag is " + other.gameObject.tag);
        //if (other.gameObject.tag == "Player")
        //{
        //    other.gameObject.GetComponent<CharacterStats>().TakeDamage(AttackDamage);
        //    Debug.Log("Enemy deal " + AttackDamage + " damage");
        //}
    //}

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {

        if (path == null || enemy.UnitIsAlive == false || target == null || inCharge == true || enemy.IsTaking == true)
            return;

        if (inRange == true)
        {
            inCharge = true;
            anim.SetBool("isAttacking", inCharge);
        } 
        else
        {
            if (currentWaypoint >= path.vectorPath.Count)
            {
                //reachedEndofPath = true;
                //Debug.Log("true");
                return;
            }
            else
            {
                //Debug.Log("false");
                //reachedEndofPath = false; ;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * 100 * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            //float distanceBetweenPoints = Vector2.Distance(transform.position, target.position);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            float speedAnim = 0;
            if (rb.velocity.x != 0 || rb.velocity.y != 0)
                speedAnim = 1;
            anim.SetFloat("Speed", speedAnim);

            if (rb.velocity.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(scale, scale, scale);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(-scale, scale, scale);
            }
        }
        
    }

    void Attack()
    {
        if (enemy.UnitIsAlive == true && enemy.IsTaking == false)
        {
            
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint.position, attackRange, 0, Layers); //AttackPoint.position

            foreach (Collider2D ally in hitEnemies)
            {
                if (ally.tag == "Player")
                    ally.GetComponentInParent<Player>().PlayerTakeDamage(attackDamage);
                //Debug.Log(ally.name + " is hitted.");
            }
        }
            
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPoint.position, attackRange);
    }

    public void PlaySoundAttack()
    {
        //Debug.Log("Slash");
        if (DamageSound != "")
            FindObjectOfType<AudioManager>().Play(DamageSound);
        else
            Debug.LogWarning("WARNING! Sound: " + DamageSound + " for " + gameObject.name + " not set.");
    }

    public void EndAnimation()
    {
        inRange = false;
        inCharge = false;
        anim.SetBool("isAttacking", inCharge);
    }

}
