using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        //Debug.Log("Loading menu...");
        //��� �������� ����. ����, �������� �� ��� ����
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitMenu()
    {
       //Debug.Log("Quitting game...");
        Application.Quit();
    }
}
