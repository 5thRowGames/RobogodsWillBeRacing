using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que controla datos fundamentales del jugador (no se aún si haría falta), solo hace falta para saber que control tiene cada jugador, pero aún no le hemos dado uso 19/05/2019
public class PlayerManager : MonoBehaviour,IPlayer
{
    private int id;
    public int Id
    {
        get => id;
        set => id = value;
    }

    private string playerName;
    public string PlayerName
    {
        get => playerName; 
        set => playerName = value; 
    }
}
