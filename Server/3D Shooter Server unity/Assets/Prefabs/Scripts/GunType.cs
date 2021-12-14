using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunType {
    public string name;
    public KeyCode buttonIndex;
    public float attackDamage = 1;
    public float bulletForce = 20f;
    public float attackSpeed = 3f;
    public GameObject bulletPrefab;
    public Transform[] firePoints;
    [HideInInspector]
    public float attackTimer = 0;

}
