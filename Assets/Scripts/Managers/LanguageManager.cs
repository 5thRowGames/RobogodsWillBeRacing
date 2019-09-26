using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : SettingsBase
{

    public void Update()
    {
        if (canInput)
        {
            if (inputModule.MoveAction.Left.IsPressed)
            {
                SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
                LocalizationManager.Instance.ChangeLanguageType(true);
                StartCoroutine(TimeBetweenInputs());
            }
            else if (inputModule.MoveAction.Right.IsPressed)
            {
                SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
                LocalizationManager.Instance.ChangeLanguageType(false);
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
        yield return new WaitForSeconds(timeBetweenInput);
        canInput = true;

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
