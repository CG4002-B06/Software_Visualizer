using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowerP2 : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public GameObject explosionEffect;
    public Transform grenade2Spawn;

    // Update is called once per frame
    void Update()
    {
    }

    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, grenade2Spawn.position, grenade2Spawn.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(grenade2Spawn.forward * throwForce, ForceMode.VelocityChange);
        GameObject.Destroy(grenade, 5f);
        Invoke("InvokeGrenadeExplosion", 2f);
    }

    public void InvokeGrenadeExplosion()
    {
        GameObject explosion = Instantiate(explosionEffect, new Vector3(0,0,0), transform.rotation);
        GameObject.Destroy(explosion, 4f);
    }
}
