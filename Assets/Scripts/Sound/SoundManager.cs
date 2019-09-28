using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public enum Fx
    {
        UI_MetallicButtonFocus = 0,
        UI_Panel_In = 1,
        UI_Cortinilla_In = 2,
        UI_Select = 3,
        UI_Servos_In = 4,
        UI_Start_In = 5,
        UI_Cambio_Volumen_In = 6,
        UI_Holograma_Base_In = 7,
        UI_Holograma_Up_In = 8,
        UI_Conversion_In = 9,
        UI_Cursor_Holograma_In = 10,
        UI_Select_Holograma_In = 11,
        UI_Transicion_Holograma = 12,
        UI_Back = 13,
        CajaRandom
    }

    public enum Music
    {
        UI,
        Limbo,
        Egipto,
        Nordica
    }

    private Dictionary<Fx, string> fxDictionary;
    private Dictionary<Music, GameObject> musicGameObjects;
    private Dictionary<Music, string> musicDictionary;

    [Header("Orden: UI - Limbo - Egipto - Nordica")]
    public List<GameObject> gameObjectEvents;

    public List<GameObject> gameObjectHologramLoop;

    //TODO esto habrá que hacerlo mediante csv pero por ahora hacerlo a mano
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        fxDictionary = new Dictionary<Fx, string>();
        fxDictionary.Add(Fx.UI_MetallicButtonFocus,"UI_Cursor_In");
        fxDictionary.Add(Fx.UI_Panel_In,"UI_Panel_In");
        fxDictionary.Add(Fx.UI_Cortinilla_In,"UI_Cortinilla_In");
        fxDictionary.Add(Fx.UI_Select,"UI_Select");
        fxDictionary.Add(Fx.UI_Servos_In,"UI_Servos_In");
        fxDictionary.Add(Fx.UI_Start_In,"UI_Start_In");
        fxDictionary.Add(Fx.UI_Cambio_Volumen_In,"UI_Cambio_Volumen_In");
        fxDictionary.Add(Fx.UI_Holograma_Base_In,"UI_Holograma_Base_In");
        fxDictionary.Add(Fx.UI_Holograma_Up_In,"UI_Holaograma_Up_In");
        fxDictionary.Add(Fx.UI_Conversion_In,"UI_Conversion_In");
        fxDictionary.Add(Fx.UI_Cursor_Holograma_In,"Cursor_Holograma_In");
        fxDictionary.Add(Fx.UI_Select_Holograma_In,"UI_Conversion_In");
        fxDictionary.Add(Fx.UI_Transicion_Holograma,"UI_Transicion");
        fxDictionary.Add(Fx.UI_Back,"UI_Back_In");
        fxDictionary.Add(Fx.CajaRandom,"Caja_Random_In");
        
        musicGameObjects = new Dictionary<Music, GameObject>();
        musicGameObjects.Add(Music.UI,gameObjectEvents[0]);

        musicDictionary = new Dictionary<Music, string>();
        musicDictionary.Add(Music.UI, "Musica_UI");


    }

    public void PlayFx(int index)
    {
        AkSoundEngine.PostEvent(fxDictionary[(Fx)index], gameObject);
    }

    public void PlayFx(Fx fx)
    {
        AkSoundEngine.PostEvent(fxDictionary[fx], gameObject);
    }

    public void PlayFxHologram(Fx fx, int id)
    {
        AkSoundEngine.PostEvent(fxDictionary[fx], gameObjectHologramLoop[id]);
    }

    public void StopFxHologram(int id)
    {
        gameObjectHologramLoop[id].GetComponent<AkEvent>().Stop(0);
    }

    public void PlayLoop(Music music)
    {
        AkSoundEngine.PostEvent(musicDictionary[music], musicGameObjects[music]);
    } 

    public void StopLoop(Music music)
    {
        musicGameObjects[music].GetComponent<AkEvent>().Stop(0);
    }
}
