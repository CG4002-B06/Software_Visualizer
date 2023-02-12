// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ShieldBar : MonoBehaviour
// {
//     public const float maxShieldHealth = 30;
//     public Image FrontShieldBar;

//     public float chipSpeed = 2f;
//     private float lerpTimer;

//     public void UpdateShieldUI(float shieldHealth)
//     {
//         Debug.Log(shieldHealth);
//         FrontShieldBar.fillAmount = shieldHealth;
//         float fillF = FrontShieldBar.fillAmount;
//         float hFraction = shieldHealth / maxShieldHealth;

//         if (fillF > hFraction)
//         {
//             FrontShieldBar.fillAmount = hFraction;
//             lerpTimer += Time.deltaTime;
//             float percentComplete = lerpTimer / chipSpeed;
//             percentComplete = percentComplete * percentComplete;
//             FrontShieldBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
//         }
//     }
// }
