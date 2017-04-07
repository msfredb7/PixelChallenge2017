using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road {

    public Ville currentDepart;
    public Ville currentDestination;

    public float distance; // distance entre depart et destination (temps de la route)

    public List<Stop> stopList = new List<Stop>();

    public List<SpecialEvent> specialEventList = new List<SpecialEvent>();

    public List<ItemEvent> itemEventList = new List<ItemEvent>();

    public Road(Ville depart, Ville destination, List<Stop> stopList, List<SpecialEvent> specialEventList, List<ItemEvent> itemEventList, float distance)
    {
        currentDepart = depart;
        currentDestination = destination;
        this.stopList = stopList;
        this.specialEventList = specialEventList;
        this.itemEventList = itemEventList;
        this.distance = distance;
    }

    public ItemEvent GetNextItem(float distance)
    {
        if (itemEventList == null)
            return null;

        ItemEvent resultEvent = null;

        for(int i = 0; i < itemEventList.Count; i++)
        {
            if (resultEvent == null)
                resultEvent = itemEventList[i];
            if (itemEventList[i].distance > resultEvent.distance)
                resultEvent = itemEventList[i];
        }
        return resultEvent;
    }

    public SpecialEvent GetNextSpecialEvent(float distance)
    {
        if (specialEventList == null)
            return null;

        SpecialEvent resultEvent = null;

        for (int i = 0; i < specialEventList.Count; i++)
        {
            if (resultEvent == null)
                resultEvent = specialEventList[i];
            if (itemEventList[i].distance > resultEvent.distance)
                resultEvent = specialEventList[i];
        }
        return resultEvent;
    }

    public Stop GetNextStop(float distance)
    {
        if (stopList == null)
            return null;

        Stop resultEvent = null;

        for (int i = 0; i < stopList.Count; i++)
        {
            if (resultEvent == null)
                resultEvent = stopList[i];
            if (itemEventList[i].distance > resultEvent.distance)
                resultEvent = stopList[i];
        }
        return resultEvent;
    }
}
