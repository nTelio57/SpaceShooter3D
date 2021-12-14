using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        if (menuPanel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            if (GameManager.currentGameMode == GameMode.Singleplayer)
                Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (GameManager.currentGameMode == GameMode.Singleplayer)
                Time.timeScale = 1;
        }
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1;
        Toggle();
    }

    public void OnOptionsClick()
    {
        
    }

    public void OnMainMenuClick()
    {
        Time.timeScale = 1;

        if (GameManager.currentGameMode == GameMode.Multiplayer)
            Client.instance.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnDesktopClick()
    {
        if (GameManager.currentGameMode == GameMode.Multiplayer)
            Client.instance.Disconnect();
        Application.Quit();
    }
}
