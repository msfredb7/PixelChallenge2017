using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest
{
    public class QuestEvent : UnityEvent<Quest> { }
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
    public QuestEvent onWin = new QuestEvent();
    public QuestEvent onFail = new QuestEvent();
    public List<Item> items;
    public bool isOver = false;

    public float totalReward;
    public float totalItems;

    public Quest(string questDescription, float distance, float recompense, Personne personne, Destination destination, List<ItemQuest> itemNecessaire = null)
    {
        this.questDescription = questDescription;
        this.distance = distance;
        this.destination = destination;
        this.recompense = recompense;
        this.personne = personne;

        totalReward = 0;

        if (itemNecessaire != null)
            this.itemNecessaire = itemNecessaire;

        // Listenner d'event de quand on arrive a un stop
        if (destination.DestinationIsStop())
            RoadManager.instance.onLateStopReached.AddListener(OnStopReached);

        // Listenner d'event de quand on arrive a une ville
        else if (destination.DestinationIsCity())
            RoadManager.instance.onLateDestinationReached.AddListener(OnCityReached);
    }

    // Debut de la quete
    public void OnBegin()
    {

        items = GameManager.instance.SpawnItems(itemNecessaire); // On fait apparaitre les objets a l'arret

        int i = 0;
        foreach (Item item in items)
        {
            ItemQuest itemQuest = new ItemQuest(item, itemNecessaire[i].reward);
            GameManager.instance.car.listSpecialItems.Add(itemQuest);
            item.onDeath.AddListener(delegate ()
            {
                if (!isOver)
                    GameManager.instance.car.listSpecialItems.Remove(itemQuest);
            });
            i++;
        }

        if (personne == null)
        {
            Fail();
            return;
        }
        personne = GameManager.instance.SpawnPersonne(personne); // On fait apparaitre la personne a l'arret
        personne.onDeath.AddListener(Fail);
    }

    public void OnCityReached()
    {
        if (RoadManager.instance.currentRoad.currentDestination == destination.ville)
            Complete();
    }


    public void OnStopReached()
    {
        if (RoadManager.instance.currentRoad.currentStop == destination.stop)
        {
            Complete();
        }
    }

    public void Fail()
    {
        RemoveSpecialItems(false);
        RemoveListeners();

        onFail.Invoke(this);
        isOver = true;
    }

    public void Complete()
    {
        onWin.Invoke(this);

        GameManager.instance.car.ChangeCash(recompense);
        totalReward += recompense;

        RemoveSpecialItems(true);
        RemoveListeners();
        RemovePerson();
        isOver = true;
    }

    void RemovePerson()
    {
        if (personne != null)
            personne.Kill();
        personne = null;
    }

    void RemoveListeners()
    {
        if (personne != null)
            personne.onDeath.RemoveListener(Fail);

        // Listenner d'event de quand on arrive a un stop
        if (destination.DestinationIsStop())
            RoadManager.instance.onLateStopReached.RemoveListener(OnStopReached);

        // Listenner d'event de quand on arrive a une ville
        else if (destination.DestinationIsCity())
            RoadManager.instance.onLateDestinationReached.RemoveListener(OnCityReached);
    }

    void RemoveSpecialItems(bool cashIn)
    {
        for (int j = 0; j < GameManager.instance.car.listSpecialItems.Count; j++)
        {
            if (items.Contains(GameManager.instance.car.listSpecialItems[j].item)) // si il est dans la liste
            {
                if (cashIn)
                {
                    GameManager.instance.car.ChangeCash(GameManager.instance.car.listSpecialItems[j].reward); //cash in
                    totalReward += GameManager.instance.car.listSpecialItems[j].reward;
                    totalItems++;
                }

                GameManager.instance.car.listSpecialItems.Remove(GameManager.instance.car.listSpecialItems[j]); //remove
            }
        }
        if (cashIn)
        {
            for (int i = 0; i < items.Count; i++)
            {
                GameManager.instance.car.listItems.Remove(items[i]);
                items[i].Kill();
            }
            items.Clear();
        }
    }
}
