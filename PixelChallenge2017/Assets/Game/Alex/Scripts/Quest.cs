using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    public class Destination
    {
        public Ville ville;
        public Stop stop;

        public Destination(Ville ville, Stop stop)
        {
            if (ville != null)
                this.ville = ville;
            if (stop != null && ville == null)
                this.stop = stop;
        }

        public bool DestinationIsCity()
        {
            return (stop == null) && (ville != null);
        }

        public bool DestinationIsStop()
        {
            return (stop != null) && (ville == null);
        }
    }

    public float distance;

    public string questDescription;

    public Destination destination;
    public List<Item> itemNecessaire = new List<Item>();

    public Quest(string questDescription, float distance, Destination destination, List<Item> itemNecessaire = null)
    {
        this.questDescription = questDescription;
        this.distance = distance;
        this.destination = destination;
        if(itemNecessaire != null)
            this.itemNecessaire = itemNecessaire;
        if(destination.DestinationIsStop())
            RoadManager.instance.onDestinationReached.AddListener(OnCityReached);
        else if(destination.DestinationIsCity())
            RoadManager.instance.onStopReached.AddListener(OnStopReached);
    }

    public void OnBegin()
    {

    }

    public void OnCityReached()
    {

    }

    public void OnStopReached()
    {

    }

    public void OnFail()
    {

    }

    public void OnComplete()
    {

    }
}
