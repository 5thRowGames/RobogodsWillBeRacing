﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsManager : SettingsBase
{
    public Image fillImage;
    public float amountFilled;

    public void UpdateFillAmount()
    {
        fillImage.fillAmount = PlayerPrefs.GetFloat(PlayerPrefsEnum.PlayerPrefs.SFX.ToString());
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
        yield return new WaitForSecondsRealtime(timeBetweenInput);
        canInput = true;

    }

    private void RaiseVolume()
    {
        //Meter llamada a evento de subir volumen
        //Meter sonido de botón paradar feedback de input y de volumen
        
        fillImage.fillAmount += amountFilled;

        if (fillImage.fillAmount > 1)
        {
            fillImage.fillAmount = 1;
        }
        else
            SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
        
        AkSoundEngine.SetRTPCValue("Volumen_SFX", fillImage.fillAmount * 100f);

        PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.SFX.ToString(),fillImage.fillAmount);
        PlayerPrefs.Save();
    }

    private void DecreaseVolume()
    {
        //Meter llamada a evento de bajar volumen
        //Meter sonido de botón para dar feedback de input y de volumen
        fillImage.fillAmount -= amountFilled;

        if (fillImage.fillAmount < 0)
        {
            fillImage.fillAmount = 0;
        }
        else
            SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
        
        AkSoundEngine.SetRTPCValue("Volumen_SFX", fillImage.fillAmount * 100f);

        PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.SFX.ToString(),fillImage.fillAmount);
        PlayerPrefs.Save();
    }
}
