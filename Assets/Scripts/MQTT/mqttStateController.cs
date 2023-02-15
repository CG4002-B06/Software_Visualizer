using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mqttStateController : MonoBehaviour
{
    public string nameController = "State Controller";
    public string tagOfTheMQTTReceiver = "MQTTState";
    public mqttReceiver _eventSender;
    public mqttSendToUltra mqttSend;

    public TextMeshProUGUI simulatorMessage;
    public TextMeshProUGUI P1BulletCount;
    public TextMeshProUGUI P1GrenadeCount;
    public TextMeshProUGUI P1ShieldCount;
    public TextMeshProUGUI P1DeathCount;
    
    public TextMeshProUGUI P2BulletCount;
    public TextMeshProUGUI P2GrenadeCount;
    public TextMeshProUGUI P2ShieldCount;
    public TextMeshProUGUI P2DeathCount;

    // Initial Values
    public const float maxHealth = 100;    
    public const float maxShieldHealth = 30;
    public int bulletCount = 6;
    public int grenadeCount = 2;
    public int shieldCount = 3;
    public int killCount = 0;  

    public Player player1;
    public Player player2;

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
        // Warning Message
        // Debug.Log(gameState.p1.invalid_action);
        
        simulatorMessage.text = "" + gameState.p1.action; 

        if(gameState.correction == false)
        {
            if(gameState.p1.action == "grenade")
            {
                player2.Grenade();
                Debug.Log(player1.ReturnTargetQuery());
                if(player1.ReturnTargetQuery())
                {
                    Output output = new Output();
                    output.p1 = true;
                    string message = JsonUtility.ToJson(output);
                    Debug.Log(message);

                    _eventSender.SetMessage(message);
                    _eventSender.Publish();
                }
                else {
                    Output output = new Output();
                    output.p1 = false;
                    string message = JsonUtility.ToJson(output);
                    _eventSender.SetMessage(message);
                    _eventSender.Publish();
                }
            }
            if(gameState.p2.action == "grenade")
            {
                player1.Grenade();
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
            if(gameState.p1.action == "shoot" && player1.ReturnTargetQuery())
            {
                player2.Bullet();
            }
            else if(gameState.p2.action == "shoot")
            {
                player1.Bullet();
            }
            else if(gameState.p1.action == "shield")
            {
                player1.ActivateShield();
            }
            else if(gameState.p2.action == "shield")
            {
                player2.ActivateShield();
            }
            else if(gameState.p1.action == "reload")
            {
                player1.ReloadBullets();
            }
            else if(gameState.p2.action == "reload")
            {
                player2.ReloadBullets();
            }
        }

        // When a corrected packet (normal != eval packet) is sent to the visualiser

        // Correct player status and Update UI
        else if(gameState.correction == true)
        {
            gameState.p1.hp = Mathf.Clamp(gameState.p1.hp, 0, maxHealth);
            gameState.p1.shield_health = Mathf.Clamp(gameState.p1.shield_health, 0, maxShieldHealth);
            player1.UpdateHealth(gameState.p1.hp);
            // player1.UpdateHealthUI(gameState.p1.hp);
            player1.UpdateShieldHealth(gameState.p1.shield_health);

            gameState.p2.hp = Mathf.Clamp(gameState.p2.hp, 0, maxHealth);
            gameState.p2.shield_health = Mathf.Clamp(gameState.p2.shield_health, 0, maxShieldHealth);
            player2.UpdateHealth(gameState.p2.hp);
            // player2.UpdateHealthUI(gameState.p2.hp);
            player2.UpdateShieldHealth(gameState.p2.shield_health);

            // Player 1
            player1.UpdateBulletCount(gameState.p2.bullet);
            // player2.BulletCount.text = "" + gameState.p1.bullet;

            player1.UpdateGrenadeCount(gameState.p2.grenade);
            // player2.GrenadeCount.text = "" + gameState.p1.grenade;

            player1.UpdateShieldCount(gameState.p1.num_of_shield);
            // player1.ShieldCount.text = "" + gameState.p1.num_of_shield;

            player1.deathCounter.UpdatePlayerDeathCount(gameState.p1.num_of_death);
            
            // Player 2   
            player2.UpdateBulletCount(gameState.p1.bullet);
            // player1.BulletCount.text = "" + gameState.p2.bullet;

            player2.UpdateGrenadeCount(gameState.p1.grenade);
            // player1.GrenadeCount.text = "" + gameState.p2.grenade;

            player2.UpdateShieldCount(gameState.p2.num_of_shield);
            // player2.ShieldCount.text = "" + gameState.p2.num_of_shield;

            player2.deathCounter.UpdatePlayerDeathCount(gameState.p2.num_of_death);
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
    public int grenade;
    public int num_of_death;
    public int num_of_shield;
    public int bullet;
    public float shield_health;
    public string action;
    public bool shot;
    public string invalid_action;
}

[System.Serializable]
public class Output
{
    public bool p1;
    public bool p2;
}


