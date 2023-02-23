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
        var p1 = gameState.p1;
        var p2 = gameState.p2;
        
        if(gameState.correction == false) // No need correction. Show action and update all parameters
        {
            // Display warning emssage saying that game is updating 
            // soundEffect.playStatusUpdatingSound();
            // message.SetWarning("Game State Updating... \n Please wait a moment");
                        
            // Correct player status and Update UI
    
            float p1Health = Mathf.Clamp(p1.hp, 0, maxHealth);
            float p1ShieldHealth = Mathf.Clamp(p1.shield_health, 0, maxShieldHealth);
            player1.UpdateHealth(p1Health);
            player1.UpdateShieldHealth(p1.shield_health);

            float p2Health = Mathf.Clamp(p2.hp, 0, maxHealth);
            float p2ShieldHealth = Mathf.Clamp(p2.shield_health, 0, maxShieldHealth);
            player2.UpdateHealth(p2.hp);
            player2.UpdateShieldHealth(p2.shield_health);

            // Player 1
            player1.UpdateBulletCount(p1.bullets);
            player1.UpdateGrenadeCount(p1.grenades);
            player1.UpdateShieldCount(p1.num_shield);
            player1.deathCounter.UpdatePlayerDeathCount(p1.num_deaths);

            // Player 2   
            player2.UpdateBulletCount(p2.bullets);
            player2.UpdateGrenadeCount(p2.grenades);
            player2.UpdateShieldCount(p2.num_shield);
            player2.deathCounter.UpdatePlayerDeathCount(p2.num_deaths);
        }
        
        // Display action (not necessary)
        simulatorMessage.text = "" + p1.action;

        // Perform actions only when there are no warning messages
        if(p1.invalid == null)
        {      
            if(p1.action == "grenade")
            {
                if(player2.ReturnTargetQuery())
                {
                    player1.Grenade();
                    // soundEffect.playGrenadeThrowSound();
                    // // soundEffect.Invoke("playGrenadeExplosionSound", 2f);
                    // Debug.Log(player2.ReturnTargetQuery());
                    // soundEffect.playHitSound();

                    Output output = new Output();
                    output.p1 = true;
                    string message = JsonUtility.ToJson(output);
                    Debug.Log(message);
                    _eventSender.SetMessage(message);
                    _eventSender.Publish();
                }
                else {
                    player1.Grenade();
                    // soundEffect.playMissSound();
                    // soundEffect.playGrenadeThrowSound();

                    Output output = new Output();
                    output.p1 = false;
                    string message = JsonUtility.ToJson(output);
                    Debug.Log(message);
                    _eventSender.SetMessage(message);
                    _eventSender.Publish();
                }
            }
            if(p1.action == "shoot") // Need to check if hit
            {
                if(p1.shot == true)
                {
                    player1.Bullet();
                    soundEffect.playBulletShootSound();
                    soundEffect.playHitSound();
                }
                else if(p1.shot == false)
                {
                    soundEffect.playMissSound();
                }         
            }
            else if(p2.action == "shoot")
            {
                if(p2.shot == true)
                {
                    player2.Bullet();
                    // Probably add player grunting sound
                    if(p1.shield_health >= 0)
                    {
                        soundEffect.playsShieldCooldownSound();
                    }
                }
                else if(p2.shot == false)
                {
                    //  Opponent has missed shooting you
                }
            }
            else if(p1.action == "shield")
            {
                player1.ActivateShield();
                soundEffect.playShieldActivationSound();
                shieldTimer.SetTime(p1.shield_time);
            }
            else if(p2.action == "shield")
            {
                player2.ActivateShield();
                shieldTimer.SetTime(p2.shield_time);
            }
            else if(p1.action == "reload")
            {
                player1.ReloadBullets();
                soundEffect.playReloadSound();
            }
            else if(p2.action == "reload")
            {
                player2.ReloadBullets();
            }
            else if(p1.action  == "logout" || p2.action == "logout")
            {
                player1.Logout();
                player2.Logout();
            }
        }
        else 
        {
            // Display Warning message
            message.SetWarning(p1.invalid);
        }
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
    public int num_deaths;
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