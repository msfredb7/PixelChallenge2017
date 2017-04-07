using System.Collections;
using System.Collections.Generic;
using CCC.Manager;
using UnityEngine;

public class PersonneBank : Bank<Personne>
{
    public Personne[] listePersonne;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < listePersonne.Length; i++)
        {
            AddItem(listePersonne[i]);
        }
    }

    public Personne GetPersonneByName(string name)
    {
        for (int i = 0; i < listePersonne.Length; i++)
        {
            if (listePersonne[i].nom == name)
                return listePersonne[i];
        }
        return null;
    }
}
