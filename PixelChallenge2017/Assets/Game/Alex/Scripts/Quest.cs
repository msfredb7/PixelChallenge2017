using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    public float distance;

    public string questDescription;

    public Ville destination;
    public List<Item> itemNecessaire = new List<Item>();

    public Quest(string questDescription, float distance, Ville destination, List<Item> itemNecessaire = null)
    {
        this.questDescription = questDescription;
        this.distance = distance;
        this.destination = destination;
        if(itemNecessaire != null)
            this.itemNecessaire = itemNecessaire;
    }

    public void OnBegin()
    {

    }

    public void OnFail()
    {

    }

    public void OnComplete()
    {

    }
}
