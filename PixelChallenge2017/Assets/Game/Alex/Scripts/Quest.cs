using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    // Un item ayant une recompense donnée a un certain moment
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

    // Une destination pouvant etre une ville ou un stop
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

    public float timeDestination;

    public Destination destination;
    public List<ItemQuest> itemNecessaire = new List<ItemQuest>();
    public Personne personne;

    public Quest(string questDescription, float distance, float recompense, Personne personne , Destination destination, List<ItemQuest> itemNecessaire = null)
    {
        this.questDescription = questDescription;
        this.distance = distance;
        this.destination = destination;
        this.recompense = recompense;
        this.personne = personne;

        if(itemNecessaire != null)
            this.itemNecessaire = itemNecessaire;

        // Listenner d'event de quand on arrive a un stop
        if(destination.DestinationIsStop())
            RoadManager.instance.onStopReached.AddListener(OnStopReached);

        // Listenner d'event de quand on arrive a une ville
        else if (destination.DestinationIsCity())
            RoadManager.instance.onDestinationReached.AddListener(OnCityReached);
    }

    // TOUT LE RESTE EN DESSOUS EST A CHANGER

    // Debut de la quete
    public void OnBegin()
    {
        Debug.Log("la quete commence");

        GameManager.instance.SpawnItems(itemNecessaire); // On fait apparaitre les objets a l'arret

        if (personne != null)
            GameManager.instance.SpawnPersonne(personne); // On fait apparaitre la personne a l'arret
    }

    public void OnCityReached()
    {
        if (RoadManager.instance.currentRoad.currentDestination == destination.ville)
            OnComplete();
    }


    public void OnStopReached()
    {
        if (RoadManager.instance.currentRoad.currentStop == destination.stop)
        {
            DelayManager.CallTo(delegate() {
                personne.onCarExit.AddListener(OnComplete);
                timeDestination = Time.time;
            },3);
        }
    }

    public void OnFail()
    {
        Debug.Log("la quete a fail");

        QuestManager.instance.DeleteQuest(this);

        for (int j = 0; j < GameManager.instance.car.listSpecialItems.Count; j++)
        {
            for (int i = 0; i < GameManager.instance.car.listItems.Count; i++)
            {
                if (GameManager.instance.car.listSpecialItems[j].item == GameManager.instance.car.listItems[i])
                    GameManager.instance.car.listItems.Remove(GameManager.instance.car.listItems[i]);
            }
            GameManager.instance.car.listSpecialItems.Remove(GameManager.instance.car.listSpecialItems[j]);
        }
    }

    public void OnComplete()
    {
        Debug.Log("Quete complete");
        // Si ca fait pas trop longtemp depuis le stop qu'on etait sense debarquer
        if (Time.time <= timeDestination + 5)
        {
            Debug.Log("la quete est une reussite!");

            GameManager.instance.car.ChangeCash(recompense);

            QuestManager.instance.DeleteQuest(this);

            for (int j = 0; j < GameManager.instance.car.listSpecialItems.Count; j++)
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
        else
        {
            OnFail();
        }
    }
}
