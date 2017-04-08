using System.Collections;
using System.Collections.Generic;
using CCC.Manager;
using UnityEngine;

public class ItemBank : Bank<Item>{

    public Item[] listeItem;

    
    // Use this for initialization
    void Start() {
        print("Initialisation de la ItemBank");
        for (int i = 0; i < listeItem.Length; i++)
        {
            AddItem(listeItem[i]);
        }
    }
}
