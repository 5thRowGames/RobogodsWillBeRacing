using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class SoundManager : SingletonDontDestroy<SoundManager>
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
        UI_Conversion_Out = 14,
        CajaRandom = 15,
        Inicio_Carrera = 16,
        Banda_Aceleracion = 17
    }

    public enum Music
    {
        None,
        UI,
        Portal_Out,
        Inicio,
        Limbo,
        Egipto,
        Nordica,
        India,
        Griega
    }

    private Dictionary<Fx, string> fxDictionary;
    private Dictionary<Music, string> musicDictionary;

    private Coroutine portal_out = null;

    private Music currentMusic;
    public Music CurrentMusic => currentMusic;

    [Header("Orden: UI - Limbo - Egipto - Nordica")]
    public List<GameObject> gameObjectEvents;
    public List<GameObject> gameObjectHologramLoop;

    //TODO esto habrá que hacerlo mediante csv pero por ahora hacerlo a mano
    private void Awake()
    {
        currentMusic = Music.None;
        
        base.Awake();
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
        fxDictionary.Add(Fx.UI_Conversion_Out,"UI_Conversion_Out");
        fxDictionary.Add(Fx.CajaRandom,"Caja_Random_In");
        fxDictionary.Add(Fx.Inicio_Carrera,"Inicio_Carrera_In");
        fxDictionary.Add(Fx.Banda_Aceleracion, "Aceleraciones_In");

        musicDictionary = new Dictionary<Music, string>();
        musicDictionary.Add(Music.UI, "Musica_UI");
        musicDictionary.Add(Music.Portal_Out, "Portal_Out");
        musicDictionary.Add(Music.Inicio, "Musica_Inicio");
        musicDictionary.Add(Music.Limbo, "Musica_Limbo");
        musicDictionary.Add(Music.Egipto, "Musica_Egipto");
        musicDictionary.Add(Music.India, "Musica_India");
        musicDictionary.Add(Music.Nordica, "Musica_Nordica");
        musicDictionary.Add(Music.Griega, "Musica_Agua");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayLoop(Music.UI);
        }
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
        if (!musicDictionary.ContainsKey(music)) return;
        currentMusic = music;
        AkSoundEngine.PostEvent(musicDictionary[music], gameObjectEvents[0]);
    }

    public void StopLoop()
    {
        currentMusic = Music.None;
        gameObjectEvents[0].GetComponent<AkEvent>().Stop(0);
    }

    //A petición de Eduardo hay que retrasar dos segundos la llamada
    public void DelayPortalOutSound()
    {
        if (portal_out == null)
        {
            portal_out = StartCoroutine(DelayPortalOut());
        }
    }

    IEnumerator DelayPortalOut()
    {
        yield return new WaitForSeconds(2f);
        PlayLoop(Music.Portal_Out);
        portal_out = null;
    }
}
