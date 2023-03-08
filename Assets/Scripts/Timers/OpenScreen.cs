using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenScreen : MonoBehaviour
{
    float currentTime = 0;
    public float startingTime = 0f;
    public float fadeSpeed = 1.5f;

    public GameObject RedPanel;
    public Image RedScreenEffect;
    public GameObject BluePanel;
    public Image BlueScreenEffect;
    public GameObject YellowPanel;
    public Image YellowScreenEffect;
    public GameObject WhitePanel;
    public Image WhiteScreenEffect;

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
            currentTime = 1f;
        }
    }

    public void OpenRedScreenForGrenade()
    {
        hasHit = true;
        hasReload = false;
        currentTime = 0.5f;
    }

    public void InvokeRedScreen()
    {
        Invoke("OpenRedScreenForGrenade", 2f);
    }

    // When firing bullets
    public void OpenWhiteScreen(bool status)
    {
        hasFired = status;
        hasHit = false;
        if(status == true)
        {
            currentTime = 1f;
        }
    }

    // When shield is on
    public void OpenBlueScreen(bool status)
    {
        hasShield = status;
        if(hasShield == true)
        {
            BluePanel.SetActive(true);
        }
    }

    // When reloading bullets
    public void OpenYellowScreen(bool status)
    {
        hasReload = status;
        hasHit = false;
        if(status == true)
        {
            currentTime = 0.5f;
        }
    }

    void Update()
    {
        // Timers
        if(hasHit)
        {
            RedPanel.SetActive(true);
            float temp = RedScreenEffect.color.a;
            temp -= Time.deltaTime * fadeSpeed;
            RedScreenEffect.color = new Color(RedScreenEffect.color.r, RedScreenEffect.color.g, RedScreenEffect.color.b, temp);

            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                RedPanel.SetActive(false);
                hasHit = false;
                RedScreenEffect.color = new Color(RedScreenEffect.color.r, RedScreenEffect.color.g, RedScreenEffect.color.b, 0.58823529411f);
            }
        }

        if(hasFired)
        {
            WhitePanel.SetActive(true);
            float temp = WhiteScreenEffect.color.a;
            temp -= Time.deltaTime * fadeSpeed;
            WhiteScreenEffect.color = new Color(WhiteScreenEffect.color.r, WhiteScreenEffect.color.g, WhiteScreenEffect.color.b, temp);

            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                WhitePanel.SetActive(false);
                hasFired = false;
                WhiteScreenEffect.color = new Color(WhiteScreenEffect.color.r, WhiteScreenEffect.color.g, WhiteScreenEffect.color.b, 0.23529411764f);
            }
        }

        if(hasShield == true)
        {
            float temp = BlueScreenEffect.color.a;
            temp += Time.deltaTime * fadeSpeed;
            
            if(temp <= 0.23529411764)
            {
                BlueScreenEffect.color = new Color(BlueScreenEffect.color.r, BlueScreenEffect.color.g, BlueScreenEffect.color.b, temp);
            }
        }
        
        if(hasShield == false) 
        {
            // float temp = BlueScreenEffect.color.a;
            // temp -= Time.deltaTime * fadeSpeed;
            // BlueScreenEffect.color = new Color(BlueScreenEffect.color.r, BlueScreenEffect.color.g, BlueScreenEffect.color.b, temp);
            BluePanel.SetActive(false);
        }

        if(hasReload)
        {
            YellowPanel.SetActive(true);
            float temp = YellowScreenEffect.color.a;
            temp += Time.deltaTime * fadeSpeed;
            if(temp <= 0.58823529411)
            {
                YellowScreenEffect.color = new Color(YellowScreenEffect.color.r, YellowScreenEffect.color.g, YellowScreenEffect.color.b, temp);
            }
            
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0)
            {
                YellowPanel.SetActive(false);
                hasReload = false;
                YellowScreenEffect.color = new Color(YellowScreenEffect.color.r, YellowScreenEffect.color.g, YellowScreenEffect.color.b, 0f);
            }
        }
    }     

}
