using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Personne : Item {
    
    public LieuType objectifStop;

    public float cashValue;

    public float _food;
    public float consomation;
    public float maxFood;
    public GameObject vomi;
    public GameObject dechet;
    public ItemState previousState;

    public UnityEvent onCarExit = new UnityEvent();

    public SpriteRenderer hummeur;
    public float lastDechet;

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
        if (centralCase != null)
        {
            List<Case> caseLibreProche = new List<Case>();

            if (occupedCase != null)
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
                if(caseLibreProche.Count == 0)
                {
                    return;
                }
                List<Case> listVomi = new List<Case>();

                Case randomCase = caseLibreProche[Random.Range(0, caseLibreProche.Count)];
                GameObject temp = Instantiate(vomi);
                Vomi v = temp.GetComponent<Vomi>();
                if (v != null)
                {
                    v.centralCase = randomCase;
                }
                listVomi.Add(randomCase);

                for (int i = 0; i < 4; i++)
                {
                    List<Case> caseLibreProcheVomi = new List<Case>();

                    if (listVomi != null)
                    {
                        foreach (Case c in listVomi)
                        {
                            if (c.Haut != null && c.Haut.caseOccupe == false)
                            {
                                caseLibreProcheVomi.Add(c.Haut);
                            }
                            if (c.Bas != null && c.Bas.caseOccupe == false)
                            {
                                caseLibreProcheVomi.Add(c.Bas);
                            }
                            if (c.Gauche != null && c.Gauche.caseOccupe == false)
                            {
                                caseLibreProcheVomi.Add(c.Gauche);
                            }
                            if (c.Droite != null && c.Droite.caseOccupe == false)
                            {
                                caseLibreProcheVomi.Add(c.Droite);
                            }
                        }
                        if(caseLibreProcheVomi.Count == 0)
                        {
                            return;
                        }
                        randomCase = caseLibreProcheVomi[Random.Range(0, caseLibreProcheVomi.Count)];
                        temp = Instantiate(vomi);
                        v = temp.GetComponent<Vomi>();
                        if (v != null)
                        {
                            v.centralCase = randomCase;
                        }
                        listVomi.Add(randomCase);
                        caseLibreProcheVomi.Remove(randomCase);
                    }

                    


                }

            }

        }
    }

	// Use this for initialization
	public override void Start () {
        base.Start();
        previousState = _placementState;
        rend.Remove(hummeur);
        if(maxFood <=_food)
        {
            maxFood = _food;
        }
        UpdateRepresentation();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if ((RoadManager.instance.currentDistance % 2) < 0.1 && GameManager.instance.car.IsRunning)
        {
            food--;
            if (lastDechet != RoadManager.instance.currentDistance && Random.Range(0f, 1f) < 0.05f)
            {
                lastDechet = RoadManager.instance.currentDistance;
                if (centralCase != null)
                {
                    List<Case> caseLibreProche = new List<Case>();

                    if (occupedCase != null)
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
                        if (caseLibreProche.Count == 0)
                        {
                            return;
                        }


                        Case randomCase = caseLibreProche[Random.Range(0, caseLibreProche.Count)];
                        GameObject temp = Instantiate(dechet);
                        Item v = temp.GetComponent<Item>();
                        if (v != null)
                        {
                            v.centralCase = randomCase;
                        }

                    }
                }
            }
        }
        if (previousState != _placementState)
        {
            if(_placementState == ItemState.notPlaced && previousState == ItemState.onDragUnplacable)
            {
                onCarExit.Invoke();
            }
            previousState = _placementState;
        }
    }

    override protected bool valideCase(Case c)
    {
        return c.caseOccupe == false && c.caseType != CaseType.Coffre;
    }
}
