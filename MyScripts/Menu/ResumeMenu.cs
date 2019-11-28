using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    //public FixedButton ResumeBtn;

    // Update is called once per frame
    void Update()
    {

        //if (ResumeBtn.Pressed)
        //    Pause();

        if (GameIsPaused) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //TO:DO separate mainmenu with button resume
        SceneManager.LoadScene(0);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting the game..");
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        Application.Quit();
    }
}
