using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiture {

    public float cash;
    public float gas;

    public bool noMoreCash;
    public bool noMoreGas;

    public bool IsRunning;

    public Voiture(float cash, float gas)
    {
        this.cash = cash;
        this.gas = gas;
        IsRunning = false;
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
        if (gas == 0)
            noMoreGas = true;
    }
}
