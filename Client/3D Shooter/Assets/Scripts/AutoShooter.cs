using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject buletPrefab;
    public float attackSpeed = 2f;
    public float bulletForce = 100f;
    public Transform target;

    float timer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= (1 / attackSpeed))
        {
            GameObject bullet = Instantiate(buletPrefab, firePoint.position, buletPrefab.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (target == null)
            {
                rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce((target.position - firePoint.position).normalized * Vector3.Distance(target.position, firePoint.position), ForceMode.Impulse);
            }
            
            
            
            timer = 0;
        }
    }
}
