using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personne : Item {

    public string nom;

    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;

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

    }

    private void OnNoFodd()
    {

    }

	// Use this for initialization
	protected override void Start () {
        base.Start();
		
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
