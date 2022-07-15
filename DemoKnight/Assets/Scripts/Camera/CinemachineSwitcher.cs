using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator anim;
    private bool overWorldCamera = true;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if (overWorldCamera)
            anim.Play("EndGame");
        else
            anim.Play("OverWorld");
        overWorldCamera = !overWorldCamera;
    }

}
