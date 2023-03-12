using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Summary : MonoBehaviour
{
    public TextMeshProUGUI p1Death;
    public TextMeshProUGUI p1ShotAcc;
    public TextMeshProUGUI p1GrenadeAcc;
    public TextMeshProUGUI p2Death;
    public TextMeshProUGUI p2ShotAcc;
    public TextMeshProUGUI p2GrenadeAcc;
    int p1DeathCounter;
    int p1ShotAccuracy;
    int p1GrenadeAccuracy;
    int p2DeathCounter;
    int p2ShotAccuracy;
    int p2GrenadeAccuracy;

    void Start()
    {

    }

    void Update()
    {
        p1Death.text = "" + p1DeathCounter;
        p1ShotAcc.text = "" + p1ShotAccuracy + "%";
        p1GrenadeAcc.text = "" + p1GrenadeAccuracy + "%";
        p2Death.text = "" + p2DeathCounter; 
        p2ShotAcc.text = "" + p2ShotAccuracy + "%";
        p2GrenadeAcc.text = "" + p2GrenadeAccuracy + "%";
    }

    public void SendSummary(int p1D, int p1SA, int p1GA, int p2D, int p2SA, int p2GA)
    {
        p1DeathCounter = p1D;
        p1ShotAccuracy = p1SA;
        p1GrenadeAccuracy = p1GA;
        p2DeathCounter = p2D;
        p2ShotAccuracy = p2SA;
        p2GrenadeAccuracy = p2GA;
    }
}
