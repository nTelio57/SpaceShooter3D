using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet :  Projectile
{
    

    void Start()
    {
        base.Start();
        DestroyOnTime(5);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfPassedTheTarget();
    }


}
