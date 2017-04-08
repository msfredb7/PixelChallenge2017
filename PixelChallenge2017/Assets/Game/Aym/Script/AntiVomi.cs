using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVomi : Item {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override bool valideCase(Case c)
    {
        Item temp;
        bool tempo=false;
        foreach (Item i in Item.allItem)
        {
            if(i.centralCase == c && i.GetType() == typeof(Vomi))
            {
                tempo = true;
            }
        }

        if(c.caseOccupe==false || tempo)
        {
            return true;
        }

        return false;
    }

    public override void EndDrag()
    {
        base.EndDrag();
        List<Case> vomiTouched = new List<Case>();
        foreach(Case c in occupedCase)
        {
            foreach(Item i in allItem)
            {
                if(i.centralCase == c)
                {
                    Vomi v = i.GetComponent<Vomi>();
                    if (v != null)
                    {
                        vomiTouched.Add(c);
                    }
                }
            }
        }

        bool newVomi = true;
        while(newVomi)
        {
            newVomi = false;
            List<Case> caseVoisine = new List<Case>();
            List<Case> toAdd = new List<Case>();
            foreach (Case c in vomiTouched)
            {
                if (c.Haut != null&& hasVomi(c.Haut) && vomiTouched.Contains(c.Haut) == false)
                {
                    toAdd.Add(c.Haut);
                    newVomi = true;
                }
                if (c.Bas != null && hasVomi(c.Bas) && vomiTouched.Contains(c.Bas) == false)
                {
                    toAdd.Add(c.Bas);
                    newVomi = true;
                }
                if (c.Gauche != null && hasVomi(c.Gauche) && vomiTouched.Contains(c.Gauche) == false)
                {
                    toAdd.Add(c.Gauche);
                    newVomi = true;
                }
                if (c.Droite != null  && hasVomi(c.Droite) && vomiTouched.Contains(c.Droite) == false)
                {
                    toAdd.Add(c.Droite);
                    newVomi = true;
                }
            }
            vomiTouched.AddRange(toAdd);
        }
        List<Vomi> toKill = new List<Vomi>();
        foreach(Case c in vomiTouched)
        {
            foreach(Item v in allItem)
            {
                if (v.centralCase == c && v.GetType() == typeof(Vomi))
                {
                    toKill.Add((Vomi)v);
                }
            }
        }

        bool k = false;
        if(toKill.Count>0)
        {
            k = true;
        }
        foreach(Vomi v in toKill)
        {
            v.Kill();
        }

        if(k)
        {
            Kill();
        }

    }

    private bool hasVomi(Case c)
    {
        foreach(Item v in allItem)
        {
            if(v.centralCase == c && v.GetType() == typeof(Vomi))
            {
                return true;
            }
        }
        return false;
    }
}
