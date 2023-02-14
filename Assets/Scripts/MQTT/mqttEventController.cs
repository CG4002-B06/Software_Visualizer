// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class mqttEventController : MonoBehaviour
// {
//     public string nameController = "Event Controller";
//     public string tagOfTheMQTTReceiver = "MQTTEvent";
//     public mqttEventReceiver _eventSender;

//     public TextMeshProUGUI simulatorMessage;

//     // Initial Values
//     public const float maxHealth = 100;    
//     public int bulletCount = 6;
//     public int grenadeCount = 2;
//     public int shieldCount = 3;
//     public int killCount = 0;  

//     // Player 1

//     // Player 2

//     void Start()
//     {
//         _eventSender=GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttEventReceiver>();
//         _eventSender.OnMessageArrived += OnMessageArrivedHandler;
//     }
//     // Separate data and link it to variables used in the other scripts and call the necessary functions
//     private void OnMessageArrivedHandler(string newMsg)
//     {
//         var gameEvent = JsonUtility.FromJson<mqttEvent>(newMsg);
//         Debug.Log(gameEvent);
//         Debug.Log(gameEvent.p1);
//         simulatorMessage.text = gameEvent.p1.action;
//     }
// }

// // [System.Serializable]
// // public class mqttEvent 
// // {
// //     public bool correction; 
// //     public players p1;
// //     public players p2;
// // }

// // [System.Serializable]
// // public class players 
// // {
// //     public string action;
// //     public bool shot;
// //     public string invalid_action;
// // }