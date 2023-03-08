using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    // Damage
    public const int bulletDamage = 10;
    public int bulletCount;
    public const int grenadeDamage = 30;
    public int grenadeCount;
    
    // Health
    public const float maxHealth = 100;
    public float health;

    // Shield
    public const float maxShieldHealth = 30;
    public float shieldHealth;
    public int shieldCount;
  
    // Kill Count 
    public int killCount = 0;  

    // Parameters
    public float chipSpeed = 2f;
    private float lerpTimer;
    public bool status = false;

    // Texts
    public TextMeshProUGUI HP;
    public TextMeshProUGUI BulletCount;
    public TextMeshProUGUI GrenadeCount;
    public TextMeshProUGUI ShieldCount;

    // Scripts
    public DeathCounter deathCounter;
    public ShieldTimer shieldTimer;
    public Shield2Timer shield2Timer;
    public GrenadeThrowerP1 grenadeThrower1;
    public GrenadeThrowerP2 grenadeThrower2;
    public BulletShooter bulletShooter;
    public OpenScreen openScreen;
    public SoundEffects soundEffects;
    public InventoryBars inventoryBars;
    public ChangeCrossHairSize changeCrossHairSize;

    // Gameobjects
    public GameObject Timer;
    public GameObject Timer2;
    public GameObject Players;
    public GameObject Laser;

    // Opponent Detection
    public GameObject CrossHair;
    public GameObject ImgTarget;
    public GameObject SWShield;
    public GameObject CrackedShield;

    // Health and Shield Bar
    public Image FrontHealthBar;
    public Image BackHealthBar;
    public Image FrontShieldBar;

    // Start is called before the first frame update
    void Start()
    {
        // Initalising variables
        health = maxHealth;
        shieldHealth = 0;
        HP.text = "100";
        deathCounter.UpdatePlayerDeathCount(0);
        bulletCount = 6;
        grenadeCount = 2;
        shieldCount = 3;
        BulletCount.text = "" + bulletCount;
        ShieldCount.text = "" + shieldCount;
        GrenadeCount.text = "" + grenadeCount;
        inventoryBars.SetAmmoBar(bulletCount);
        inventoryBars.SetGrenadeBar(grenadeCount);
        inventoryBars.SetShieldBar(shieldCount);
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        Shield();
        InventoryCount();
    }

    // General functions
// ---------------------------------------------------------------------------------------------------------------------

    public void Health()
    {
        HP.text = "" + health;
        health = Mathf.Clamp(health, 0, maxHealth);

        if(shieldHealth == 0)
        {
            UpdateHealthUI(health);
            if(Players.Equals(GameObject.Find("P1")))
            {
                openScreen.OpenBlueScreen(false);
            }
        }

        if(health >= 30 && health <= 50)
        {
            HP.color = Color.yellow;
        }
        if(health < 30)
        {
            HP.color = Color.red;
        }
    }

    // Update healthbar UI
    public void UpdateHealthUI(float health)
    {
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

        if(shieldHealth <= 0)
        {
            CrackedShield.SetActive(false);
        }
    }

    // Update shieldbar UI
    public void UpdateShieldUI(float shieldHealth)
    {
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
    
    // Update UI for bullet, shield and grenade counts
    public void InventoryCount()
    {
        BulletCount.text = "" + bulletCount;
        ShieldCount.text = "" + shieldCount;
        GrenadeCount.text = "" + grenadeCount;
        inventoryBars.SetAmmoBar(bulletCount);
        inventoryBars.SetGrenadeBar(grenadeCount);
        inventoryBars.SetShieldBar(shieldCount);
        BulletCount.color = Color.white;
        GrenadeCount.color = Color.white;
        ShieldCount.color = Color.white;

        if(bulletCount <= 2) 
        {
            BulletCount.color = Color.red;
        }
        else if (bulletCount == 3 || bulletCount == 4)
        {
            BulletCount.color = Color.yellow;
        }

        if(grenadeCount <= 1) 
        {
            GrenadeCount.color = Color.red;
        }

        if(shieldCount <= 1) 
        {
            ShieldCount.color = Color.red;
        } 
        else if (shieldCount == 2)
        {
            ShieldCount.color = Color.yellow;
        }
    }

    // Action functions
// ---------------------------------------------------------------------------------------------------------------------

    // Action function for bullet
    public void InvokeShootLaser()
    {
        Laser.SetActive(false);
    }
    public void Bullet()
    {   
        // Update AR and screen effects
        if(Players.Equals(GameObject.Find("P1")))
        {
            openScreen.OpenWhiteScreen(true);
            Laser.SetActive(true);
            Invoke("InvokeShootLaser", 1f);
            bulletShooter.ShootBullet();
            changeCrossHairSize.changeSize(1f);
        }
        
        if(Players.Equals(GameObject.Find("P2")))
        {
            openScreen.OpenRedScreen(true);
            soundEffects.PlayBeingHitSound();
        }

        if(shieldHealth >= bulletDamage)
        {
            CrackedShield.SetActive(true);
        }

        // Update UI
        if(shieldHealth == bulletDamage)
        {
            SWShield.SetActive(false);
            openScreen.OpenBlueScreen(false);
            lerpTimer = 0f;
        }
        
        lerpTimer = 0f;
    }

    // Action function for grenade
    public void Grenade()
    {
        // Update AR and screen effects
        if(Players.Equals(GameObject.Find("P1")))
        {
            grenadeThrower1.ThrowGrenade();
            SWShield.SetActive(false);
            changeCrossHairSize.changeSize(2f);
        }
        
        if(Players.Equals(GameObject.Find("P2")))
        {
            grenadeThrower2.ThrowGrenade();
            openScreen.OpenBlueScreen(false);
            openScreen.InvokeRedScreen();
            soundEffects.InvokePlayBeingHitSound();
        }
        
        lerpTimer = 0f;
    }

    // Action function for reload
    public void ReloadBullets()
    {
        openScreen.OpenYellowScreen(true);
    }

    // Action function for shield
    public void ActivateShield()
    {
        // Update UI
        FrontShieldBar.fillAmount = shieldHealth;
        lerpTimer = 0f;

        // Update AR and screen effects
        if(Players.Equals(GameObject.Find("P2")))
        {
            SWShield.SetActive(true);
            Timer2.SetActive(true);
            shield2Timer.SetHasStart(true);
        }
        
        if(Players.Equals(GameObject.Find("P1")))
        {
            
            Timer.SetActive(true);
            shieldTimer.SetHasStart(true); 
        }
    }

    // Helper functions
// ---------------------------------------------------------------------------------------------------------------------
    public void DeactivateShield()
    {
        // shieldHealth = 0;
        soundEffects.PlayShieldDeactivationSound();
    }

    public void TargetFound(bool target)
    {
        status = target;
        if(target == false)
        {
            CrossHair.SetActive(false);
            soundEffects.PlayTargetOffSightSound();
        } 
        else 
        {
            CrossHair.SetActive(true);
            soundEffects.PlayTargetOnSightSound();
        }
    }

    public bool ReturnTargetQuery()
    {
        return status;
    }

    public void Logout()
    {
        SceneManager.LoadScene("LogoutScene");
    }

    public void InvokeLogout()
    {
        Invoke("Logout", 1.5f);
    }

    // Updating new component values after recieveing new data packet from Game Engine
// ---------------------------------------------------------------------------------------------------------------------

    public void UpdateHealth(float newHealth)
    {
        health = newHealth;
        lerpTimer = 0f;
    }

    public void UpdateShieldHealth(float newShieldHealth)
    {
        shieldHealth = newShieldHealth;
        lerpTimer = 0f;
    }

    public void UpdateBulletCount(int newBulletCount)
    {
        bulletCount = newBulletCount;
    }

    public void UpdateShieldCount(int newShieldCount)
    {
        shieldCount = newShieldCount;
    }

    public void UpdateGrenadeCount(int newGrenadeCount)
    {
        grenadeCount = newGrenadeCount;
    }
}