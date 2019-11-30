using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UpdateUILocalizationIcon : MonoBehaviour
{
    public Sprite spanishIcon;
    public Sprite englishIcon;

    private Image icon;
    
    void Awake()
    {
        icon = GetComponent<Image>();
    }

    private void Start()
    {
        LocalizationManager.Instance.UpdateIcon += UpdateIcon;
        UpdateIcon();
    }

    public void UpdateIcon()
    {

        if (LocalizationManager.Instance.currentLanguage == GameLanguage.Language.Spanish)
            icon.sprite = spanishIcon;
        else
            icon.sprite = englishIcon;
    }
}
