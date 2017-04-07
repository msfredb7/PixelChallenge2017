using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScripting {

    public static int currentEvent = 0;

	public static void Init(Voiture car)
    {
        Ville depart = new Ville("St-Stanislas");
        Ville destination = new Ville("Montreal");

        // Evennement a faire...

        Road newRoad = new Road(depart, destination, null, null, null, 5);
        RoadManager.instance.SetRoad(newRoad);
    }

    public static void NextEvents(Voiture car)
    {
        switch (currentEvent)
        {
            case 0:
                currentEvent++;
                break;
            case 1:
                currentEvent++;
                break;
            case 2:
                currentEvent++;
                break;
            case 3:
                currentEvent++;
                break;
        }
    }
}
