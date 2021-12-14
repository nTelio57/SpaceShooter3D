using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernameCanvas : MonoBehaviour
{
    [Header("Player")]
    public PlayerManager playerManager;
    public TMP_Text usernameText;

    void Start()
    {
        if (GameManager.currentGameMode == GameMode.Multiplayer)
        {
            usernameText.text = playerManager.username;
            if (playerManager.id == FindObjectOfType<Client>().myId)
                usernameText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
