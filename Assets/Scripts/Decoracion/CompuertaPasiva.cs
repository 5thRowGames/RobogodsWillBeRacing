using System;
using System.Collections.Generic;
using UnityEngine;

public class CompuertaPasiva : MonoBehaviour
{
    public List<PuertaCompuerta> puertas;

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
