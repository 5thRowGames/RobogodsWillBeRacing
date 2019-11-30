using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLinePlano : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Animator>() != null)
        {
            if (other.CompareTag("Thor"))
            {
                other.GetComponent<Animator>().SetBool("Win", true);
            }
            else
            {
                other.GetComponent<Animator>().SetBool("Lose", true);
            }
        }
    }
}
