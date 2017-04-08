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
        zoneDest.text = rd.currentRoad.currentDestination.nom + " dans " + Mathf.Floor(rd.currentRoad.distance - rd.currentDistance) + " km";
    }
}
