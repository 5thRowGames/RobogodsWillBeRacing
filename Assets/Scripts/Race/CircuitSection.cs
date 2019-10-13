using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSection : MonoBehaviour
{
    // Los checkpoints y portales deben ser añadidos desde el editor y en orden
    public List<Checkpoint> checkpoints;
    public List<Portal> portalsEntrances;
    public List<Transform> portalsExits;
}
