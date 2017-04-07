using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiture {

    public float cash;
    public float gas;
    public float maxGas = 50;

    public bool noMoreCash;
    public bool noMoreGas;

    public bool IsRunning;

    public List<Item> listItems = new List<Item>();
    public List<Quest.ItemQuest> listSpecialItems = new List<Quest.ItemQuest>();

    public Voiture(float cash, float gas)
    {
        this.cash = cash;
        this.gas = gas;
        IsRunning = false;
        OilDisplay.UpdateOil(gas / maxGas);
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
        gas += amount;
        if (gas < 0)
            gas = 0;
        if (gas > maxGas)
            gas = maxGas;
        if (gas == 0)
            noMoreGas = true;

        OilDisplay.UpdateOil(gas / maxGas);
    }
}
