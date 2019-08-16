using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour, IControllable
{
    //Hay que establecer un orden por ahora: 0 anubis, 1 poseidon 2 thor 3 kali
    public int godID; 
    public static int position;
    private int currentItemID; //-1 si no hay ningún objeto a usar

    public PlayerSkillManager playerSkillManager;
    public MyCarController carController;
    public GameObject energyShield;

    [Header("Item resources")] 
    public float coldownReduced;
    public float manaStolen;
    public float turboRecharged;

    private Dictionary<int, List<int>> itemPercentageDictionary;
    private bool isItemChosen;

    private void Awake()
    {
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
        currentItemID = -1;
        HUDManager.Instance.RemoveCurrentItem();
        isItemChosen = false;
        
        switch (currentItemID)
        {
            //Reloj de arena
            case 0:
                ReduceColdown();
                break;
            
            //Imán
            case 1:
                IncreaseMana();
                break;
            
            //Bidón de recarga
            case 2:
                RechargeTurbo();
                break;
            
            //Escudo de energía
            case 3:
                ActivateShield();
                break;
            
            //LLave inglesa
            case 4:
                break;
        }
    }

    IEnumerator ChooseItem()
    {
        int itemNumber =  Enum.GetValues(typeof(Item.Type)).Length;
        int random = Random.Range(0, 100);
        int itemChosen = 0;

        int amount = 0;
        
        for (int i = 0; i < itemNumber; i++)
        {
            amount += itemPercentageDictionary[position][i];

            if (amount >= random)
                itemChosen = i;
        }

        yield return HUDManager.Instance.ChooseItemUI(itemChosen);

        currentItemID = itemChosen;

    }

    private void ReduceColdown()
    {
        playerSkillManager.GodSkillTimer -= coldownReduced;
    }

    private void IncreaseMana()
    {
        playerSkillManager.Mana += manaStolen;
        
        int closestGod = RacePositionManager.Instance.ClosestGod(godID);

        RacePositionManager.Instance.godPosition[closestGod].GetComponent<PlayerSkillManager>().Mana -= manaStolen;
    }

    private void RechargeTurbo()
    {
        carController.TurboAmount += turboRecharged;
    }

    private void ActivateShield()
    {
        energyShield.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemBox") && !isItemChosen)
        {
            isItemChosen = true;
            StartCoroutine(ChooseItem());
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
