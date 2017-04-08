using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoadManager : PublicSingleton<RoadManager>
{

    public Road currentRoad;

    public UnityEvent onQuitCity = new UnityEvent();
    public UnityEvent onStopReached = new UnityEvent();
    public UnityEvent onDestinationReached = new UnityEvent();
    public UnityEvent onLateDestinationReached = new UnityEvent();
    public UnityEvent onLateStopReached = new UnityEvent();

    public float startTime = -1;
    
    public float timeToIgnore = 0;
    
    public float timeLastStop = 0;

    public int lastPrint = 0;

    public float currentDistance;

    public bool start = true;

    public bool onestauneville = false;

    void Update()
    {
        if (!start)
            return;
        if (startTime <= 0)
            startTime = Time.time;
         
        if (currentRoad == null)
        {
            print("Ain't going anywhere bruh");
            return;
        }

        if (GameManager.instance.car.IsRunning)
        {
            currentDistance = (Time.time - startTime) - timeToIgnore; // 1km = 1 secondes
            GameManager.instance.car.ChangeGas(-Time.deltaTime);
            if (!GameManager.instance.car.IsRunning) // on vient de faire un arret
                return;

            if (currentDistance > (lastPrint + 1))
            {
                //print("They see me rollin' ! They hatin' ( Distance : " + currentDistance + "km )");
                lastPrint++;
            }

            Stop nextStop = currentRoad.GetNextStop(currentDistance);
            SpecialEvent nextEvent = currentRoad.GetNextSpecialEvent(currentDistance);
            ItemEvent nextItem = currentRoad.GetNextItem(currentDistance);
            Quest nextQuest = currentRoad.GetNextQuest(currentDistance);

            if (nextStop != null && nextStop.distance <= currentDistance)
            {
                GameManager.instance.car.IsRunning = false; // On arrête d'avancer

                timeLastStop = Time.time; // On garde une notion du temps perdu

                
                nextStop.StartEvent(null, delegate ()
                {
                    onLateStopReached.Invoke();
                }); // On débute l'evennement du stop
                
                currentRoad.currentStop = nextStop; // Le stop courrant est maintenant ce stop

                onStopReached.Invoke(); // Activation des events de quêtes qui se deroule a un stop

                // Dans 6 secondes, on aura fini d'être au stop et on reprendra la route.
                //DelayManager.CallTo(StopEnd, 6);

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
                QuestManager.instance.AddQuest(nextQuest, true);

                currentRoad.RemoveQuestEvent(nextQuest); // On retire la quête des évennements a faire dans la route
            }

            if (currentRoad.distance <= currentDistance)
            {
                print("Welcome to the amazing city of " + currentRoad.currentDestination.nom);
                onestauneville = true;
                onDestinationReached.Invoke();
                switch (currentRoad.currentDestination.nom)
                {
                    case "Montreal":
                        GameManager.instance.car.IsRunning = false;
                        GlobalAnimator.StopAt(LieuType.Montreal, delegate ()
                        {
                            onQuitCity.Invoke();
                        }, delegate ()
                        {
                            onLateDestinationReached.Invoke();
                        });
                        break;
                    case "Trois-Riviere":
                        GameManager.instance.car.IsRunning = false;
                        GlobalAnimator.StopAt(LieuType.TroisRiviere, delegate ()
                        {
                            onQuitCity.Invoke();
                        }, delegate ()
                        {
                            onLateDestinationReached.Invoke();
                        });
                        break;
                    case "Quebec":
                        GameManager.instance.car.IsRunning = false;
                        GlobalAnimator.StopAt(LieuType.Quebec, delegate ()
                        {
                            onQuitCity.Invoke();
                        }, delegate ()
                        {
                            onLateDestinationReached.Invoke();
                        });
                        break;
                    case "Saguenay":
                        GameManager.instance.car.IsRunning = false;
                        GlobalAnimator.StopAt(LieuType.Saguenay, delegate ()
                        {
                            onQuitCity.Invoke();
                        }, delegate ()
                        {
                            onLateDestinationReached.Invoke();
                        });
                        break;
                    case "Sept-Iles":
                        GameManager.instance.car.IsRunning = false;
                        GlobalAnimator.StopAt(LieuType.SeptIles, delegate ()
                        {
                            onQuitCity.Invoke();
                        }, delegate ()
                        {
                            onLateDestinationReached.Invoke();
                        });
                        break;
                }
            }
        }
    }

    // La fin d'un Stop
    public void StopEnd()
    {
        //GameManager.instance.car.IsRunning = true;
        if(!onestauneville)
            timeToIgnore += Time.time - timeLastStop;
        onestauneville = false;
    }

    // Permet de définir la route courante à suivre
    public void SetRoad(Road road)
    {
        GameManager.instance.car.IsRunning = true;
        currentRoad = road;
        startTime = Time.time;
        timeToIgnore = 0;
        print("On part de " + road.currentDepart.nom);
    }
}
