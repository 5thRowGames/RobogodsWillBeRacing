using System;
using System.Collections.Generic;
using UnityEngine;

public class CompuertaPasiva : MonoBehaviour
{
    public List<PuertaCompuerta> puertas;

    private int players;

    private void Awake()
    {
        players = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") ||
            other.CompareTag("Thor"))
        {
            players++;

            if (players == 1)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") ||
            other.CompareTag("Thor"))
        {
            players--;

            if (players == 0)
            {
                CloseDoor();
            }
        }
    }

    public void OpenDoor()
    {
        foreach (var puerta in puertas)
        {
            puerta.OpenDoor();
        }
    }

    public void CloseDoor()
    {
        foreach (var puerta in puertas)
        {
            puerta.CloseDoor();
        }
    }
}
