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

    void Start()
    {
        _eventSender=GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttReceiver>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }

    // Separate data and link it to variables used in the other scripts and call the necessary functions
    private void OnMessageArrivedHandler(string newMsg)
    {
        var gameState = JsonUtility.FromJson<mqttState>(newMsg);
        var player1_object = gameState.p1;
        var player2_object = gameState.p2;
        
        // this is a correct packet. should update player status
        if(gameState.correction) // No need correction. Show action and update all parameters
        {
            // Display warning emssage saying that game is updating 
            // soundEffect.playStatusUpdatingSound();
            message.SetWarning("Game State Updating... \n Please wait a moment");
            updatePlayerStatus(player1_object, player2_object);
            return;
        }
        
        // Display action (not necessary)
        simulatorMessage.text = "" + gameState.p1.action;

        // Perform actions only when there are no warning messages
        if(gameState.p1.invalid == null)
        {      
            if(gameState.p1.action == "grenade")
            {
                if (gameState.p1.num_deaths < 0)
                {
                    Output output = new Output();
                    player1.Grenade();
                    // soundEffect.playGrenadeThrowSound();
                    if(player2.ReturnTargetQuery())
                    {
                        // soundEffect.Invoke("playGrenadeExplosionSound", 2f);
                        // soundEffect.playHitSound();
                        output.p1 = true;
                    }
                    else {
                        // soundEffect.playMissSound();
                        output.p1 = false;
                    }
                    Debug.Log(output.p1);
                    _eventSender.SetMessage(JsonUtility.ToJson(output));
                    _eventSender.Publish();
                    return;
                }
            }    
            else if(gameState.p1.action == "shoot") // Need to check if hit
            {
                if(gameState.p1.shot == true)
                {
                    player1.Bullet();
                    soundEffect.playBulletShootSound();
                    soundEffect.playHitSound();
                }
                else if(gameState.p1.shot == false)
                {
                    soundEffect.playMissSound();
                }         
            }
            else if(gameState.p2.action == "shoot")
            {
                if(gameState.p2.shot == true)
                {
                    player2.Bullet();
                    // Probably add player grunting sound
                    if(gameState.p1.shield_health >= 0)
                    {
                        soundEffect.playsShieldCooldownSound();
                    }
                }
                else if(gameState.p2.shot == false)
                {
                    //  Opponent has missed shooting you
                }
            }
            else if(gameState.p1.action == "shield")
            {
                player1.ActivateShield();
                soundEffect.playShieldActivationSound();
                shieldTimer.SetTime(gameState.p1.shield_time);
            }
            else if(gameState.p2.action == "shield")
            {
                player2.ActivateShield();
                shieldTimer.SetTime(gameState.p2.shield_time);
            }
            else if(gameState.p1.action == "reload")
            {
                player1.ReloadBullets();
                soundEffect.playReloadSound();
            }
            else if(gameState.p2.action == "reload")
            {
                player2.ReloadBullets();
            }
            else if(gameState.p1.action  == "logout" || gameState.p2.action == "logout")
            {
                player1.Logout();
                player2.Logout();
            }
            updatePlayerStatus(player1_object, player2_object);
        }
        
        else 
        {
            // Display Warning message
            message.SetWarning(gameState.p1.invalid);
        }
    }

    private void updatePlayerStatus(player player1_object, player player2_object)
    {
            float p1Health = Mathf.Clamp(player1_object.hp, 0, maxHealth);
            float p1ShieldHealth = Mathf.Clamp(player1_object.shield_health, 0, maxShieldHealth);
            player1.UpdateHealth(p1Health);
            player1.UpdateShieldHealth(player1_object.shield_health);

            float p2Health = Mathf.Clamp(player2_object.hp, 0, maxHealth);
            float p2ShieldHealth = Mathf.Clamp(player2_object.shield_health, 0, maxShieldHealth);
            player2.UpdateHealth(p2Health);
            player2.UpdateShieldHealth(player2_object.shield_health);

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
    }
}

[System.Serializable]
public class mqttState 
{
    public bool correction; 
    public player p1;
    public player p2;
}

[System.Serializable]
public class player 
{
    public float hp;
    public string action;
    public int bullets;
    public int grenades;
    public float shield_time;
    public float shield_health;
    public int num_deaths = -1;
    public int num_shield;
    public bool shot;
    public string invalid;
}

[System.Serializable]
public class Output
{
    public bool p1;
    public bool p2;
}