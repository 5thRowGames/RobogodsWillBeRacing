using System;
using System.Collections;
using InControl;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeManager : SettingsBase
{
    public Image fillImage;
    public float amountFilled;

    private void OnEnable()
    {
        fillImage.fillAmount = PlayerPrefs.GetFloat(PlayerPrefsEnum.PlayerPrefs.Music.ToString());
    }
    
    public void Update()
    {
        if (canInput)
        {
            if (inputModule.MoveAction.Left.IsPressed)
            {
                DecreaseVolume();
                StartCoroutine(TimeBetweenInputs());
            }
            else if (inputModule.MoveAction.Right.IsPressed)
            {
                RaiseVolume();
                StartCoroutine(TimeBetweenInputs());
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(TimeBetweenInputs());
        canInput = true;
    }

    IEnumerator TimeBetweenInputs()
    {
        canInput = false;
        yield return new WaitForSeconds(timeBetweenInput);
        canInput = true;

    }

    private void RaiseVolume()
    {
        //Meter llamada a evento de subir volumen

        fillImage.fillAmount += amountFilled;

        if (fillImage.fillAmount > 1)
        {
            fillImage.fillAmount = 1;
        }
        else
            SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
        
        AkSoundEngine.SetRTPCValue("Volumen_Musica", (1 - fillImage.fillAmount) * 100f);
        

        PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.Music.ToString(),(1 - fillImage.fillAmount));
        PlayerPrefs.Save();

    }

    private void DecreaseVolume()
    {
        //Meter llamada a evento de bajar volumen

        fillImage.fillAmount -= amountFilled;

        if (fillImage.fillAmount < 0)
        {
            fillImage.fillAmount = 0;
        }
        else
            SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
        
        AkSoundEngine.SetRTPCValue("Volumen_Musica", (1 - fillImage.fillAmount) * 100f);

        PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.Music.ToString(),(1 - fillImage.fillAmount));
        PlayerPrefs.Save();
    }
}
