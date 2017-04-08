﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nourriture : Item {

    public int ValeurNourriture;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    override protected void Update () {
        base.Update();
	}

    public override void EndDrag()
    {
        
        CalculCollidedCase();
        Case tempC = calculCentralCase();
        List<Case> hoveredCase = occupedByCentral(tempC);

        foreach(Case c in hoveredCase)
        {
            foreach (Item it in Item.allItem)
            {
                if (it.occupedCase.Contains(c)&& it.GetType() == typeof(Personne))
                {
                    Personne p = (Personne)it;
                    p.food += ValeurNourriture;
                    clearCase();
                    Destroy(gameObject);
                    return;
                }

            }
        }
        base.EndDrag();
    }

    protected override bool valideCase(Case c)
    {
        foreach (Item v in allItem)
        {
            if (v.occupedCase.Contains(c) && v.GetType() == typeof(Personne))
            {
                return true;
            }
        }
        return base.valideCase(c);
    }
}
