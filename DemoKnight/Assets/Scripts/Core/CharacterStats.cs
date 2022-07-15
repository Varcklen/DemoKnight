using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public static int EnemiesKilled = 0;
    public static bool isEnded = false;
    public float maxHealth;
    [HideInInspector] public float currentHealth;

    [HideInInspector] public bool UnitIsAlive;
    [HideInInspector] public bool IsTaking;
    //public GameObject scriptCinemachineSwitcher;

    private float lastDamageTime = 0f;
    public float damageDelay = 0f;
    public Animator anim;

    public string takeDamageSound = "";
    public SpriteRenderer sprite;
    private bool isHidden = false;

    [HideInInspector]
    public GameObject endOfGame;
    private float canvasVision = 0f;
    private Image scriptBackground;

    public bool hasStunResist = false;

    public void TakeDamage(float damage)
    {
        if (UnitIsAlive == true && Time.time >= lastDamageTime)
        {
            currentHealth -= damage;
            //Debug.Log(transform.name + " takes " + damage + " damage.");

            lastDamageTime = Time.time + damageDelay;
            if (currentHealth <= 0)
                Die();
            else
                Take();
        }
    }

    void Take()
    {
        FindObjectOfType<AudioManager>().Play(takeDamageSound);
        if (anim != null && !hasStunResist)
        {
            IsTaking = true;
            anim.SetBool("isTaking", IsTaking);
        }
        if (damageDelay > 0)
        {
            StartCoroutine(HiddenChange());
        }

    }

    IEnumerator HiddenChange()
    {
        while (Time.time < lastDamageTime)
        {
            yield return new WaitForSeconds(0.05f);
            //Debug.Log("Change");
            if (isHidden)
                sprite.color = new Color(1f, 1f, 1f, 1f);
            else
                sprite.color = new Color(1f, 1f, 1f, 0f);
            isHidden = !isHidden;
        }
        sprite.color = new Color(1f, 1f, 1f, 1f);
        isHidden = false;
    }

    public virtual void Die()
    {
        UnitIsAlive = false;
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        Collider2D[] offColliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in offColliders)
        {
            collider.enabled = false;
        }
        anim.SetBool("isDead", true);
        if (gameObject.tag == "Player")
            EndOfGame();
        else if (!isEnded)
        {
            EnemiesKilled++;
            GameObject.Find("KillsText").GetComponent<TextMeshProUGUI>().text = EnemiesKilled.ToString();
        }
    }

    void EndOfGame()
    {
        isEnded = true;
        if (Sound.music != null)
            Sound.music.source.volume = 0f;

        sprite.sortingOrder = 5;
        FindObjectOfType<AudioManager>().Play("EndOfGame");
        FindObjectOfType<CinemachineSwitcher>().SwitchState();
        GameObject.Find("CanvasUI").GetComponent<CanvasUI>().CanvasUIActivate(false);

        StartCoroutine(CanvasVision());

    }

    IEnumerator CanvasVision()
    {
        yield return new WaitForSeconds(2.5f);
        endOfGame.GetComponent<CanvasUI>().CanvasUIActivate(true);
        scriptBackground = GameObject.Find("Background").GetComponent<Image>();
        GameObject.Find("TextKillsNumber").GetComponent<Text>().text = EnemiesKilled.ToString();
        GameObject.Find("TextTimeNumber").GetComponent<Text>().text = (PlayerCreate.time[1] <= 9 ? ("0" + PlayerCreate.time[1]) : PlayerCreate.time[1].ToString()) + ":" + (PlayerCreate.time[0] <= 9 ? ("0" + PlayerCreate.time[0]) : PlayerCreate.time[0].ToString());

        scriptBackground.color = new Color(1f, 1f, 1f, canvasVision);
        StartCoroutine(CanvasVisionUp());

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    IEnumerator CanvasVisionUp()
    {
        while (canvasVision < 1f)
        {
            yield return new WaitForSeconds(0.04f);
            canvasVision = canvasVision + 0.04f;
            scriptBackground.color = new Color(1f, 1f, 1f, canvasVision);
        }
        scriptBackground.color = new Color(1f, 1f, 1f, 1f);
    }

    void DestroyDead()
    {
        Destroy(gameObject, 1.0f);
        //Debug.Log(transform.name + " died.");
    }

    void AnimTake()
    {
        IsTaking = false;
        anim.SetBool("isTaking", IsTaking);
    }
}
