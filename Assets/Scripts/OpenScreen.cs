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

    bool hasShield = false;
    bool hasHit = false;
    bool hasReload = false;
    
    // When shot or hit by grenade
    public void OpenRedScreen(bool status)
    {
        hasHit = status;
        if(status == true)
        {
            currentTime = 1;
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
            }
        }
    }     

}
