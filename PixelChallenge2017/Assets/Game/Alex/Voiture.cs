using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiture {

    public float cash;
    public float gas;
    public float food;

    public bool noMoreCash;
    public bool noMoreGas;

    public Voiture(float cash, float essence, float food)
    {
        this.cash = cash;
        this.gas = essence;
        this.food = food;
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

    public void ChangeFood(float amount)
    {

    }
}
