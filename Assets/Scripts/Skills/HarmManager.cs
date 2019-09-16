using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HarmManager : Singleton<HarmManager>
{
    public MyCarController anubisCar;
    public MyCarController poseidonCar;
    public MyCarController thorCar;
    public MyCarController kaliCar;

    private Coroutine anubisCoroutine;
    private Coroutine poseidonCoroutine;
    private Coroutine thorCoroutine;
    private Coroutine kaliCoroutine;

    public bool isAnubisWet;
    public bool isThorWet;
    public bool isKaliWet;

    public bool isAnubisMarked;
    public bool isThorMarked;
    public bool isPoseidonMarked;
    
    //Falta meter lo de la marca

    public bool isPoseidonStunned;
    public bool isAnubisStunned;
    public bool isThorStunned;

    public bool isPoseidonShielded;
    public bool isAnubisShielded;
    public bool isThorShielded;
    public bool isKaliShielded;

    public GameObject anubisShield;
    public GameObject poseidonShield;
    public GameObject thorShield;
    public GameObject kaliShield;

    public Action StartLocustPlagueDelegate;
    public Action FinishLocustPlagueDelegate;

    private bool isAnubisRecovering;
    private bool isThorRecovering;
    private bool isKaliRecovering;

    private void Awake()
    {
        isAnubisWet = false;
        isThorWet = false;
        isKaliWet = false;

        isAnubisMarked = false;
        isThorMarked = false;
        isPoseidonMarked = false;

        isPoseidonStunned = false;
        isAnubisStunned = false;
        isThorStunned = false;
    }

    public void RecoverSpeedTimer(God.Type god, float recoverSpeedTime, int speedAmount)
    {
        switch (god)
        {
            case God.Type.Anubis:
                anubisCoroutine = StartCoroutine(RecoverSpeedTimerCoroutine(god, recoverSpeedTime, speedAmount));
                break;

            case God.Type.Thor:
                thorCoroutine = StartCoroutine(RecoverSpeedTimerCoroutine(god, recoverSpeedTime, speedAmount));
                break;
            
            case God.Type.Kali:
                kaliCoroutine = StartCoroutine(RecoverSpeedTimerCoroutine(god, recoverSpeedTime, speedAmount));
                break;
        }
    }

    public void StopRecoverSpeedTimer(God.Type god)
    {
        switch (god)
        {
            case God.Type.Anubis:
                StopCoroutine(anubisCoroutine);
                break;

            case God.Type.Thor:
                StopCoroutine(thorCoroutine);
                break;
            
            case God.Type.Kali:
                StopCoroutine(kaliCoroutine);
                break;
        }
    }

    public void ReduceSpeed(God.Type god, int speedReduced)
    {
        switch (god)
        {
            case God.Type.Anubis:
                anubisCar.speedForce -= speedReduced;
                break;

            case God.Type.Thor:
                thorCar.speedForce -= speedReduced;
                break;
            
            case God.Type.Kali:
                kaliCar.speedForce -= speedReduced;
                break;
            
            case God.Type.Poseidon:
                poseidonCar.speedForce -= speedReduced;
                break;
        }
    }
    
    IEnumerator RecoverSpeedTimerCoroutine(God.Type god, float recoverSpeedTime, int speedReduced)
    {
        
        switch (god)
        {
            case God.Type.Anubis:
                isAnubisRecovering = true;
                break;

            case God.Type.Thor:
                isThorRecovering = true;
                break;
            
            case God.Type.Kali:
                isKaliRecovering = true;
                break;
        }
        
        yield return new WaitForSeconds(recoverSpeedTime);

        switch (god)
        {
            case God.Type.Anubis:
                anubisCar.speedForce += speedReduced;
                isAnubisRecovering = false;
                isAnubisWet = false;
                break;

            case God.Type.Thor:
                thorCar.speedForce += speedReduced;
                isThorRecovering = false;
                isThorWet = false;
                break;
            
            case God.Type.Kali:
                kaliCar.speedForce += speedReduced;
                isKaliRecovering = false;
                isKaliWet = false;
                break;
        }
    }

    public void DisableSkid(God.Type god, int amount, float time)
    {
        StartCoroutine(DisableSkidCoroutine(god, amount, time));
    }

    IEnumerator DisableSkidCoroutine(God.Type god, int amount, float time)
    {
        float original = 0;
        
        switch (god)
        {
            case God.Type.Anubis:
                original = anubisCar.speedForce;
                anubisCar.speedForce -= amount;

                if (anubisCar.speedForce < 0)
                    anubisCar.speedForce = 0;
                
                break;

            case God.Type.Poseidon:
                original = poseidonCar.speedForce;
                poseidonCar.speedForce -= amount;

                if (poseidonCar.speedForce < 0)
                    poseidonCar.speedForce = 0;
                break;
            
            case God.Type.Kali:
                original = kaliCar.speedForce;
                kaliCar.speedForce -= amount;

                if (kaliCar.speedForce < 0)
                    kaliCar.speedForce = 0;
                break;
        }

        yield return new WaitForSeconds(time);
        
        switch (god)
        {
            case God.Type.Anubis:
                anubisCar.speedForce = original;
                break;

            case God.Type.Poseidon:
                poseidonCar.speedForce = original;
                break;
            
            case God.Type.Kali:
                kaliCar.speedForce = original;
                break;
        }
    }

    public void ModifySpeedTemporally(God.Type god, int amount, float time)
    {
        StartCoroutine(ModifySpeedTemporallyCoroutine(god, amount, time));
    }

    IEnumerator ModifySpeedTemporallyCoroutine(God.Type god, int amount, float time)
    {
        float original = 0;
        
        switch (god)
        {
            case God.Type.Anubis:
                original = anubisCar.speedForce;
                anubisCar.speedForce += amount;
                break;
            
            case God.Type.Poseidon:
                original = poseidonCar.speedForce;
                poseidonCar.speedForce += amount;
                break;
            
            case God.Type.Thor:
                original = thorCar.speedForce;
                thorCar.speedForce += amount;
                break;
            
            case God.Type.Kali:
                original = kaliCar.speedForce;
                kaliCar.speedForce += amount;
                break;
        }

        yield return new WaitForSeconds(time);
        
        switch (god)
        {
            case God.Type.Anubis:
                anubisCar.speedForce = original;
                break;
            
            case God.Type.Poseidon:
                poseidonCar.speedForce = original;
                break;
            
            case God.Type.Thor:
                thorCar.speedForce = original;
                break;
            
            case God.Type.Kali:
                kaliCar.speedForce = original;
                break;
        }
    }

    public void MarkGod(God.Type god, float time)
    {
        StartCoroutine(MarkGodCoroutine(god, time));
    }

    IEnumerator MarkGodCoroutine(God.Type god, float time)
    {
        switch (god)
        {
            case God.Type.Anubis:
                isAnubisMarked = true;
                break;
            
            case God.Type.Poseidon:
                isPoseidonMarked = true;
                break;
            
            case God.Type.Thor:
                isThorMarked = true;
                break;
        }

        yield return new WaitForSeconds(time);
        
        switch (god)
        {
            case God.Type.Anubis:
                isAnubisMarked = false;
                break;
            
            case God.Type.Poseidon:
                isPoseidonMarked = false;
                break;
            
            case God.Type.Thor:
                isThorMarked = false;
                break;
        }
    }

    public void StunGod(God.Type god)
    {
        switch (god)
        {
            case God.Type.Anubis:

                GameObject anubis = anubisCar.gameObject;
                anubisCar.DisconnectCar();

                isAnubisStunned = true;
                Sequence sequenceA = DOTween.Sequence();
                sequenceA.Append(anubis.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear))
                    .Append(anubis.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear))
                    .Append(anubis.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear))
                    .OnComplete(() =>
                    {
                        anubisCar.ConnectCar();
                        isAnubisStunned = false;
                    });
                
                break;
            
            case God.Type.Poseidon:
                
                GameObject poseidon = poseidonCar.gameObject;
                poseidonCar.DisconnectCar();

                isPoseidonStunned = true;
                Sequence sequenceP = DOTween.Sequence();
                sequenceP.Append(poseidon.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .Append(poseidon.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .Append(poseidon.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .OnComplete(() =>
                    {
                        poseidonCar.ConnectCar();
                        isPoseidonStunned = false;
                    });
                
                break;
            
            case God.Type.Thor:
                
                GameObject thor = thorCar.gameObject;
                thorCar.DisconnectCar();

                isThorStunned = true;
                Sequence sequenceT = DOTween.Sequence();
                sequenceT.Append(thor.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .Append(thor.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .Append(thor.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
                    .OnComplete(() =>
                    {
                        thorCar.ConnectCar();
                        isThorStunned = false;
                    });
                
                break;
        }
    }

    public void RemoveShield(God.Type god)
    {
        switch (god)
        {
            case God.Type.Anubis:
                anubisShield.SetActive(false);
                isAnubisShielded = false;
                break;
            
            case God.Type.Poseidon:
                poseidonShield.SetActive(false);
                isPoseidonShielded = false;
                break;
            
            case God.Type.Thor:
                thorShield.SetActive(false);
                isThorShielded = false;
                break;
            
            case God.Type.Kali:
                kaliShield.SetActive(false);
                isKaliShielded = false;
                break;
        }
    }

    public void ActivateShield(God.Type god)
    {
        switch (god)
        {
            case God.Type.Anubis:
                anubisShield.SetActive(true);
                isAnubisShielded = true;
                break;
            
            case God.Type.Poseidon:
                poseidonShield.SetActive(true);
                isPoseidonShielded = true;
                break;
            
            case God.Type.Thor:
                thorShield.SetActive(true);
                isThorShielded = true;
                break;
            
            case God.Type.Kali:
                kaliShield.SetActive(true);
                isKaliShielded = true;
                break;
        }
    }

    public void RestoreSpeedByWater(float time, int speedReduced)
    {
        if (!isAnubisRecovering)
        {
            StartCoroutine(RecoverSpeedTimerCoroutine(God.Type.Kali, time, speedReduced));
        }

        if (!isThorRecovering)
        {
            StartCoroutine(RecoverSpeedTimerCoroutine(God.Type.Thor, time, speedReduced));
        }

        if (!isKaliRecovering)
        {
            StartCoroutine(RecoverSpeedTimerCoroutine(God.Type.Kali, time, speedReduced));
        }
    }

    public void ActivateThunder(God.Type god)
    {
        switch (god)
        {
            case God.Type.Anubis:
                anubisCar.transform.Find("Thunder").gameObject.SetActive(true);
                break;
            
            case God.Type.Poseidon:
                poseidonCar.transform.Find("Thunder").gameObject.SetActive(true);
                break;
            
            case God.Type.Kali:
                kaliCar.transform.Find("Thunder").gameObject.SetActive(true);
                break;
            
        }
    }

}
