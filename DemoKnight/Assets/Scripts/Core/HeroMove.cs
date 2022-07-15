using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMove : MonoBehaviour
{
    public Player scriptPlayer;
    public Animator animator;
    private float scale;

    public float speed = 5f;
    public Rigidbody2D rb;
    public Transform playerGFX;

    Vector2 movement;
    void Start()
    {
        scale = transform.lossyScale.x;
        if (SceneManager.GetActiveScene().name == "Main Menu")
            playerGFX.localScale = new Vector3(-scale, scale, scale);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (scriptPlayer.UnitIsAlive == true)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            float speedAnim = 0;
            if (movement.x != 0 || movement.y != 0)
                speedAnim = 1;
            animator.SetFloat("Speed", speedAnim);

            if (movement.x >= 0.01f)
                playerGFX.localScale = new Vector3(scale, scale, scale);
            else if (movement.x <= -0.01f)
                playerGFX.localScale = new Vector3(-scale, scale, scale);
        }

    }

}
