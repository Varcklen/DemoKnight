using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    EnemyAI myParent;

    void Start()
    {
        myParent = transform.parent.GetComponent<EnemyAI>();
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            myParent.inRange = true;
        }
    }
}
