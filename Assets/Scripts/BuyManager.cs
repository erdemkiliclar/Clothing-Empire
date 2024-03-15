using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public int moneyCount = 0;
    private void OnEnable()
    {
        TriggerManager.OnMoneyCollect += IncreaseMoney;
    }

    private void OnDisable()
    {
        TriggerManager.OnClothCollect -= IncreaseMoney;
    }

    void IncreaseMoney()
    {
        moneyCount += 50;
    }
}
