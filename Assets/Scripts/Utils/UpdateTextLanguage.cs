using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextLanguage : MonoBehaviour
{
    public string localizationID;
    public TextMeshProUGUI textMeshPro;

    //Definitivo, pero para las pruebas no se puede poner primero porque se ejecuta antes que cuando se lee el diccionario
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
