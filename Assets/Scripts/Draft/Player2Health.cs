// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class Player2Health : MonoBehaviour
// {
//     public const int bulletDamage = 10;
//     public const int grenadeDamage = 30;
//     private float health;
//     private float shieldHealth;
//     private float lerpTimer;
//     public const float maxHealth = 100;
//     public const float maxShieldHealth = 30;
//     public float chipSpeed = 2f;
//     public int killCount = 0;
//     public int bulletCount = 6;
//     public int grenadeCount = 2;
//     public int shieldCount = 3;

//     public TextMeshProUGUI OPHP;
//     public TextMeshProUGUI P2KillCount;
//     public TextMeshProUGUI P1BulletCount;
//     public TextMeshProUGUI P1GrenadeCount;
//     public TextMeshProUGUI P2ShieldCount;
//     public TextMeshProUGUI WarningMessages;

//     public Image P2FrontHealthBar;
//     public Image P2BackHealthBar;
//     public Image P2FrontShieldBar;

//     public Button P1GrenadeButton;
//     public Button P1BulletButton;
//     public Button P2ShieldButton;
//     public Button P2ReloadButton;

//     // Start is called before the first frame update
//     void Start()
//     {
//         health = maxHealth;
//         OPHP.text = "100";
//         P2KillCount.text = "0";
//         P1BulletCount.text = "6";
//         P1GrenadeCount.text = "2";
//         P2ShieldCount.text = "3";
//         shieldHealth = 0;
//         WarningMessages.text = "";
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         health = Mathf.Clamp(health, 0, maxHealth);
//         shieldHealth = Mathf.Clamp(shieldHealth, 0, maxShieldHealth);

//         if(shieldHealth == 0)
//         {
//             UpdateHealthUI(health);
//         }

//         UpdateShieldUI(shieldHealth);

//         // Restore Health when player dies
//         if(health <= 0)
//         {
//             RestoreHealth();
//             killCount++;
//             P2KillCount.text = "" + killCount;
//             WarningMessages.text = "Player 2 is Dead";
//         }

//     }

//     public void UpdateHealthUI(float health)
//     {
//         Debug.Log(health);
//         float fillF = P2FrontHealthBar.fillAmount;
//         float fillB = P2BackHealthBar.fillAmount;
//         float hFraction = health / maxHealth;

//         if (fillB > hFraction)
//         {
//             P2FrontHealthBar.fillAmount = hFraction;
//             P2BackHealthBar.color = Color.red;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P2BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
//         }
//         if (fillF < hFraction)
//         {
//             P2BackHealthBar.color = Color.green;
//             P2BackHealthBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P2FrontHealthBar.fillAmount = Mathf.Lerp(fillF, P2BackHealthBar.fillAmount, percentComplete);
//         }
//     }

//     public void UpdateShieldUI(float shieldHealth)
//     {
//         Debug.Log(shieldHealth);
//         P2FrontShieldBar.fillAmount = shieldHealth;
//         float fillF = P2FrontShieldBar.fillAmount;
//         float hFraction = shieldHealth / maxShieldHealth;
        
//         if (fillF > hFraction)
//         {
//             P2FrontShieldBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P2FrontShieldBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
//         }
//     }

//     public void ActivateShield()
//     {
//         if(shieldCount == 0)
//         {
//             return;
//         }

//         if(!IsActiveShield())
//         {
//             shieldCount -= 1;
//             P2ShieldCount.text = "" + shieldCount;
//             shieldHealth = maxShieldHealth;
//             lerpTimer = 0f;
//         }
//     }

//     public void BulletDamage()
//     {
//         if(bulletCount == 0)
//         { 
//             WarningMessages.text = "Out of Ammo! RELOAD";
//             return;
//         }

//         if(shieldHealth == bulletDamage)
//         {
//             shieldHealth -= bulletDamage;
//             OPHP.text = "" + health;
//             bulletCount -= 1;
//             P1BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//             // Minus 1 shield count
//         }
//         else if(shieldHealth < bulletDamage)
//         {
//             health -= bulletDamage - shieldHealth; 
//             shieldHealth = 0;
//             OPHP.text = "" + health;
//             OPHP.text = "" + health;
//             bulletCount -= 1;
//             P1BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//         else if(shieldHealth > bulletDamage)
//         {
//             shieldHealth -= bulletDamage;
//             OPHP.text = "" + health;
//             bulletCount -= 1;
//             P1BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//         else 
//         {
//             health -= bulletDamage;
//             OPHP.text = "" + health;
//             bulletCount -= 1;
//             P1BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//     }

//     public void GrenadeDamage()
//     {
//         if(grenadeCount == 0)
//         {
//             WarningMessages.text = "Out of Grenade!";
//             return;
//         }

//         if(shieldHealth == grenadeDamage)
//         {
//             health -= grenadeDamage - shieldHealth; 
//             shieldHealth = 0;
//             OPHP.text = "" + health;
//             grenadeCount -= 1;
//             P1GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
//         }
//         else if(shieldHealth < grenadeDamage)
//         {
//             health -= grenadeDamage - shieldHealth;
//             shieldHealth = 0;
//             OPHP.text = "" + health;
//             grenadeCount -= 1;
//             P1GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
//         } 
//         else 
//         {
//             health -= grenadeDamage;
//             if(health <= 0)
//             {
//                 RestoreHealth();
//             }
//             OPHP.text = "" + health;
//             grenadeCount -= 1;
//             P1GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
//         }
//     }

//     public void RestoreHealth()
//     {
//         health = maxHealth;
//         OPHP.text = "" + health;
//         lerpTimer = 0f;
//     }

//     public void ReloadBullets()
//     {
//         if(bulletCount == 0)
//         {
//             bulletCount = 6;
//             P1BulletCount.text = "" + bulletCount;
//             WarningMessages.text = "RELOAD SUCCESSFUL";
//         }
//         else
//         { 
//             WarningMessages.text = "Ammo still left";
//             return;
//         }
//     }

//     public bool IsActiveShield()
//     {
//         if(shieldHealth > 0)
//         {
//             return true;
//         }

//         return false;
//     }
// }
