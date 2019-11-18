using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateUnityTextLanguage : MonoBehaviour
{
    public string localizationID;
    
    private Text UIText;

    private void Awake()
    {
        UIText = GetComponent<Text>();
    }
    
    private void Start()
    {
        LocalizationManager.Instance.UpdateLanguage += UpdateText;
        UpdateText();
    }
    
    public void UpdateText()
    {
        UIText.text = LocalizationManager.Instance.GetWord(localizationID);
    }
}
