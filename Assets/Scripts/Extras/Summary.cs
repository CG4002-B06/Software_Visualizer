using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Summary : MonoBehaviour
{
    public TextMeshProUGUI playerDeath;
    public TextMeshProUGUI playerShotAcc;
    public TextMeshProUGUI playerGrenadeAcc;
    public TextMeshProUGUI opponentDeath;
    public TextMeshProUGUI opponentShotAcc;
    public TextMeshProUGUI opponentGrenadeAcc;
    int playerDeathCounter;
    int playerShotAccuracy;
    int playerGrenadeAccuracy;
    int opponentDeathCounter;
    int opponentShotAccuracy;
    int opponentGrenadeAccuracy;

    void Start()
    {

    }

    void Update()
    {
    }

    public void SendSummary()
    {
        playerDeathCounter = PlayerSummary.playerDeathCounter;
        playerShotAccuracy = PlayerSummary.playerShootCount*100/(PlayerSummary.playerShootCount + PlayerSummary.playerMissCount);
        playerGrenadeAccuracy = PlayerSummary.playerGrenadeHitCount*100/(PlayerSummary.playerGrenadeHitCount + PlayerSummary.playerGrenadeMissCount);
        opponentDeathCounter = PlayerSummary.opponentDeathCounter;
        opponentShotAccuracy = PlayerSummary.opponentShootCount*100/(PlayerSummary.opponentShootCount + PlayerSummary.opponentMissCount);
        opponentGrenadeAccuracy = PlayerSummary.opponentGrenadeHitCount*100/(PlayerSummary.opponentGrenadeHitCount + PlayerSummary.opponentGrenadeMissCount);
    }
}
