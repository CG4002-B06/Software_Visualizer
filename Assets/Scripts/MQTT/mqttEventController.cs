using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mqttEventController : MonoBehaviour
{
    public string nameController = "Event Controller";
    public string tagOfTheMQTTReceiver = "MQTTEvent";
    public mqttReceiver _eventSender;

    public TextMeshProUGUI connectionMessage;
    public GameObject blackScreen;
    public SoundEffects soundEffect;
    private int packetId = 0;

    void Start()
    {
        _eventSender=GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttReceiver>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }

    public void ShowMessage()
    {
        connectionMessage.text = "";
    }

    // Separate data and link it to variables used in the other scripts and call the necessary functions
    private void OnMessageArrivedHandler(string newMsg)
    {
        var gameEvent = JsonUtility.FromJson<MqttEvent>(newMsg);
        packetId = 1;

        if(PlayerSelection.PlayerIndex == 1)
        {
            string player = gameEvent.p1;
            string opponent = gameEvent.p2;
            Debug.Log("Player 1 and Opponent Set");
            DisplayPlayerEvent(player, opponent);
        } 
        else if(PlayerSelection.PlayerIndex == 2)
        {
            string player = gameEvent.p2;
            string opponent = gameEvent.p1;
            Debug.Log("Player 2 and Opponent Set");
            DisplayPlayerEvent(player, opponent);
        }

        void DisplayPlayerEvent(string player, string opponent)
        {
            Debug.Log(player);
            Debug.Log(opponent);

            if(player == "DONT MOVE GLOVE! \n SENSORS ARE INITIALISING..." && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayDontMoveGloveSound();
                blackScreen.SetActive(true);
                connectionMessage.text = "" + player;
                connectionMessage.color = Color.white;
            }

            if(player == "SENSORS HAVE BEEN INITIALISED \n ENJOY SHOOTING!" && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayBeginGameSound();
                blackScreen.SetActive(false);
                connectionMessage.text = "" + player;
                connectionMessage.color = Color.green;
                Invoke("ShowMessage" , 3f);
            }

            if(player == "ACTION UNDETECTED! \n REDO ACTION" && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayRedoActionSound();
                blackScreen.SetActive(false);
                if(player == gameEvent.p1)
                {
                    connectionMessage.text = "P1 " + player;
                }
                if(player == gameEvent.p2)
                {
                    connectionMessage.text = "P2 " + player;
                }
                
                connectionMessage.color = Color.white;
                Invoke("ShowMessage" , 5.5f);
            }

            if(player == "CONNECTION RE-ESTABLISHED \n REDO ACTION" && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayConnectionReestablishedSound();
                blackScreen.SetActive(false);
                if(player == gameEvent.p1)
                {
                    connectionMessage.text = "P1 " + player;
                }
                if(player == gameEvent.p2)
                {
                    connectionMessage.text = "P2 " + player;
                }
                
                connectionMessage.color = Color.green;
                Invoke("ShowMessage" , 5.5f);
            }

            if(player == "CONNECTION LOST \n GET CLOSER TO THE RELAY NODE" && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayConnectionLostSound();
                blackScreen.SetActive(true);
                if(player == gameEvent.p1)
                {
                    connectionMessage.text = "P1 " + player;
                }
                if(player == gameEvent.p2)
                {
                    connectionMessage.text = "P2 " + player;
                }
                
                connectionMessage.color = Color.red;
            }

            if(player == "CONNECTION RE-ESTABLISHED" && packetId != 0)
            {
                packetId = 0;
                soundEffect.PlayConnectionReestablishedSound();
                blackScreen.SetActive(false);
                if(player == gameEvent.p1)
                {
                    connectionMessage.text = "P1 " + player;
                }
                if(player == gameEvent.p2)
                {
                    connectionMessage.text = "P2 " + player;
                }
                
                connectionMessage.color = Color.green;
                Invoke("ShowMessage" , 5f);
            }          
        }
    }
}

[System.Serializable]
public class MqttEvent
{
    public string p1;
    public string p2;
}