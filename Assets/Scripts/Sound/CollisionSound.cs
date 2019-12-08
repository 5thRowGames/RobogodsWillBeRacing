using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public bool Anubis;
    public bool Poseidon;
    public bool Kali;
    public bool Thor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Anubis") && Anubis)
        {
            AkSoundEngine.PostEvent("Impactos_In", gameObject);
        }
        
        if (other.CompareTag("Poseidon") && Poseidon)
        {
            AkSoundEngine.PostEvent("Impactos_In", gameObject);
        }
        
        if (other.CompareTag("Kali") && Kali)
        {
            AkSoundEngine.PostEvent("Impactos_In", gameObject);
        }
        
        if (other.CompareTag("Thor") && Thor)
        {
            AkSoundEngine.PostEvent("Impactos_In", gameObject);
        }
        
        
    }
}
