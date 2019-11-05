using System;
using System.Collections;
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
            players++;

            if (players == 1)
            {
                OpenDoor();
                speedLines.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") || other.CompareTag("Thor"))
        {
            players--;

            if (players == 0)
                StartCoroutine(DelayClose());
        }
    }

    IEnumerator DelayClose()
    {
        yield return new WaitForSeconds(0.5f);

        if (players == 0)
        {
            CloseDoor();
            speedLines.Stop();
        }
    }
}
