using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mqttStateController : MonoBehaviour
{
    public string nameController = "State Controller";
    public string tagOfTheMQTTReceiver = "MQTTState";
    public mqttReceiver _eventSender;

    // Initial Values
    public const float maxHealth = 100;    
    public int bulletCount = 6;
    public int grenadeCount = 2;
    public int shieldCount = 3;
    public int killCount = 0;  

    // Player 1

    // Player 2

    void Start()
    {
        _eventSender=GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttReceiver>();
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }
    // Separate data and link it to variables used in the other scripts and call the necessary functions
    private void OnMessageArrivedHandler(string newMsg)
    {
        Debug.Log("Event Fired. The message, from Object " +nameController+" is = " + newMsg);
        
    }
}
