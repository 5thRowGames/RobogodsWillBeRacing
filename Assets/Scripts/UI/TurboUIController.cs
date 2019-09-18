using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurboUIController : MonoBehaviour
{
    [SerializeField] private MyCarController myCarController;
    [SerializeField] private Image turbo;

    // Update is called once per frame
    void Update()
    {
        turbo.fillAmount = myCarController.Turbo;
    }
}
