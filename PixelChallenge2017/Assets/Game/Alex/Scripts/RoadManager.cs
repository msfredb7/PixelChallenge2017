﻿using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoadManager : PublicSingleton<RoadManager> {

    public Road currentRoad;

    public UnityEvent onDestinationReached = new UnityEvent();
    public UnityEvent onStopReached = new UnityEvent();

    [HideInInspector]
    public float startTime = 0;
    [HideInInspector]
    public float timeToIgnore = 0;
    [HideInInspector]
    public float timeLastStop = 0;

    public int lastPrint = 0;

    public GameObject buyButton;

    public float currentDistance;

    void Update()
    {
        if (currentRoad == null)
        {
            print("Ain't going anywhere bruh");
            return;
        }

        if (GameManager.instance.car.IsRunning)
        {
            currentDistance = (Time.time - startTime) - timeToIgnore; // 1km = 1 secondes

            if(currentDistance > (lastPrint + 1))
            {
                GameManager.instance.car.ChangeGas(-1);
                print("They see me rollin' ! They hatin' ( Distance : " + lastPrint + "km )");
                lastPrint++;
            }

            Stop nextStop = currentRoad.GetNextStop(currentDistance);
            SpecialEvent nextEvent = currentRoad.GetNextSpecialEvent(currentDistance);
            ItemEvent nextItem = currentRoad.GetNextItem(currentDistance);
            Quest nextQuest = currentRoad.GetNextQuest(currentDistance);

            if (nextStop != null && nextStop.distance <= currentDistance)
            {
                GameManager.instance.car.IsRunning = false;
                timeLastStop = Time.time;
                nextStop.StartEvent();
                currentRoad.currentStop = nextStop;
                onStopReached.Invoke();
                if(nextStop.lieu != LieuType.arretBus)
                    buyButton.SetActive(true);
                DelayManager.CallTo(StopEnd, 6);

                // On a fini de traiter l'evennement, on le supprime
                currentRoad.RemoveStop(nextStop);
            }

            if (nextEvent != null && nextEvent.distance <= currentDistance)
            {
                nextEvent.StartEvent();

                currentRoad.RemoveSpecialEvent(nextEvent);
            }

            if (nextItem != null && nextItem.distance <= currentDistance)
            {
                nextItem.StartEvent();

                currentRoad.RemoveItem(nextItem);
            }

            if (nextQuest != null && nextQuest.distance <= currentDistance)
            {
                QuestManager.instance.AddQuest(nextQuest);

                currentRoad.RemoveQuestEvent(nextQuest);
            }

            if (currentRoad.distance <= currentDistance)
            {
                print("Welcome to the amazing city of " + currentRoad.currentDestination.nom);

                switch (currentRoad.currentDestination.nom)
                {
                    case "Montreal":
                        GlobalAnimator.StopAt(LieuType.Montreal, delegate ()
                        {
                            ContinueRoadTrip();
                        });
                        break;
                    case "Trois-Riviere":
                        GlobalAnimator.StopAt(LieuType.TroisRiviere, delegate ()
                        {
                            ContinueRoadTrip();
                        });
                        break;
                    case "Quebec":
                        GlobalAnimator.StopAt(LieuType.Quebec, delegate ()
                        {
                            ContinueRoadTrip();
                        });
                        break;
                    case "Saguenay":
                        GlobalAnimator.StopAt(LieuType.Saguenay, delegate ()
                        {
                            ContinueRoadTrip();
                        });
                        break;
                    case "Sept-Iles":
                        GlobalAnimator.StopAt(LieuType.SeptIles, delegate ()
                        {
                            ContinueRoadTrip();
                        });
                        break;
                }

                // clean up
                GameManager.instance.car.IsRunning = false;

                onDestinationReached.Invoke();
            }
        }
    }

    public void StopEnd()
    {
        buyButton.SetActive(false);
    }

    public void SetRoad(Road road)
    {
        GameManager.instance.car.IsRunning = true;
        currentRoad = road;
        startTime = Time.time;
        timeToIgnore = 0;
        print("On part de " + road.currentDepart.nom);
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
