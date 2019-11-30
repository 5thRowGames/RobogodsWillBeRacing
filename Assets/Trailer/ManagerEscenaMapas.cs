using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEscenaMapas : MonoBehaviour
{
    public Plano1 limboPlano1;
    public Plano2 limboPlano2;
    public Plano1 egiptoPlano1;
    public Plano2 egiptoPlano2;
    public Plano1 greciaPlano1;
    public Plano2 greciaPlano2;

    private void DeactivateAll()
    {
        limboPlano1.enabled = false;
        limboPlano2.enabled = false;
        egiptoPlano1.enabled = false;
        egiptoPlano2.enabled = false;
        greciaPlano1.enabled = false;
        greciaPlano2.enabled = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DeactivateAll();
            limboPlano1.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            DeactivateAll();
            limboPlano2.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            DeactivateAll();
            egiptoPlano1.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            DeactivateAll();
            egiptoPlano2.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            DeactivateAll();
            greciaPlano1.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            DeactivateAll();
            greciaPlano2.enabled = true;
        }
    }

}    
