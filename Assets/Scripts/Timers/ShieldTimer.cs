using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldTimer : MonoBehaviour
{
    float currentTime = 10;
    [SerializeField] TextMeshProUGUI countdownTextP1;
    [SerializeField] TextMeshProUGUI countdownTextP2;
    public GameObject Timer;
    public GameObject ShieldCountP1;
    public GameObject ShieldCountP2;
    public GameObject CrackedShieldUI;
    public GameObject ShieldPicP1;
    public GameObject ShieldPicP2;
    public OpenScreen openScreen;
    public Player shieldHealth;
    bool hasStart = false;
    bool player1 = false;
    bool player2 = false;

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void SetHasStart(bool status)
    {
        hasStart = status;
    }

    public void SetText(int playerNumber)
    {
        if(playerNumber == 1)
        {
            player1 = true;
            // currentTime = 10f;
        }
        if(playerNumber == 2)
        {
            player2 = true;
            // currentTime = 10f;
        }   
    }

    void Update()
    {
        if(hasStart && player1)
        {
            openScreen.OpenBlueScreen(true);
            currentTime -= 1 * Time.deltaTime;
            countdownTextP1.text = currentTime.ToString("0");
            ShieldCountP1.SetActive(false);
            ShieldPicP1.SetActive(false);
            
            if(currentTime <= 3)
            {
                countdownTextP1.color = Color.red;
            }
            if(currentTime <= 1)
            {
                CrackedShieldUI.SetActive(true);
            }
            if(currentTime <= 0)
            {
                currentTime = 0;
                countdownTextP1.text = "" + currentTime;

                ShieldCountP1.SetActive(true);
                ShieldPicP1.SetActive(true);
                Timer.SetActive(false);
                CrackedShieldUI.SetActive(false);
                openScreen.OpenBlueScreen(false);
                shieldHealth.DeactivateShield();
            }
        }

        if(hasStart && player2)
        {
            openScreen.OpenBlueScreen(true);
            currentTime -= 1 * Time.deltaTime;
            countdownTextP2.text = currentTime.ToString("0");
            ShieldCountP2.SetActive(false);
            ShieldPicP2.SetActive(false);
            
            if(currentTime <= 3)
            {
                countdownTextP2.color = Color.red;
            }
            if(currentTime <= 1)
            {
                CrackedShieldUI.SetActive(true);
            }
            if(currentTime <= 0)
            {
                currentTime = 0;
                countdownTextP2.text = "" + currentTime;

                ShieldCountP2.SetActive(true);
                ShieldPicP2.SetActive(true);
                Timer.SetActive(false);
                CrackedShieldUI.SetActive(false);
                openScreen.OpenBlueScreen(false);
                shieldHealth.DeactivateShield();
            }
        }
    }
}