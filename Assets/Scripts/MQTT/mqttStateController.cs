using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mqttStateController : MonoBehaviour
{
    public string nameController = "State Controller";
    public string tagOfTheMQTTReceiver = "MQTTState";
    public mqttReceiver _eventSender;
    public TextMeshProUGUI simulatorMessage;
    public GameObject godModeButton;
    public GameObject exitGodModeButton;
    public bool godMode;

    // Initial Values
    public const float maxHealth = 100;    
    public const float maxShieldHealth = 30;

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

    public void OnClickGodMode()
    {
        godMode = true;
        simulatorMessage.text = "GOD MODE \n ACTIVATED";
        exitGodModeButton.SetActive(true);
        godModeButton.SetActive(false);
    }

    public void OnExitGodMode()
    {
        godMode = false;
        simulatorMessage.text = "";
        exitGodModeButton.SetActive(false);
        godModeButton.SetActive(true);
    }

    // Separate data and link it to variables used in the other scripts and call the necessary functions
    private void OnMessageArrivedHandler(string newMsg)
    {
        var gameState = JsonUtility.FromJson<MqttState>(newMsg);

        PlayerNo player = gameState.p1;
        PlayerNo opponent = gameState.p2;

        if(PlayerSelection.PlayerIndex == 1)
        {
            player = gameState.p1;
            opponent = gameState.p2;
            PlayerSummary.playerDeathCounter = player.num_deaths;
            PlayerSummary.opponentDeathCounter = opponent.num_deaths;

            Debug.Log("Player 1 and Opponent Set");
            DisplayPlayerAction(player, opponent);
        } 
        else if (PlayerSelection.PlayerIndex == 2)
        {
            player = gameState.p2;
            opponent = gameState.p1;
            PlayerSummary.playerDeathCounter = player.num_deaths;
            PlayerSummary.opponentDeathCounter = opponent.num_deaths;

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
            if(player.invalid == null || godMode == true)
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
                            PlayerSummary.playerGrenadeHitCount += 1;
                        }
                        else {
                            soundEffect.PlayMissSound();
                            output.p1 = false;
                            PlayerSummary.playerGrenadeMissCount += 1;
                        }
                        
                        Debug.Log(output.p1);
                        _eventSender.SetMessage(JsonUtility.ToJson(output));
                        _eventSender.Publish();
                        return;
                    }
                } 
                if(opponent.action == "grenade")
                {
                    Debug.Log("Opponent Grenade Throwing Works!");
                    player2.Grenade();
                    PlayerSummary.opponentGrenadeHitCount += 1;
                }   
                if(player.action == "shoot") 
                {
                    player1.Bullet();

                    if(player.isHit == true)
                    {
                        Debug.Log("isHit is true");
                        shootEffect.ShootBullet();
                        soundEffect.PlayHitSound();
                        PlayerSummary.playerShootCount += 1;
                    }
                    else
                    {
                        Debug.Log("isHit is false");
                        soundEffect.PlayMissSound();
                        PlayerSummary.playerMissCount += 1;
                    }        
                }
                if(opponent.action == "shoot")
                {
                    Debug.Log("Opponent Shooting Works!");
                    if(opponent.isHit == true)
                    {
                        player2.Bullet();
                        PlayerSummary.opponentShootCount += 1;
                    }
                    else
                    {
                        PlayerSummary.opponentMissCount += 1;
                    }
                }
                if(player.action == "shield")
                {
                    player1.ActivateShield();
                    shieldTimer.SetTime(player.shield_time);
                }
                if(opponent.action == "shield")
                {
                    Debug.Log("Opponent Shield Works!");
                    player2.ActivateShield();
                }
                if(player.action == "reload")
                {
                    player1.ReloadBullets();
                }
                if(opponent.action == "reload")
                {
                    Debug.Log("Opponent Reload Works!");
                    player2.ReloadBullets();
                }
                if(player.action  == "logout" || opponent.action == "logout")
                {
                    // SceneManager.LoadScene("LogoutScene");
                    player1.InvokeLogout();
                    player2.InvokeLogout();
                }
                
                if(godMode == false)
                {
                    updatePlayerStatus(player, opponent);
                }
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

        if(player1_object.shield_time > 0)
        {
            player1.ActivateShield();
            shieldTimer.SetTime(player1_object.shield_time);
        }

        if(player1_object.action == "logout")
        {
            SceneManager.LoadScene("LogoutScene");
        }
    }
}

[System.Serializable]
public class MqttState 
{
    public bool correction; 
    public PlayerNo p1 = new PlayerNo();
    public PlayerNo p2 = new PlayerNo();
}

[System.Serializable]
public class PlayerNo 
{
    public float hp;
    public string action;
    public int bullets = 6;
    public int grenades = 2;
    public float shield_time = 0;
    public float shield_health = 0;
    public int num_deaths = -1;
    public int num_shield = 3;
    public bool isHit;
    public string invalid;
}

[System.Serializable]
public class Output
{
    public bool p1;
    public bool p2;
}