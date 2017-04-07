using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptAlex : MonoBehaviour {

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Ville depart = new Ville("Montreal");
            Ville destination = new Ville("Chicoutimi");
            Road newRoad = new Road(depart, destination, null, null, null, 5);
            RoadManager.instance.SetRoad(newRoad);
        }
	}
}
