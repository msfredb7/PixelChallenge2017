using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personne : Item {

    public string nom;

    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;

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
            }
        }
    }

    private void UpdateRepresentation()
    {
        float green = (food * 0.01f);
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
