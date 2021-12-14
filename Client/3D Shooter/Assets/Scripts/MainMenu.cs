using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject multiplayerPanel;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnTrainingClicked()
    {
        GameManager.currentGameMode = GameMode.Singleplayer;
        SceneManager.LoadScene("SingleplayerWorld");
    }

    public void OnMultiplayeClicked()
    {
        mainPanel.SetActive(false);
        multiplayerPanel.SetActive(true);
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
    }
}
