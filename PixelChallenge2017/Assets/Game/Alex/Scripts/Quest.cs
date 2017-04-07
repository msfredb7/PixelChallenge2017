using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    public class ItemQuest
    {
        public float reward;
        public Item item;

        public ItemQuest(Item item, float reward)
        {
            this.reward = reward;
            this.item = item;
        }
    }

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

    public float recompense;

    public string questDescription;

    public Destination destination;
    public List<ItemQuest> itemNecessaire = new List<ItemQuest>();

    public Quest(string questDescription, float distance, float recompense, Destination destination, List<ItemQuest> itemNecessaire = null)
    {
        this.questDescription = questDescription;
        this.distance = distance;
        this.destination = destination;
        this.recompense = recompense;
        if(itemNecessaire != null)
            this.itemNecessaire = itemNecessaire;
        if(destination.DestinationIsStop())
            RoadManager.instance.onStopReached.AddListener(OnStopReached);
        else if(destination.DestinationIsCity())
            RoadManager.instance.onDestinationReached.AddListener(OnCityReached);
    }

    public void OnBegin()
    {
        Debug.Log("la quete commence");
    }

    public void OnCityReached()
    {
        if (RoadManager.instance.currentRoad.currentDestination == destination.ville)
            OnComplete();
    }


    public void OnStopReached()
    {
        if (RoadManager.instance.currentRoad.currentStop == destination.stop)
            OnComplete();
    }

    public void OnFail()
    {
        Debug.Log("la quete a fail");
    }

    public void OnComplete()
    {
        Debug.Log("la quete est une reussite!");

        GameManager.instance.car.ChangeCash(recompense);

        for(int j = 0; j < GameManager.instance.car.listSpecialItems.Count; j++)
        {
            for (int i = 0; i < GameManager.instance.car.listItems.Count; i++)
            {
                if (GameManager.instance.car.listSpecialItems[j].item == GameManager.instance.car.listItems[i])
                    GameManager.instance.car.listItems.Remove(GameManager.instance.car.listItems[i]);
            }
            GameManager.instance.car.ChangeCash(GameManager.instance.car.listSpecialItems[j].reward);
            GameManager.instance.car.listSpecialItems.Remove(GameManager.instance.car.listSpecialItems[j]);
        }
    }
}
