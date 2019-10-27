using System;
using UnityEngine;

public class CompuertaActiva : MonoBehaviour
{
    private int players;

    public Action OpenDoor;
    public Action CloseDoor;
    public ParticleSystem speedLines;

    private void Start()
    {
        players = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") || other.CompareTag("Thor"))
        {
            if (players == 0)
            {
                OpenDoor();
                speedLines.Play();
            }

            players++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") || other.CompareTag("Thor"))
        {
            if (players == 1)
            {
                CloseDoor();
                speedLines.Stop();
            }

            players--;
        }
    }
}
