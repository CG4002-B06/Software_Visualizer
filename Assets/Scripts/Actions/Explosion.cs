using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion: MonoBehaviour
{
    public GameObject explosionEffect;

    // Update is called once per frame
    void Update()
    {
    }

    public void InvokeGrenadeExplosion()
    {
        Invoke("GrenadeExplosion", 2f);
    }


}
