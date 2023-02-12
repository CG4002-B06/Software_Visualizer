using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
    }

    public void BulletShooter()
    {
        GameObject bullet = Instantiate(impactEffect, transform.position, transform.rotation);
   
        // RaycastHit hit;
        // if(Physics.Raycast(transform.position, transform.forward, out hit))
        // {
        //     // if(hit.rigidbody != null)
        //     // {
        //     //     hit.rigidbody.AddForce()
        //     // }
        //     GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //     Destroy(impactGO, 2f);
        // }

        // var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        // bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        // Destroy(gameObject);
    }
}
