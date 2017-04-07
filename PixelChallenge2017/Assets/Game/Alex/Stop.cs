using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Structure de donnees pour les arrets (ville, snack, depaneur)
public class Stop {

    public stopType type;

    public float distance;

    public enum stopType
    {
        Snack = 1,
        Essence = 2
    }

    public Stop(stopType type, float distance)
    {
        this.type = type;
        this.distance = distance;
    }
}
