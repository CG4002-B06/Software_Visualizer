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
    public GameObject P1ShieldButton;
    public Image FrontShieldBar;
    public OpenScreen openScreen;
    public Player shieldHealth;
    bool hasStart = false;

    public void SetHasStart(bool status)
    {
        hasStart = status;
        if(status == true)
        {
            currentTime = 10;
            countdownText.color = Color.white;
            P1ShieldButton.SetActive(false);
        }
    }

    void Update()
    {
        if(hasStart)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 3)
            {
                countdownText.color = Color.red;
            }
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownText.text = "" + currentTime;
                shieldHealth.DeactivateShield();
                Timer.SetActive(false);
                openScreen.OpenBlueScreen(false);
            }
        }
    }
}