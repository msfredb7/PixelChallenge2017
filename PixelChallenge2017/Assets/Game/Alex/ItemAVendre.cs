using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAVendre : MonoBehaviour {

    public Item item;
    public float cost;
    public int quantity;

    public ItemAVendre(Item item, float cost, int quantity)
    {
        this.item = item;
        this.cost = cost;
        this.quantity = quantity;
    }

    public void BuyItem()
    {

    }
}
