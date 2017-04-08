using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voiture {

    public float cash;
    public float gas;
    public float maxGas = 50;

    public bool noMoreCash;

    public bool IsRunning;

    public List<Item> listItems = new List<Item>();
    public List<Quest.ItemQuest> listSpecialItems = new List<Quest.ItemQuest>();
    public UnityEvent onDie = new UnityEvent();

    public Voiture(float cash, float gas)
    {
        this.cash = cash;
        this.gas = gas;
        IsRunning = false;
        OilDisplay.UpdateOil(gas / maxGas);
    }

    public void Repair()
    {
        ChangeGas(maxGas);
    }

    public void ChangeCash(float amount) // peut etre negatif
    {
        cash += amount;
        if (cash < 0)
            cash = 0;
        if(cash == 0)
            noMoreCash = true;
    }

    public void ChangeGas(float amount)
    {
        bool wasOutOfGas = gas <= 0;

        //Debug.Log("Chat " + gas);

        gas += amount;
        if (gas < 0)
            gas = 0;
        else if (gas > maxGas)
            gas = maxGas;
        
        if (gas == 0)
        {
            onDie.Invoke();
        }
        else if(wasOutOfGas)
        {
            GlobalAnimator.Restart();
            PewDiePieUI.instance.repairButton.gameObject.SetActive(false);
        }

        OilDisplay.UpdateOil(gas / maxGas);
    }
}
