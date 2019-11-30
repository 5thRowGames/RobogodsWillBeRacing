using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlanoUIManager : MonoBehaviour
{
    public GameObject anubis;
    public GameObject poseidon;
    public GameObject kali;
    public GameObject thor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
    }

    private void Activate()
    {
        anubis.SetActive(true);
        poseidon.SetActive(true);
        kali.SetActive(true);
        thor.SetActive(true);
    }
}
