using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerCreate : MonoBehaviour
{
    public static int[] time = { 0, 0 };

    public GameObject playerPrefab;
    public GameObject scriptEndCamera;
    public GameObject endOfGame;

    public GameObject readySprite;
    public float spriteChange = 0.5f;
    public Sprite[] readySprites;
    private int sprite = 0;

    private TextMeshProUGUI spriptTimeText;

    private void Start()
    {
        CharacterStats.isEnded = false;
        CharacterStats.EnemiesKilled = 0;
        time[0] = 0;
        time[1] = 0;
        spriptTimeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(WaitForIt());
    }

    IEnumerator WaitForIt()
    {
        //readySprite.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(SpriteChange());
    }

    IEnumerator SpriteChange()
    {
        while (sprite < readySprites.Length)
        {
            yield return new WaitForSeconds(spriteChange);
            readySprite.GetComponent<SpriteRenderer>().sprite = readySprites[sprite];
            sprite++;
        }
        StartCoroutine(SpriteEnd());
        Create();
    }

    IEnumerator SpriteEnd()
    {
        yield return new WaitForSeconds(0.8f);
        readySprite.SetActive(false);
    }

    void Create()
    {
        //Debug.Log("Summoned!");
        GameObject player = Instantiate(playerPrefab) as GameObject;
        player.transform.position = new Vector2();
        gameObject.GetComponent<EnemyGenerator>().StartGeneration(player);
        scriptEndCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        //player.GetComponent<Animator>().speed = 2f;
        enabled = false;

        player.GetComponent<CharacterStats>().endOfGame = endOfGame;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (!CharacterStats.isEnded)
        {
            yield return new WaitForSeconds(1);
            time[0]++;
            if (time[0] >= 60)
            {
                time[0] = 0;
                time[1]++;
            }
            spriptTimeText.text = (time[1] <= 9 ? ("0"+time[1]) : time[1].ToString()) + ":" + (time[0] <= 9 ? ("0" + time[0]) : time[0].ToString());
        }
    }
}
