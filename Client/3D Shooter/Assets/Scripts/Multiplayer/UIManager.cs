using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        GameManager.currentGameMode = GameMode.Multiplayer;
        startMenu.SetActive(false);
        usernameField.interactable = false;
        SceneManager.LoadScene("MultiplayerWorld");
        //Client.instance.ConnectToServer();
    }
}
