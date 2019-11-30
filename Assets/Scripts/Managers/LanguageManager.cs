using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private bool pressed;
    public InControlInputModule inputModule;
    private bool input;

    private void OnEnable()
    {
        input = true;
    }

    public void Update()
    {
        if (input)
        {
            if (inputModule.MoveAction.Left.IsPressed)
            {
                SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
                pressed = true;
                input = false;
                LocalizationManager.Instance.ChangeLanguageType(true);

            }
            else if (inputModule.MoveAction.Right.IsPressed)
            {
                SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
                pressed = true;
                input = false;
                LocalizationManager.Instance.ChangeLanguageType(false);
            }
        }

        if (pressed && (inputModule.MoveAction.Left.WasReleased || inputModule.MoveAction.Right.WasReleased))
        {
            pressed = false;
            input = true;
        }
    }

    private void OnDisable()
    {
        input = true;
    }
    
    public virtual void Activate()
    {
        enabled = true;
    }

    public virtual void Deactivate()
    {
        enabled = false;
    }
}
