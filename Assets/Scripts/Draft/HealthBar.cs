// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class HealthBar : MonoBehaviour
// {
//     public const float maxHealth = 100;
//     public Image FrontHealthBar;
//     public Image BackHealthBar;

//     public float chipSpeed = 2f;
//     private float lerpTimer;

//     public void UpdateHealthUI(float health)
//     {
//         Debug.Log(health);
//         float fillF = FrontHealthBar.fillAmount;
//         float fillB = BackHealthBar.fillAmount;
//         float hFraction = health / maxHealth;
        
//         if (fillB > hFraction)
//         {
//             FrontHealthBar.fillAmount = hFraction;
//             BackHealthBar.color = Color.red;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
//         }
//         if (fillF < hFraction)
//         {
//             BackHealthBar.color = Color.green;
//             BackHealthBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             FrontHealthBar.fillAmount = Mathf.Lerp(fillF, BackHealthBar.fillAmount, percentComplete);
//         }
//     }

//     public float RestoreHealth(float health)
//     {
//         health = maxHealth;
//         lerpTimer = 0f;

//         return health;
//     }
// }
