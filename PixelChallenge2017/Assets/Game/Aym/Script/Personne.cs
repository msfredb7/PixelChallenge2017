using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personne : Item {

    public string nom;

    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;
    public float maxFood;

    public SpriteRenderer hummeur;

    public float food
    {
        get
        {
            return _food;
        }
        set
        {
            _food = value;
            UpdateRepresentation();
            if(_food < 0)
            {
                OnNoFodd();
                food = 0;
            }
            if(food>maxFood)
            {
                food = maxFood;
            }
        }
    }

    private void UpdateRepresentation()
    {
        float green = (food/maxFood);
        float red = 1-green;
        hummeur.color = new Color(red, green, 0);
    }

    private void OnNoFodd()
    {

    }

	// Use this for initialization
	protected override void Start () {
        base.Start();
        rend.Remove(hummeur);
        if(maxFood <=_food)
        {
            maxFood = _food;
        }
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        food -= Time.deltaTime * consomation;
	}

    override protected bool valideCase(Case c)
    {
        return c.caseOccupe == false && c.caseType != CaseType.Coffre;
    }
}
