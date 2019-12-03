using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSound : MonoBehaviour
{

    void Awake()
    {
        if(!PlayerPrefs.HasKey(PlayerPrefsEnum.PlayerPrefs.FirstTime.ToString()))
        {
            PlayerPrefs.SetInt(PlayerPrefsEnum.PlayerPrefs.FirstTime.ToString(), 1);
            PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.Music.ToString(), 0.5f);
            PlayerPrefs.SetFloat(PlayerPrefsEnum.PlayerPrefs.SFX.ToString(), 0.5f);
            PlayerPrefs.Save();
            
            AkSoundEngine.SetRTPCValue("Volumen_SFX", 50);
            AkSoundEngine.SetRTPCValue("Volumen_Musica", 50);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("Volumen_SFX", PlayerPrefs.GetFloat(PlayerPrefsEnum.PlayerPrefs.SFX.ToString())* 100f);
            AkSoundEngine.SetRTPCValue("Volumen_Musica", PlayerPrefs.GetFloat(PlayerPrefsEnum.PlayerPrefs.Music.ToString()) * 100f);
        }

    }
}
