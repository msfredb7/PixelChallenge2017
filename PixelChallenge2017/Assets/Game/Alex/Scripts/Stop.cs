using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Structure de donnees pour les arrets (ville, snack, depaneur)
public class Stop {

    public float distance;

    public GameObject prefabStop;

    public UnityEvent onEventComplete;

    public List<ItemAVendre> listItems = new List<ItemAVendre>();

    public Stop(float distance, GameObject prefabStop, List<ItemAVendre> listItems = null)
    {
        this.distance = distance;
        if (listItems != null)
            this.listItems = listItems;
        this.prefabStop = prefabStop;
    }

    public void AddItemAVendre(Item item, float cost, int quantity)
    {
        listItems.Add(new ItemAVendre(item, cost, quantity));
    }

    public void StartEvent()
    {
        GameManager.instance.CreateStop(prefabStop);
        onEventComplete.Invoke();
    }
}
