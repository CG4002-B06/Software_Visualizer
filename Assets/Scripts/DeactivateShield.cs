using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateShield : MonoBehaviour
{
    public GameObject Shield;
    public void ShieldOff()
    {
        Shield.SetActive(false);
    } 
}
