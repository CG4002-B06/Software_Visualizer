using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{

    public TextMeshProUGUI PlayerKillCount;

    // Start is called before the first frame update
    void Start()
    {
        PlayerKillCount.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerDeathCount(int killCount)
    {
        PlayerKillCount.text = "" + killCount;
    }
}
