using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class destination : MonoBehaviour
{
    public Text zoneDest;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        RoadManager rd = RoadManager.instance;
        int montant = (int)Mathf.Floor(rd.currentRoad.distance - rd.currentDistance);
        if (montant < 0)
            montant = 0;
        zoneDest.text = rd.currentRoad.currentDestination.nom + " dans " + montant + " km";
    }
}
