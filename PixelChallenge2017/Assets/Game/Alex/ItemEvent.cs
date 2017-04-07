using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent {

    public float distance;

    public Item item;

    public ItemEvent(float distance, Item item)
    {
        this.distance = distance;
        this.item = item;
    }

    public void StartEvent()
    {
        // evennement d'un item
    }
}
