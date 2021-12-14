using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet :  Projectile
{
    

    void Start()
    {
        DestroyOnTime(5);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfPassedTheTarget();
    }

    private void OnDestroy()
    {
        base.OnDestroy();
    }

}
