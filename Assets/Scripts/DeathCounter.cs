using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{

    public TextMeshProUGUI PlayerDeathCount;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeathCount.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerDeathCount(int deathCount)
    {
        PlayerDeathCount.text = "" + deathCount;
    }
}
