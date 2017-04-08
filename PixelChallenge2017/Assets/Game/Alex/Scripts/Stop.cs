using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Structure de donnees pour les arrets (ville, snack, depaneur)
public class Stop {

    public float distance;

    public LieuType lieu;

    public List<ItemAVendre> listItems = new List<ItemAVendre>();

    public Stop(float distance, LieuType lieu, List<ItemAVendre> listItems = null)
    {
        this.distance = distance;
        if (listItems != null)
            this.listItems = listItems;
        this.lieu = lieu;
    }

    public void AddItemAVendre(Item item, float cost, int quantity)
    {
        listItems.Add(new ItemAVendre(item, cost, quantity));
    }

    public void StartEvent(UnityAction onContinue = null)
    {
        GameManager.instance.CreateStop(lieu, listItems, onContinue);
    }
}
