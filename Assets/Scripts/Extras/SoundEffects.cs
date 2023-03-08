using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AudioSource beingHitSound;

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
    public AudioSource logOutSound; 
    public AudioSource siuuuSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBeingHitSound()
    {
        beingHitSound.Play();
    }

    public void InvokePlayBeingHitSound()
    {
        Invoke("PlayBeingHitSound", 2f);
    }

    public void PlaySettingsButtonSound()
    {
        settingsSound.Play();
    }

    public void PlayGrenadeThrowSound()
    {
        grenadeThrowSound.Play();
    }

    public void PlayBulletShootSound()
    {
        bulletShootSound.Play();
    }

    public void PlayGrenadeExplosionSound()
    {
        grenadeExplosionSound.Play();
    }

    public void InvokePlayGrenadeExplosionSound()
    {
        Invoke("PlayGrenadeExplosionSound", 2f);
    }

    public void PlayShieldActivationSound()
    {
        shieldActivationSound.Play();
    }

    public void PlayShieldDeactivationSound()
    {
        shieldDeactivationSound.Play();
    }

    public void PlayReloadSound()
    {
        reloadSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void PlayAmmoWarningSound()
    {
        ammoWarningSound.Play();
    }

    public void PlayGrenadeUnableSound()
    {
        grenadeUnableSound.Play();
    }

    public void PlayShieldUnableSound()
    {
        shieldUnableSound.Play();
    }

    public void PlayReloadUnableSound()
    {
        reloadUnableSound.Play();
    }

    public void PlayGrenadeIncomingSound()
    {
        grenadeIncomingSound.Play();
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

    public void PlayMissSound()
    {
        missSound.Play();
    }

    public void PlayShieldCooldownSound()
    {
        shieldCooldownSound.Play();
    }

    public void PlayTargetOnSightSound()
    {
        targetOnSightSound.Play();
    }
    
    public void PlayTargetOffSightSound()
    {
        targetOffSightSound.Play();
    }

    public void PlayLogOutSound()
    {
        logOutSound.Play();
    }

    public void PlayStatusUpdatingSound()
    {
        statusUpdatingSound.Play();
    }

    public void PlaySiuuuSound()
    {
        siuuuSound.Play();
    }
}
