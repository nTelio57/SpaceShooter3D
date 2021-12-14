using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{

    public int kills = 0;
    public int deaths = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ServerSend.UpdateScoreboard(GetComponent<Player>().id, this);
    }
}
