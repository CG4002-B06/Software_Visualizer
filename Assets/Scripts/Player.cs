using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Attach script to P1 and P2 separately
public class Player : MonoBehaviour
{
    // Damage
    public const int bulletDamage = 10;
    public int bulletCount = 6;
    public const int grenadeDamage = 30;
    public int grenadeCount = 2;
    
    // Health
    public const float maxHealth = 100;
    public float health;

    // Shield
    public const float maxShieldHealth = 30;
    private float shieldHealth;
    public int shieldCount = 3;
  
    // Kill Count 
    public int killCount = 0;  

    // Parameters
    public float chipSpeed = 2f;
    private float lerpTimer;
    private bool isTimerOver = true;

    public bool status = true;

    // Texts
    public TextMeshProUGUI HP;
    public TextMeshProUGUI BulletCount;
    public TextMeshProUGUI GrenadeCount;
    public TextMeshProUGUI ShieldCount;

    // Scripts
    public DeathCounter deathCounter;
    public ShieldTimer shieldTimer;
    public GrenadeThrowerP1 grenadeThrower1;
    public GrenadeThrowerP2 grenadeThrower2;
    public Bullet bulletShooter;
    public OpenScreen openScreen;
    public Destructible destruct;

    // Gameobjects
    public GameObject Timer;
    public GameObject Players;

    public GameObject ImgTarget;
    public GameObject SWShield;

    // Health and Shield Bar
    public Image FrontHealthBar;
    public Image BackHealthBar;
    public Image FrontShieldBar;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shieldHealth = 0;
        HP.text = "100";
        deathCounter.UpdatePlayerDeathCount(0);
        bulletCount = 6;
        grenadeCount = 2;
        shieldCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        Shield();
        IsActiveShield();
        HP.text = "" + health; 
        BulletCount.text = "" + bulletCount;
        ShieldCount.text = "" + shieldCount;
        GrenadeCount.text = "" + grenadeCount;
    }

    public void Health()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if(shieldHealth == 0)
        {
            UpdateHealthUI(health);
            if(Players.Equals(GameObject.Find("P1")))
            {
                openScreen.OpenBlueScreen(false);
            }
        }
        
        if(health <= 0)
        {
            RestoreHealth();
            // bulletCount = 6;
            // grenadeCount = 2;
            // shieldCount = 3;
            // BulletCount.text = "" + bulletCount;
            // GrenadeCount.text = "" + grenadeCount;
            // ShieldCount.text = "" + shieldCount;
            killCount++;
            deathCounter.UpdatePlayerDeathCount(killCount);
        }
    }

    public void UpdateHealthUI(float health)
    {
        Debug.Log(health);
        float fillF = FrontHealthBar.fillAmount;
        float fillB = BackHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        
        if (fillB > hFraction)
        {
            FrontHealthBar.fillAmount = hFraction;
            BackHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            BackHealthBar.color = Color.green;
            BackHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            FrontHealthBar.fillAmount = Mathf.Lerp(fillF, BackHealthBar.fillAmount, percentComplete);
        }
    }

    public void Shield()
    {
        shieldHealth = Mathf.Clamp(shieldHealth, 0, maxShieldHealth);
        UpdateShieldUI(shieldHealth);
    }

    public void UpdateShieldUI(float shieldHealth)
    {
        Debug.Log(shieldHealth);
        
        float fillF = FrontShieldBar.fillAmount;
        float hFraction = shieldHealth / maxShieldHealth;

        if (fillF > hFraction)
        {
            FrontShieldBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            FrontShieldBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void Bullet()
    {
        if(bulletCount == 0)
        { 
            return;
        }
        
        if(status)
        { 
            if(Players.Equals(GameObject.Find("P2")))
            {
                bulletShooter.BulletShooter();   
            }
            else if(Players.Equals(GameObject.Find("P1")))
            {
                openScreen.OpenRedScreen(true);
            }

            if(shieldHealth == bulletDamage)
            {
                shieldHealth -= bulletDamage;
                shieldHealth = 0;

                SWShield.SetActive(false);
                openScreen.OpenBlueScreen(false);

                HP.text = "" + health;
                bulletCount -= 1;
                lerpTimer = 0f;
            }
            else if(shieldHealth > bulletDamage)
            {
                shieldHealth -= bulletDamage;  
                bulletCount -= 1;
                lerpTimer = 0f;
            }
            else 
            {
                health -= bulletDamage;
                HP.text = "" + health;
                bulletCount -= 1;
                lerpTimer = 0f;
            }
        }
        else {
            Debug.Log("Miss");
        }
    }

    public void Grenade()
    {
        if(grenadeCount == 0)
        {
            return;
        }
        if(status)
        {
            if(Players.Equals(GameObject.Find("P2")))
            {
                grenadeThrower1.ThrowGrenade();
                SWShield.SetActive(false);
            }
            
            if(Players.Equals(GameObject.Find("P1")))
            {
                grenadeThrower2.ThrowGrenade();  
                openScreen.OpenBlueScreen(false);
            }
    
            if(shieldHealth <= grenadeDamage)
            {
                health -= grenadeDamage - shieldHealth; 
                shieldHealth = 0;
                grenadeCount -= 1; 
                lerpTimer = 0f;
            }
            else 
            {
                health -= grenadeDamage;
                grenadeCount -= 1;
                GrenadeCount.text = "" + grenadeCount;
                lerpTimer = 0f;
            }
        }
        else {
            Debug.Log("Miss");
        }
    }

    public void ReloadBullets()
    {
        if(bulletCount == 0)
        {
            bulletCount = 6;
            openScreen.OpenYellowScreen(true);
        }
        else
        {
            return;
        }
    }

    public bool IsActiveShield()
    {
        if(shieldHealth > 0)
        {
            return true;
        }
        
        return false;
    }

    public void ActivateShield()
    {
        if(shieldCount == 0)
        {
            return;
        }

        // If shield is not yet active and player activates shield
        if(!IsActiveShield())
        {
            if(Players.Equals(GameObject.Find("P2")))
            {
                SWShield.SetActive(true);
            }
            else if(Players.Equals(GameObject.Find("P1")))
            {
                openScreen.OpenBlueScreen(true);
                Timer.SetActive(true);
                shieldTimer.SetHasStart(true); 
            }

            shieldCount -= 1;
            shieldHealth = maxShieldHealth;   
            FrontShieldBar.fillAmount = shieldHealth;
            lerpTimer = 0f;
        }
    }

    public void RestoreHealth()
    {
        health = maxHealth;
        lerpTimer = 0f;
    }

    public void DeactivateShield()
    {
        shieldHealth = 0;
    }

    public void TargetFound(bool target)
    {
        status = target;
    }

}
