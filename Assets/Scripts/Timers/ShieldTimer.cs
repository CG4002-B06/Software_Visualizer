using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldTimer : MonoBehaviour
{
    float currentTime = 10;
    public float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    public GameObject Timer;
    public GameObject ShieldCount;
    public Image ShieldBg;
    public GameObject CrackedShieldUI;
    public GameObject ShieldPic;
    public OpenScreen openScreen;
    public Player shieldHealth;
    bool hasStart = false;

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void SetHasStart(bool status)
    {
        hasStart = status;
        if(status == true)
        {
            currentTime = 10f; // Remember to comment this out when testing with Game Engine
            countdownText.color = Color.white;
        }
    }

    void Update()
    {
        if(hasStart)
        {
            openScreen.OpenBlueScreen(true);
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
            ShieldCount.SetActive(false);
            ShieldPic.SetActive(false);
            
            if (currentTime <= 3)
            {
                countdownText.color = Color.red;
            }
            if(currentTime <= 1)
            {
                CrackedShieldUI.SetActive(true);
            }
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownText.text = "" + currentTime;

                ShieldCount.SetActive(true);
                ShieldPic.SetActive(true);
                Timer.SetActive(false);
                CrackedShieldUI.SetActive(false);
                openScreen.OpenBlueScreen(false);
                shieldHealth.DeactivateShield();
            }
        }
    }
}