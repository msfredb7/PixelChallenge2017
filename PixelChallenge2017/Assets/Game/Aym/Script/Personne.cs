using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personne : Item {

    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;

    public float food
    {
        get
        {
            return food;
        }
        set
        {
            food = value;
            UpdateRepresentation();
            if(food < 0)
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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        food -= Time.deltaTime * consomation;
	}
}
