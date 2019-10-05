using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : SingletonDontDestroy<LocalizationManager>
{
    public Action UpdateLanguage;
    
    private Dictionary<GameLanguage.Language, Dictionary<string, string>> languageDictionary;
    private GameLanguage.Language currentLanguage;
    private int languagesNumber;

    private void Awake()
    {
        base.Awake();
        languagesNumber = Enum.GetValues(typeof(GameLanguage.Language)).Length;
        
        var file = Resources.Load<TextAsset>("Localization");
        string[] data = file.text.Split('\n');

        languageDictionary = new Dictionary<GameLanguage.Language, Dictionary<string, string>>();
        
        for (int j = 0; j < Enum.GetValues(typeof(God.Type)).Length; j++)
        {
            languageDictionary[(GameLanguage.Language) j] = new Dictionary<string, string>();    
        }  

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');
            
            for (int j = 0; j < languagesNumber; j++)
            {
                try
                {
                    languageDictionary[(GameLanguage.Language) j][row[0]] = row[j + 1];
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("No se ha podido parsear la palabra "+ row[0] + " para el lenguaje "+(GameLanguage.Language) j);
                    throw ex;
                }
            }     
        }
    }

    public void ChangeLanguageType(bool next)
    {
        if (next)
        {

            if ((int) currentLanguage == languagesNumber - 1)
            {
                currentLanguage = 0;
            }
            else
            {
                currentLanguage++;
            }

            UpdateLanguage();
        }
        else
        {
            if ((int) currentLanguage == 0)
            {
                currentLanguage = (GameLanguage.Language) languagesNumber - 1;
            }
            else
            {
                currentLanguage--;
            }

            UpdateLanguage();
        }
    }

    public string GetWord(string id)
    {
        string value = "";
        if (languageDictionary[currentLanguage].TryGetValue(id, out value))
        {
            return value;
        }
        

        return "Not localized";
    }
}
