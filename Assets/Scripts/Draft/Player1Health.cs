// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class Player1Health : MonoBehaviour
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
//     float currentTime;
//     public float startingTime = 1f;

//     public TextMeshProUGUI UPHP;
//     public TextMeshProUGUI P1KillCount;
//     public TextMeshProUGUI P2BulletCount;
//     public TextMeshProUGUI P2GrenadeCount;
//     public TextMeshProUGUI P1ShieldCount;
//     public TextMeshProUGUI WarningMessages;
//     public GameObject TextHolder;

//     public Image P1FrontHealthBar;
//     public Image P1BackHealthBar;
//     public Image P1FrontShieldBar;
//     public Image RedScreen;
//     public Image BlueScreen;

//     public Button P2GrenadeButton;
//     public Button P2BulletButton;
//     public Button P1ShieldButton;
//     public Button P1ReloadButton;

//     // Start is called before the first frame update
//     void Start()
//     {
//         health = maxHealth;
//         UPHP.text = "100";
//         P1KillCount.text = "0";
//         P2BulletCount.text = "6";
//         P2GrenadeCount.text = "2";
//         P1ShieldCount.text = "3";
//         shieldHealth = 0;
//         currentTime = startingTime;
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
//             P1KillCount.text = "" + killCount;
//             WarningMessages.text = "Player 1 Dead";
//         }
//     }

//     public void UpdateHealthUI(float health)
//     {
//         Debug.Log(health);
//         float fillF = P1FrontHealthBar.fillAmount;
//         float fillB = P1BackHealthBar.fillAmount;
//         float hFraction = health / maxHealth;
        
//         if (fillB > hFraction)
//         {
//             P1FrontHealthBar.fillAmount = hFraction;
//             P1BackHealthBar.color = Color.red;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P1BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
//         }
//         if (fillF < hFraction)
//         {
//             P1BackHealthBar.color = Color.green;
//             P1BackHealthBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P1FrontHealthBar.fillAmount = Mathf.Lerp(fillF, P1BackHealthBar.fillAmount, percentComplete);
//         }
//     }

//     public void UpdateShieldUI(float shieldHealth)
//     {
//         Debug.Log(shieldHealth);
//         P1FrontShieldBar.fillAmount = shieldHealth;
//         float fillF = P1FrontShieldBar.fillAmount;
//         float hFraction = shieldHealth / maxShieldHealth;

//         if (fillF > hFraction)
//         {
//             P1FrontShieldBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             P1FrontShieldBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
//         }
//     }

//     public void BulletDamage()
//     {
//         if(bulletCount == 0)
//         {
//             return;
//         }

//         if(shieldHealth == bulletDamage)
//         {
//             shieldHealth -= bulletDamage;
//             UPHP.text = "" + health;
//             bulletCount -= 1;
//             P2BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//             // Minus 1 shield count
//         }
//         else if(shieldHealth < bulletDamage)
//         {
            
//             health -= bulletDamage - shieldHealth; 
//             shieldHealth = 0;
//             UPHP.text = "" + health;
//             bulletCount -= 1;
//             P2BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//         else if(shieldHealth > bulletDamage)
//         {
//             shieldHealth -= bulletDamage;
//             UPHP.text = "" + health;
//             bulletCount -= 1;
//             P2BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//         else 
//         {
//             WarningMessages.text = "You have been HIT by a bullet";
//             health -= bulletDamage;
//             UPHP.text = "" + health;
//             bulletCount -= 1;
//             P2BulletCount.text = "" + bulletCount;
//             lerpTimer = 0f;
//         }
//     }

//     public void GrenadeDamage()
//     {
//         if(grenadeCount == 0)
//         {
//             return;
//         }

//         if(shieldHealth == grenadeDamage)
//         {
//             health -= grenadeDamage - shieldHealth; 
//             shieldHealth = 0;
//             UPHP.text = "" + health;
//             grenadeCount -= 1;
//             P2GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
//         }
//         else if(shieldHealth < grenadeDamage)
//         {
            
//             health -= grenadeDamage - shieldHealth;
//             shieldHealth = 0;
//             UPHP.text = "" + health;
//             grenadeCount -= 1;
//             P2GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
//         } 
//         else 
//         {
//             WarningMessages.text = "You have been HIT by Grenade";
//             health -= grenadeDamage;
//             if(health <= 0)
//             {
//                 RestoreHealth();
//             }
//             UPHP.text = "" + health;
//             grenadeCount -= 1;
//             P2GrenadeCount.text = "" + grenadeCount;
//             lerpTimer = 0f;
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
//             WarningMessages.text = "Shield has been activated";
//             shieldCount -= 1;
//             shieldHealth = maxShieldHealth;
//             P1ShieldCount.text = "" + shieldCount;
//             lerpTimer = 0f;
//         }
//     }

//     public void RestoreHealth()
//     {
//         health = maxHealth;
//         UPHP.text = "" + health;
//         lerpTimer = 0f;
//     }

//     public void ReloadBullets()
//     {
//         if(bulletCount == 0)
//         {
//             bulletCount = 6;
//             P2BulletCount.text = "" + bulletCount;
//         }
//         else
//         {
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
