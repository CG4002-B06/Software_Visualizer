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
    public GameObject SWShield;
    bool hasStart = false;
    bool hasStart2 = false;
    bool player1 = false;
    bool player2 = false;

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void SetHasStart(bool status)
    {
        hasStart = status;
        Debug.Log(hasStart);
    }

    public void SetHasStart2(bool status)
    {
        hasStart2 = status;
        Debug.Log("Counter 2");
    }

    // public void SetText(int playerNumber)
    // {
    //     if(playerNumber == 1)
    //     {
    //         player1 = true;
    //         // currentTime = 10f;
    //     }
    //     if(playerNumber == 2)
    //     {
    //         player2 = true;
    //         // currentTime = 10f;
    //     }   
    // }

    void Update()
    {
        if(hasStart)
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
                countdownTextP1.text = "";

                ShieldCountP1.SetActive(true);
                ShieldPicP1.SetActive(true);
                CrackedShieldUI.SetActive(false);
                openScreen.OpenBlueScreen(false);
                hasStart = false;
                Timer.SetActive(false);
                currentTime = 10;
                shieldHealth.DeactivateShield();
            }
        }

        // if(hasStart && player2)
        // {
        //     openScreen.OpenBlueScreen(true);
        //     currentTime -= 1 * Time.deltaTime;
        //     countdownTextP2.text = currentTime.ToString("0");
        //     ShieldCountP2.SetActive(false);
        //     ShieldPicP2.SetActive(false);
            
        //     if(currentTime <= 3)
        //     {
        //         countdownTextP2.color = Color.red;
        //     }
        //     if(currentTime <= 1)
        //     {
        //         CrackedShieldUI.SetActive(true);
        //     }
        //     if(currentTime <= 0)
        //     {
        //         currentTime = 0;
        //         countdownTextP2.text = "" + currentTime;

        //         ShieldCountP2.SetActive(true);
        //         ShieldPicP2.SetActive(true);
        //         Timer.SetActive(false);
        //         CrackedShieldUI.SetActive(false);
        //         openScreen.OpenBlueScreen(false);
        //         shieldHealth.DeactivateShield();
        //     }
        // }

        // if(hasStart2 && player1)
        // {
        //     SWShield.SetActive(true);
        //     currentTime -= 1 * Time.deltaTime;
        //     countdownTextP1.text = currentTime.ToString("0");
        //     ShieldCountP1.SetActive(false);
        //     ShieldPicP1.SetActive(false);
            
        //     if(currentTime <= 3)
        //     {
        //         countdownTextP1.color = Color.red;
        //     }
        //     if(currentTime <= 1)
        //     {
        //         CrackedShieldUI.SetActive(true);
        //     }
        //     if(currentTime <= 0)
        //     {
        //         currentTime = 0;
        //         countdownTextP1.text = "" + currentTime;

        //         ShieldCountP1.SetActive(true);
        //         ShieldPicP1.SetActive(true);
        //         Timer.SetActive(false);
        //         SWShield.SetActive(false);
        //     }
        // }

        if(hasStart2)
        {
            SWShield.SetActive(true);
            currentTime -= 1 * Time.deltaTime;
            countdownTextP2.text = currentTime.ToString("0");
            ShieldCountP2.SetActive(false);
            ShieldPicP2.SetActive(false);
            
            if(currentTime <= 3)
            {
                countdownTextP2.color = Color.red;
            }
            if(currentTime <= 0)
            {
                currentTime = 0;
                countdownTextP2.text = "" + currentTime;
                countdownTextP2.text = "";

                ShieldCountP2.SetActive(true);
                ShieldPicP2.SetActive(true);
                SWShield.SetActive(false);
                hasStart2 = false;
                Timer.SetActive(false);
                currentTime = 10;
                shieldHealth.DeactivateShield();
            }
        }
    }
}