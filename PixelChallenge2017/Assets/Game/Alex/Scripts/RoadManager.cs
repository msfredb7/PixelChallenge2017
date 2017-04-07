using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoadManager : PublicSingleton<RoadManager> {

    public Road currentRoad;

    public UnityEvent onDestinationReached = new UnityEvent();

    [HideInInspector]
    public float startTime = 0;
    [HideInInspector]
    public float timeToIgnore = 0;
    [HideInInspector]
    public float timeLastStop = 0;

    void Update()
    {
        if (currentRoad == null)
        {
            print("Ain't going anywhere bruh");
            return;
        }

        if (GameManager.instance.car.IsRunning)
        {
            float currentDistance = (Time.time - startTime) - timeToIgnore; // 1km = 1 secondes

            //print("They see me rollin' ! They hatin' ...( Distance : " + currentDistance);

            Stop nextStop = currentRoad.GetNextStop(currentDistance);
            SpecialEvent nextEvent = currentRoad.GetNextSpecialEvent(currentDistance);
            ItemEvent nextItem = currentRoad.GetNextItem(currentDistance);
            Quest nextQuest = currentRoad.GetNextQuest(currentDistance);

            if (nextStop != null && nextStop.distance <= currentDistance)
            {
                nextStop.onEventComplete.AddListener(ContinueRoadTrip);
                GameManager.instance.car.IsRunning = false;
                timeLastStop = Time.time;
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

            if (nextQuest != null && nextQuest.distance <= currentDistance)
            {
                QuestManager.instance.AddQuest(nextQuest);
            }

            if (currentRoad.distance <= currentDistance)
            {
                print("Welcome to the amazing city of " + currentRoad.currentDestination.nom);
                onDestinationReached.Invoke();
            }
        }
    }

    public void SetRoad(Road road)
    {
        print("Lets go boys n grills");
        GameManager.instance.car.IsRunning = true;
        currentRoad = road;
        startTime = Time.time;
    }

    public void ContinueRoadTrip()
    {
        GameManager.instance.car.IsRunning = true;
        timeToIgnore += Time.time - timeLastStop;
    }

    public bool IsArrived()
    {
        return currentRoad.distance <= ((Time.time - startTime) - timeToIgnore);
    }
}
