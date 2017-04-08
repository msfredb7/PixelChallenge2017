using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nourriture : Item {

    public int ValeurNourriture;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    override protected void Update () {
        base.Update();
	}

    public override void EndDrag()
    {
        
        CalculCollidedCase();
        Case temp = calculCentralCase();

        foreach(Item it in Item.allItem)
        {
            if(it.centralCase == temp && it.GetType() == typeof(Personne))
            {
                Personne p = (Personne)it;
                p.food += ValeurNourriture;
                clearCase();
                Destroy(gameObject);
                return;
            }
    
        }
        base.EndDrag();
    }
}
