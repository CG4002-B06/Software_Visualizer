using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mqttStateController : MonoBehaviour
{
    public string nameController = "State Controller";
    public string tagOfTheMQTTReceiver = "MQTTState";
    public mqttReceiver _eventSender;

    public TextMeshProUGUI simulatorMessage;
    public TextMeshProUGUI connectionMessage;

    // Initial Values
    public const float maxHealth = 100;    
    public const float maxShieldHealth = 30;
    public int bulletCount = 6;
    public int grenadeCount = 2;
    public int shieldCount = 3;
    public int killCount = 0;  

    public Player player1;
    public Player player2;
    public SoundEffects soundEffect;
    public MessageTimer message;
    public ShieldTimer shieldTimer;
    public Summary summary;
    public BulletShooter shootEffect;

    void Start()
    {
        _eventSender=GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttReceiver>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }

    // Separate data and link it to variables used in the other scripts and call the necessary functions
    private void OnMessageArrivedHandler(string newMsg)
    {
        if(newMsg == null)
        {
            connectionMessage.text = "CONNECTION BROKEN!";
        }

        var gameState = JsonUtility.FromJson<MqttState>(newMsg);

        PlayerNo player = gameState.p1;
        PlayerNo opponent = gameState.p2;

        // Information for summary table
        // int playerMissCount = 0;
        // int playerShootCount = 0;
        // int playerGrenadeMissCount = 0;
        // int playerGrenadeHitCount = 0;
        // int playerDeathCounter = player.num_deaths;
        // int playerShootAcc = playerShootCount*100/(playerShootCount + playerMissCount);
        // int playerGrenadeAcc = playerGrenadeHitCount*100/(playerGrenadeHitCount + playerGrenadeMissCount);

        // int opponentMissCount = 0;
        // int opponentShootCount = 0;
        // int opponentGrenadeMissCount = 0;
        // int opponentGrenadeHitCount = 0;
        // int opponentDeathCounter = opponent.num_deaths; 
        // int opponentShootAcc = opponentShootCount*100/(opponentShootCount + opponentMissCount);
        // int opponentGrenadeAcc = opponentGrenadeHitCount*100/(opponentGrenadeHitCount + opponentGrenadeMissCount);

        if(PlayerSelection.PlayerIndex == 1)
        {
            player = gameState.p1;
            opponent = gameState.p2;
            Debug.Log("Player 1 and Opponent Set");
            DisplayPlayerAction(player, opponent);
            // summary.SendSummary(playerDeathCounter, playerShootAcc, playerGrenadeAcc, opponentDeathCounter, opponentShootAcc, opponentGrenadeAcc);
        } 
        else if (PlayerSelection.PlayerIndex == 2)
        {
            player = gameState.p2;
            opponent = gameState.p1;
            Debug.Log("Player 2 and Opponent Set");
            DisplayPlayerAction(player, opponent);
        }
   
        void DisplayPlayerAction(PlayerNo player, PlayerNo opponent)
        {
            // These all show the correct numbers recieved from the packet
            Debug.Log(player.bullets);
            Debug.Log(player.grenades);
            Debug.Log(player.num_shield);

            // this is a correct packet. should update player status
            if(gameState.correction)
            {
                // soundEffect.PlayStatusUpdatingSound();
                message.SetWarning("Game State Updating... \n Please wait a moment");
                updatePlayerStatus(player, opponent);
                return;
            }
            
            // Display action name
            simulatorMessage.text = "" + player.action;

            // Perform actions only when there are no warning messages
            if(player.invalid == null)
            {     
                if(player.action == "grenade")
                {
                    if (player.num_deaths < 0)
                    {
                        Output output = new Output();
                        player1.Grenade();

                        if(player2.ReturnTargetQuery())
                        {
                            soundEffect.InvokePlayGrenadeExplosionSound();
                            soundEffect.PlayHitSound();
                            output.p1 = true;
                            // playerGrenadeHitCount += 1;
                        }
                        else {
                            soundEffect.PlayMissSound();
                            output.p1 = false;
                            // playerGrenadeMissCount += 1;
                        }
                        
                        Debug.Log(output.p1);
                        _eventSender.SetMessage(JsonUtility.ToJson(output));
                        _eventSender.Publish();
                        return;
                    }
                } 
                else if(opponent.action == "grenade")
                {
                    player2.Grenade();
                    // opponentGrenadeHitCount += 1;
                }   
                else if(player.action == "shoot") 
                {
                    player1.Bullet();

                    if(player.isHit == true)
                    {
                        Debug.Log("isHit is true");
                        shootEffect.ShootBullet();
                        soundEffect.PlayBulletShootSound();
                        soundEffect.PlayHitSound();
                        // playerShootCount += 1;
                    }
                    else
                    {
                        Debug.Log("isHit is false");
                        soundEffect.PlayMissSound();
                        // playerMissCount += 1;
                    }        
                }
                else if(opponent.action == "shoot")
                {
                    if(opponent.isHit == true)
                    {
                        player2.Bullet();
                        // opponentShootCount += 1;
                    }
                    else
                    {
                        // opponentMissCount += 1;
                    }
                }
                else if(player.action == "shield")
                {
                    player1.ActivateShield();
                    shieldTimer.SetTime(player.shield_time);
                }
                else if(opponent.action == "shield")
                {
                    player2.ActivateShield();
                }
                else if(player.action == "reload")
                {
                    player1.ReloadBullets();
                }
                else if(opponent.action == "reload")
                {
                    player2.ReloadBullets();
                }
                else if(player.action  == "logout" || opponent.action == "logout")
                {
                    player1.InvokeLogout();
                    player2.InvokeLogout();
                }
                
                updatePlayerStatus(player, opponent);
            }
            else 
            {
                // Display Warning message
                message.SetWarning(player.invalid);
            }
        }
    }

    private void updatePlayerStatus(PlayerNo player1_object, PlayerNo player2_object)
    {
        float p1Health = Mathf.Clamp(player1_object.hp, 0, maxHealth);
        float p1ShieldHealth = Mathf.Clamp(player1_object.shield_health, 0, maxShieldHealth);
        player1.UpdateHealth(p1Health);
        player1.UpdateShieldHealth(p1ShieldHealth);

        float p2Health = Mathf.Clamp(player2_object.hp, 0, maxHealth);
        float p2ShieldHealth = Mathf.Clamp(player2_object.shield_health, 0, maxShieldHealth);
        player2.UpdateHealth(p2Health);
        player2.UpdateShieldHealth(p2ShieldHealth);
        
        // Player 1
        player1.UpdateBulletCount(player1_object.bullets);
        player1.UpdateGrenadeCount(player1_object.grenades);
        player1.UpdateShieldCount(player1_object.num_shield);
        player1.deathCounter.UpdatePlayerDeathCount(player1_object.num_deaths);
        
        // Player 2   
        player2.UpdateBulletCount(player2_object.bullets);
        player2.UpdateGrenadeCount(player2_object.grenades);
        player2.UpdateShieldCount(player2_object.num_shield);
        player2.deathCounter.UpdatePlayerDeathCount(player2_object.num_deaths);
        
        // Assign Color and Size to death counters accordingly
        if(player1_object.num_deaths > player2_object.num_deaths)
        {
            player1.deathCounter.SetColorAndSize(Color.red, 22);
        } 
        else if (player1_object.num_deaths < player2_object.num_deaths)
        {
            player2.deathCounter.SetColorAndSize(Color.red, 22);
        } 
        else
        {
            player1.deathCounter.SetColorAndSize(Color.white, 24);
            player2.deathCounter.SetColorAndSize(Color.white, 24);
        }
    }
}

[System.Serializable]
public class MqttState 
{
    public bool correction; 
    public PlayerNo p1;
    public PlayerNo p2;
}

[System.Serializable]
public class PlayerNo 
{
    public float hp;
    public string action;
    public int bullets;
    public int grenades;
    public float shield_time;
    public float shield_health;
    public int num_deaths = -1;
    public int num_shield;
    public bool isHit;
    public string invalid;
}

[System.Serializable]
public class Output
{
    public bool p1;
    public bool p2;
}