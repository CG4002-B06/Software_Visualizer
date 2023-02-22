using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageTimer : MonoBehaviour
{
    float currentTime = 3;
    [SerializeField] TextMeshProUGUI warningText;
    public RawImage warningBg;
    public SoundEffects soundEffects;

    public void SetWarning(string warning)
    {
        warningText.text = warning;

        if(warning == "Warning! \n\n Unable to reload as you still have ammo") 
        {
            soundEffects.playReloadUnableSound();
        } 
        else if(warning == "Warning! \n\n You are out of ammo") 
        {
            soundEffects.playAmmoWarningSound();
        }
        else if(warning == "Warning! \n\n You are out of grenades") 
        {
            soundEffects.playGrenadeUnableSound();
        }
        else if(warning == "Warning! \n\n You are out of shields") 
        {
            soundEffects.playShieldUnableSound();
        }
        else if(warning == "Warning! \n\n Shield on cooldown") 
        {
            soundEffects.playsShieldCooldownSound();
        }
    }

    void Update()
    {
        warningBg.enabled = true;
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            warningText.text = "";
            warningBg.enabled = false;
        }   
    }
}