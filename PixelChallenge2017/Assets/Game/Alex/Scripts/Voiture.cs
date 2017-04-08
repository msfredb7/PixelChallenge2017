using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voiture {

    public float cash;
    public float gas;
    public float maxGas = 25;

    public bool noMoreCash;

    public bool IsRunning;

    public List<Item> listItems = new List<Item>();
    public List<Quest.ItemQuest> listSpecialItems = new List<Quest.ItemQuest>();
    public UnityEvent onDie = new UnityEvent();


    public Item getItemOfType(string objName)
    {
        Item ret = null;

        for (int i = 0; i < listItems.Count; i++)
        {
            if (listItems[i].nom == objName)
                ret = listItems[i];
        }

        return ret;
    }

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
        CashToaster.instance.SpawnAmount((int)amount);
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
