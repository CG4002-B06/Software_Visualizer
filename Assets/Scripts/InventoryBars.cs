using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBars : MonoBehaviour
{
    public Image[] ammoPoints;
    public Image[] grenadePoints;
    public Image[] shieldPoints;

    public void SetAmmoBar(int ammo)
    {
        for (int i = 0; i < ammoPoints.Length; i++)
        {
            ammoPoints[i].enabled = !DisplayAmmoPoint(ammo, i);
        }
    }

    public void SetGrenadeBar(int grenades)
    {
        for (int i = 0; i < grenadePoints.Length; i++)
        {
            grenadePoints[i].enabled = !DisplayGrenadePoint(grenades, i);
        }
    }

    public void SetShieldBar(int shields)
    {
        for (int i = 0; i < shieldPoints.Length; i++)
        {
            shieldPoints[i].enabled = !DisplayShieldPoint(shields, i);
        }
    }

    bool DisplayAmmoPoint(int ammo, int pointNumber)
    {
        return pointNumber >= ammo;
    }

    bool DisplayGrenadePoint(int grenade, int pointNumber)
    {
        return pointNumber >= grenade;
    }

    bool DisplayShieldPoint(int shield, int pointNumber)
    {
        return pointNumber >= shield;
    }
}