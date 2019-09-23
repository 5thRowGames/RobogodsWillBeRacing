using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour, IControllable
{
    public God.Type god;
    
    [Header("Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3")]
    public int positionInList; //Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3
    private int currentItemID; //-1 si no hay ningún objeto a usar

    public PlayerSkillManager playerSkillManager;

    [Header("Item resources")] 
    public float coldownReduced;
    public int manaStolen;
    public float turboRecharged;

    private Dictionary<int, List<int>> itemPercentageDictionary;
    private bool isItemChosen;

    public int itemToChooser = 0;

    private void Awake()
    {
        playerSkillManager = GetComponent<PlayerSkillManager>();

        int itemNumber =  Enum.GetValues(typeof(Item.Type)).Length;

        var file = Resources.Load<TextAsset>("ItemPercentage");
        string[] data = file.text.Split('\n');
        
        itemPercentageDictionary = new Dictionary<int, List<int>>();

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');
            List<int> itemList = new List<int>();
            
            for (int j = 0; j < itemNumber; j++)
            {
                itemList.Add(Int32.Parse(row[j + 1]));
            }

            itemPercentageDictionary[i - 1] = itemList;
        }

        isItemChosen = false;
    }

    private void OnEnable()
    {
        ConnectDisconnectManager.ConnectItemManagerDelegate += ConnectItemManager;
        ConnectDisconnectManager.DisconnectItemManagerDelegate += DisconnectItemManager;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectItemManagerDelegate -= ConnectItemManager;
        ConnectDisconnectManager.DisconnectItemManagerDelegate -= DisconnectItemManager;
    }

    public void Control(IDevice device)
    {
        if (device.State.Action.IsPressed)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        HUDManager.Instance.RemoveCurrentItem(god);
        isItemChosen = false;

        Debug.Log(currentItemID);

        switch (currentItemID)
        {
            //Reloj de arena
            case 0:
                ReduceColdown();
                Debug.Log("Activo reloj");
                break;
            
            //Imán
            case 1:
                IncreaseMana();
                Debug.Log("Incremento mana");
                break;
            
            //Bidón de recarga
            case 2:
                RechargeTurbo();
                Debug.Log("Recargo turbo");
                break;
            
            //Escudo de energía
            case 3:
                ActivateShield();
                Debug.Log("Activo escudo");
                break;
            
            //Teleport
            case 4:
                Debug.Log("Activo teleport");
                break;
        }
        currentItemID = -1;
    }

    IEnumerator ChooseItemCoroutine()
    {
        int itemNumber =  Enum.GetValues(typeof(Item.Type)).Length;
        int random = Random.Range(0, 100);
        int itemChosen = 0;

        int amount = 0;
        
        for (int i = 0; i < itemNumber; i++)
        {
            amount += itemPercentageDictionary[positionInList][i];

            if (amount >= random)
            {
                itemChosen = i;
                break;
            }
                
        }

        yield return HUDManager.Instance.ChooseItemUI(god,itemChosen);
        currentItemID = itemChosen;
    }

    private void ReduceColdown()
    {
        HUDManager.Instance.ReduceTime(god,coldownReduced);
    }

    private void IncreaseMana()
    {
        playerSkillManager.Mana += manaStolen;
        
        HUDManager.Instance.UpdateManaBar(god,manaStolen);

        float distance = Mathf.Infinity;
        int index = LapsManager.Instance.godRaceInfoList.FindIndex(x => x.god == gameObject);
        int enemyIndex = 0;
        
        if (index != -1)
        {
            for (int i = 0; i < LapsManager.Instance.godRaceInfoList.Count; i++)
            {
                if (i != index)
                {
                    float newDistance = (LapsManager.Instance.godRaceInfoList[i].god.transform.position - LapsManager.Instance.godRaceInfoList[index].god.transform.position).sqrMagnitude;

                    if (newDistance < distance)
                    {
                        enemyIndex = i;
                        distance = newDistance;
                    }
                }
            }
        }

        LapsManager.Instance.godRaceInfoList[enemyIndex].god.GetComponent<PlayerSkillManager>().Mana -= manaStolen;
        
        HUDManager.Instance.UpdateManaBar((God.Type)enemyIndex,manaStolen);
    }

    private void RechargeTurbo()
    {
        switch (god)
        {
            case God.Type.Anubis:
                HarmManager.Instance.anubisCar.Turbo += turboRecharged;
                break;
            
            case God.Type.Poseidon:
                HarmManager.Instance.poseidonCar.Turbo += turboRecharged;
                break;
            
            case God.Type.Kali:
                HarmManager.Instance.kaliCar.Turbo += turboRecharged;
                break;
            
            case God.Type.Thor:
                HarmManager.Instance.thorCar.Turbo += turboRecharged;
                break;
        }
    }

    private void ActivateShield()
    {
        HarmManager.Instance.ActivateShield(god);
    }

    public void ChooseItem()
    {
        if (!isItemChosen)
        {
            isItemChosen = true;
            StartCoroutine(ChooseItemCoroutine());
            
            //Sonido: Elección de un objeto
            AkSoundEngine.PostEvent("Caja_Random_In", gameObject);
        }
    }

    private void ConnectItemManager()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(), this);
    }

    private void DisconnectItemManager()
    {
        Core.Input.UnassignControllable(GetComponent<IncontrolProvider>(), this);
    }
}
