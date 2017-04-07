using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : PublicSingleton<RoadManager> {

    public Road currentRoad;

    [HideInInspector]
    public float startTime;

    void Update()
    {
        if (currentRoad == null)
        {
            print("Ain't going anywhere bruh");
            return;
        }

        float currentDistance = Time.time - startTime; // 1km = 1 secondes

        print("They see me rollin' ! They hatin' ...( Distance : " + currentDistance);

        Stop nextStop = currentRoad.GetNextStop(currentDistance);
        SpecialEvent nextEvent = currentRoad.GetNextSpecialEvent(currentDistance);
        ItemEvent nextItem = currentRoad.GetNextItem(currentDistance);

        if (nextStop != null && nextStop.distance <= currentDistance)
        {
            nextStop.StartEvent();
        }

        if (nextEvent != null && nextEvent.distance <= currentDistance)
        {
            nextEvent.StartEvent();
        }

        if (nextItem != null && nextItem.distance <= currentDistance)
        {
            nextItem.StartEvent();
        }

        if(currentRoad.distance <= currentDistance)
        {
            print("Welcome to the amazing city of " + currentRoad.currentDestination.nom);
        }
    }

    public void SetRoad(Road road)
    {
        print("Lets go boys n grills");

        currentRoad = road;
        startTime = Time.time;
    }
}
