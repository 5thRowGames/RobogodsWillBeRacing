using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashUpdateText : MonoBehaviour
{
    public TextMeshProUGUI label;
    public string firstLocalizationWord, secondLocalizationWord;

    private string firstWord, secondWord;
    private float time;
    private bool isFirstWord;

    private void OnEnable()
    {
        firstWord = LocalizationManager.Instance.GetWord(firstLocalizationWord);
        secondWord = LocalizationManager.Instance.GetWord(secondLocalizationWord);
        label.text = firstWord;
        time = 0;
        isFirstWord = true;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 2f)
        {
            time = 0;

            if (isFirstWord)
            {
                label.text = secondWord;
                isFirstWord = false;
            }
            else
            {
                label.text = firstWord;
                isFirstWord = true;
            }

        }
    }
}
