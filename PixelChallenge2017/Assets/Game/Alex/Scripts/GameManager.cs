using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PublicSingleton<GameManager> {

    public Voiture car;
    
    public float startCash;
    public float startGas;
    public float startFood;

    public float TowingCost;

    void Start ()
    {
        car = new Voiture(startCash, startGas);

        // Ajouts de la route initial
        EventScripting.Init(car);
        RoadManager.instance.onDestinationReached.AddListener(OnDestinationReached);
    }

    void Update()
    {
        // Condition de defaite ?
        if (car.noMoreGas)
        {
            if (car.cash < TowingCost)
            {
                // End of the game
                print("GAME OVER");
            }
            // Towing
            car.ChangeCash(TowingCost);
        }
    }

    public void OnDestinationReached()
    {
        EventScripting.NextEvents(car);
    }

    public void CreateStop(GameObject stop)
    {
        // Gere le spaw du prefab stop et set toute le reste
    }
}
