using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PublicSingleton<GameManager> {

    public Voiture car;
    
    public float startCash;
    public float startGas;
    public float startFood;

    void Start ()
    {
        car = new Voiture(startCash, startGas, startFood);
	}
}
