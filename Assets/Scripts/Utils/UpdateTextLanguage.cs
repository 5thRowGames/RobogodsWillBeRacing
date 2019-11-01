using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextLanguage : MonoBehaviour
{
    public string localizationID;
    
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    
    private void Start()
    {
        LocalizationManager.Instance.UpdateLanguage += UpdateText;
        UpdateText();
    }
    
    public void UpdateText()
    {
        textMeshPro.text = LocalizationManager.Instance.GetWord(localizationID);
    }
}
