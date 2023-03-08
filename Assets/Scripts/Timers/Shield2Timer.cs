using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shield2Timer : MonoBehaviour
{
    float currentTime = 10;
    public float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    public GameObject Timer;
    public GameObject ShieldCount;
    public Image ShieldBg;
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
            countdownText.color = Color.white;
        }
    }

    void Update()
    {
        if(hasStart)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
            Color temp = ShieldBg.color;
            temp.a = 100f;
            ShieldCount.SetActive(false);

            if (currentTime <= 3)
            {
                countdownText.color = Color.red;
            }
            if (currentTime <= 0)
            {
                currentTime = 0;
                countdownText.text = "" + currentTime;
                temp.a = 255f;
                ShieldCount.SetActive(false);
                shieldHealth.DeactivateShield();
                Timer.SetActive(false);
            }
        }
    }
}