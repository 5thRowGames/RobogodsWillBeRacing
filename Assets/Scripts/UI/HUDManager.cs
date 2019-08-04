using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    public float chooseItemTimer;
    public List<Sprite> itemIcon;
    public Image currentItem;

    public IEnumerator ChooseItemUI(int itemChosen)
    {
        float timer = chooseItemTimer;
        int i = 0;
        int total = itemIcon.Count;

        while (timer > 0)
        {
            currentItem.sprite = itemIcon[i];
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
            i++;

            if (i == total)
                i = 0;
        }

        currentItem.sprite = itemIcon[itemChosen];
    }

    public void RemoveCurrentItem()
    {
        currentItem.sprite = null;
    }
}
