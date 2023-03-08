using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowerP1 : MonoBehaviour
{
    public float throwForce = 1000f;
    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
    }

    public void ThrowGrenade()
    {

        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        // GameObject.Destroy(grenade, 2f);
    }
}
