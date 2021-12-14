using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDStats : MonoBehaviour
{
    public TMP_Text statsField;
    public PlayerStatistics playerStatistics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string statsText = "";
        PlayerStatistics ps;
        foreach (PlayerManager pm in GameManager.players.Values)
        {
            ps = pm.GetComponent<PlayerStatistics>();
            statsText += pm.id + " " + pm.username + "   " + ps.kills + "-" + ps.deaths+"\n";
        }
        statsField.text = statsText;
    }
}
