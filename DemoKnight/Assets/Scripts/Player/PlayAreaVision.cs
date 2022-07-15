using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaVision : MonoBehaviour
{

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
