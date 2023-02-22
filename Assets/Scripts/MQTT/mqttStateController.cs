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

        Debug.Log(gameState.p1.action);
        Debug.Log(gameState.p1.hp);
        Debug.Log(gameState.p1.bullets);
        
        if(gameState.correction == false) // No need correction. Show action and update all parameters
        {
            // Display warning emssage saying that game is updating 
            // soundEffect.playStatusUpdatingSound();
            // message.SetWarning("Game State Updating... \n Please wait a moment");
                        
            // Correct player status and Update UI
    
            float p1Health = Mathf.Clamp(gameState.p1.hp, 0, maxHealth);
            float p1ShieldHealth = Mathf.Clamp(gameState.p1.shield_health, 0, maxShieldHealth);
            player1.UpdateHealth(p1Health);
            player1.UpdateShieldHealth(gameState.p1.shield_health);

            float p2Health = Mathf.Clamp(gameState.p2.hp, 0, maxHealth);
            float p2ShieldHealth = Mathf.Clamp(gameState.p2.shield_health, 0, maxShieldHealth);
            player2.UpdateHealth(gameState.p2.hp);
            player2.UpdateShieldHealth(gameState.p2.shield_health);

            // Player 1
            player1.UpdateBulletCount(gameState.p1.bullets);
            player1.UpdateGrenadeCount(gameState.p1.grenades);
            player1.UpdateShieldCount(gameState.p1.num_shield);
            player1.deathCounter.UpdatePlayerDeathCount(gameState.p1.num_deaths);
            
            // Player 2   
            player2.UpdateBulletCount(gameState.p2.bullets);
            player2.UpdateGrenadeCount(gameState.p2.grenades);
            player2.UpdateShieldCount(gameState.p2.num_shield);
            player2.deathCounter.UpdatePlayerDeathCount(gameState.p2.num_deaths);
        }
        
        // Display action (not necessary)
        simulatorMessage.text = "" + gameState.p1.action;

        // Display Warning message
        message.SetWarning(gameState.p1.invalid);

        if(gameState.p1.action == "grenade")
        {
            player1.Grenade();
            soundEffect.playGrenadeThrowSound();
            soundEffect.Invoke("playGrenadeExplosionSound", 2f);
            
            if(player2.ReturnTargetQuery())
            {
                soundEffect.playHitSound();
                Output output = new Output();
                output.p1 = true;
                string message = JsonUtility.ToJson(output);
                Debug.Log(message);

                _eventSender.SetMessage(message);
                _eventSender.Publish();
            }
            else {
                soundEffect.playMissSound();
                Output output = new Output();
                output.p1 = false;
                string message = JsonUtility.ToJson(output);
                _eventSender.SetMessage(message);
                _eventSender.Publish();
            }
        }
        else if(gameState.p2.action == "grenade")
        {
            player2.Grenade();
            soundEffect.Invoke("playGrenadeIncomingSound", 2f);

            if(player1.ReturnTargetQuery())
            {
                Output output = new Output();
                output.p2 = true;
                string message = JsonUtility.ToJson(output);
                _eventSender.SetMessage(message);
                _eventSender.Publish();
            }
            else {
                Output output = new Output();
                output.p2 = false;
                string message = JsonUtility.ToJson(output);
                _eventSender.SetMessage(message);
                _eventSender.Publish();
            }
        }
        else if(gameState.p1.action == "shoot") // Need to check if hit
        {
            if(gameState.p1.isHit == true)
            {
                player2.Bullet();
                soundEffect.playBulletShootSound();
                soundEffect.playHitSound();
            }
            else if(gameState.p1.isHit == false)
            {
                player2.Bullet();
                soundEffect.playMissSound();
            }         
        }
        else if(gameState.p2.action == "shoot")
        {
            if(gameState.p2.isHit == true)
            {
                player1.Bullet();
                // Probably add player grunting sound
                if(gameState.p1.shield_health >= 0)
                {
                    soundEffect.playsShieldCooldownSound();
                }
            }
            else if(gameState.p2.isHit == false)
            {
                //  Opponent has missed shooting you
            }
        }
        else if(gameState.p1.action == "shield")
        {
            player1.ActivateShield();
            soundEffect.playShieldActivationSound();
            
            // Set Shield Timer
            shieldTimer.SetTime(gameState.p1.shield_time);
        }
        else if(gameState.p2.action == "shield")
        {
            player2.ActivateShield();

            // Set Shield Timer
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
    public int grenades;
    public int num_deaths;
    public int num_shield;
    public int bullets;
    public float shield_health;
    public string action;
    public float shield_time;
    public bool shot;
    public string invalid;
    public bool isHit;
}

[System.Serializable]
public class Output
{
    public bool p1;
    public bool p2;
}