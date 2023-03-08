using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
    }

    public void ShootBullet()
    {
        // GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        // bullet.GetComponent<ShotBehavior>().setTarget(new Vector3(0, 0, 90));
        // GameObject.Destroy(bullet, 2f);

        Invoke("InvokeLaserExplosion", 1f);   
    }

    public void InvokeLaserExplosion()
    {
        GameObject impact = Instantiate(impactEffect, transform.position, transform.rotation);
        GameObject.Destroy(impact, 2f);
    }
}
