using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    public void FullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
            Debug.Log("Fullscreen is active");
        else
            Debug.Log("Fullscreen is enabled");
    }
}
