using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Structure de donnees pour les arrets (ville, snack, depaneur)
public class Stop : MonoBehaviour {

    public float distance;

    public Stop(float distance)
    {
        this.distance = distance;
    }
}
