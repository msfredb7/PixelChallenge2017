using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public class Personne : Item {

    public string nom;

    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;
    public float maxFood;

    public UnityEvent onCarExit = new UnityEvent();

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
                food = maxFood/2;
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
        if(centralCase != null)
        {
            List<Case> caseLibreProche = new List<Case>();
            
            if(occupedCase != null)
            {
                foreach (Case c in occupedCase)
                {
                    if (c.Haut != null && c.Haut.caseOccupe == false)
                    {
                        caseLibreProche.Add(c.Haut);
                    }
                    if (c.Bas != null && c.Bas.caseOccupe == false)
                    {
                        caseLibreProche.Add(c.Bas);
                    }
                    if (c.Gauche != null && c.Gauche.caseOccupe == false)
                    {
                        caseLibreProche.Add(c.Gauche);
                    }
                    if (c.Droite != null && c.Droite.caseOccupe == false)
                    {
                        caseLibreProche.Add(c.Droite);
                    }
                }

                Case randomCase = caseLibreProche[Random.Range(0, caseLibreProche.Count)];
                randomCase.gameObject.SetActive(false);
            }
           
        }


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
        if (_placementState == ItemState.notPlaced)
            onCarExit.Invoke();
    }

    override protected bool valideCase(Case c)
    {
        return c.caseOccupe == false && c.caseType != CaseType.Coffre;
    }
}
