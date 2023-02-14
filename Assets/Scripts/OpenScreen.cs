using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenScreen : MonoBehaviour
{
    float currentTime = 0;
    public float startingTime = 0f;

    public GameObject RedPanel;
    public GameObject BluePanel;
    public GameObject YellowPanel;
    public GameObject WhitePanel;

    bool hasShield = false;
    bool hasHit = false;
    bool hasFired = false;
    bool hasReload = false;
    
    // When shot or hit by grenade
    public void OpenRedScreen(bool status)
    {
        hasHit = status;
        hasReload = false;
        if(status == true)
        {
            currentTime = 0.5f;
        }
    }

    public void OpenWhiteScreen(bool status)
    {
        hasFired = status;
        hasHit = false;
        if(status == true)
        {
            currentTime = 0.3f;
        }
    }

    // When shield is on
    public void OpenBlueScreen(bool status)
    {
        hasShield = status;
    }

    public void OpenYellowScreen(bool status)
    {
        hasReload = status;
        hasHit = false;
        if(status == true)
        {
            currentTime = 1;
        }
    }

    void Update()
    {
        if(hasHit)
        {
            RedPanel.SetActive(true);
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                RedPanel.SetActive(false);
                hasHit = false;
            }
        }

        if(hasFired)
        {
            WhitePanel.SetActive(true);
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                WhitePanel.SetActive(false);
                hasFired = false;
            }
        }

        if(hasShield == true)
        {
            BluePanel.SetActive(true);
        }
        
        if(hasShield == false) 
        {
            BluePanel.SetActive(false);
        }

        if(hasReload)
        {
            YellowPanel.SetActive(true);
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                YellowPanel.SetActive(false);
                hasReload = false;
            }
        }
    }     

}
