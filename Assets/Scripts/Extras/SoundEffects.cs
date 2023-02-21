using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffects : MonoBehaviour 
{
    // Action Sounds
    public AudioSource settingsSound;
    public AudioSource grenadeThrowSound;
    public AudioSource bulletShootSound;
    public AudioSource grenadeExplosionSound;
    public AudioSource shieldActivationSound;
    public AudioSource shieldDeactivationSound;
    public AudioSource reloadSound;
    public AudioSource deathSound;

    // Warning Sounds
    public AudioSource ammoWarningSound;
    public AudioSource grenadeUnableSound;
    public AudioSource shieldUnableSound;
    public AudioSource reloadUnableSound;
    public AudioSource grenadeIncomingSound;
    public AudioSource hitSound;
    public AudioSource missSound;
    public AudioSource shieldCooldownSound;
    public AudioSource targetOnSightSound;
    public AudioSource targetOffSightSound;
    public AudioSource statusUpdatingSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSettingsButtonSound()
    {
        settingsSound.Play();
   
        DontDestroyOnLoad(settingsSound);
    }

    public void playGrenadeThrowSound()
    {
        grenadeThrowSound.Play();
   
        DontDestroyOnLoad(grenadeThrowSound);
    }

    public void playBulletShootSound()
    {
        bulletShootSound.Play();
   
        DontDestroyOnLoad(bulletShootSound);
    }

    public void playGrenadeExplosionSound()
    {
        grenadeExplosionSound.Play();
   
        DontDestroyOnLoad(grenadeExplosionSound);
    }

    public void playShieldActivationSound()
    {
        shieldActivationSound.Play();
   
        DontDestroyOnLoad(shieldActivationSound); 
    }

    public void playShieldDeactivationSound()
    {
        shieldDeactivationSound.Play();
   
        DontDestroyOnLoad(shieldDeactivationSound); 
    }

    public void playReloadSound()
    {
        reloadSound.Play();
   
        DontDestroyOnLoad(reloadSound); 
    }

    public void playDeathSound()
    {
        deathSound.Play();
   
        DontDestroyOnLoad(deathSound);
    }

    public void playAmmoWarningSound()
    {
        ammoWarningSound.Play();
   
        DontDestroyOnLoad(ammoWarningSound);
    }

    public void playGrenadeUnableSound()
    {
        grenadeUnableSound.Play();
   
        DontDestroyOnLoad(grenadeUnableSound);
    }

    public void playShieldUnableSound()
    {
        shieldUnableSound.Play();
   
        DontDestroyOnLoad(shieldUnableSound);
    }

    public void playReloadUnableSound()
    {
        reloadUnableSound.Play();
   
        DontDestroyOnLoad(reloadUnableSound);
    }

    public void playGrenadeIncomingSound()
    {
        grenadeIncomingSound.Play();
   
        DontDestroyOnLoad(grenadeIncomingSound);
    }

    public void playHitSound()
    {
        hitSound.Play();
   
        DontDestroyOnLoad(hitSound);
    }

    public void playMissSound()
    {
        missSound.Play();
   
        DontDestroyOnLoad(missSound);
    }

    public void playsShieldCooldownSound()
    {
        shieldCooldownSound.Play();
   
        DontDestroyOnLoad(shieldCooldownSound);
    }

    public void playsTargetOnSightSound()
    {
        targetOnSightSound.Play();
   
        DontDestroyOnLoad(targetOnSightSound);
    }
    
    public void playsTargetOffSightSound()
    {
        targetOffSightSound.Play();
   
        DontDestroyOnLoad(targetOffSightSound);
    }

    // public void playStatusUpdatingSound()
    // {
    //     statusUpdatingSound.Play();
   
    //     DontDestroyOnLoad(statusUpdatingSound);
    // }
}
