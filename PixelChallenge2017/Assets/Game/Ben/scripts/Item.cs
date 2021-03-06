﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Item : MonoBehaviour
{

    public static List<Item> allItem;

    // Use this for initialization
    [System.Serializable]
    public class shape
    {
        public bool[] caseItem;
    }
    public ArrayLayout cases;
    public shape[] itemShape;
    public string nom;

    public int offsetX;
    public int offsetY;

    List<Collider> colIn;
    bool colInChange = true;
    List<Case> collidedCase;

    public Case centralCase;
    public List<Case> occupedCase;

    protected List<Case> tempoHovered;

    Case beforePlacement;
    private Vector3 posBeforePlacement;

    bool doubledSize;
    public float scaleFacteur;

    public UnityEvent onBeginDrag = new UnityEvent();
    public UnityEvent onEndDrag = new UnityEvent();
    public UnityEvent onEnterCar = new UnityEvent();
    public UnityEvent onExitCar = new UnityEvent();
    public UnityEvent onFailPlacement = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();


    protected List<SpriteRenderer> rend;
    public ItemState _placementState;
    public int wasInCar = -1;

    public string descriptionTxt;

    ItemState placementState
    {
        get
        {
            return _placementState;
        }
        set
        {
            ItemState pastState = placementState;
            _placementState = value;
            if (_placementState == ItemState.placed || _placementState == ItemState.onDragPlacable || _placementState == ItemState.notPlaced)
            {
                if (rend.Count > 0)
                {
                    foreach (SpriteRenderer r in rend)
                    {
                        r.color = Color.white;
                    }
                }

            }
            else if (_placementState == ItemState.onDragUnplacable)
            {
                if (rend.Count > 0)
                {
                    foreach (SpriteRenderer r in rend)
                    {
                        r.color = Color.red;
                    }
                }
            }

            if (_placementState == ItemState.placed)
            {
                if (GameManager.instance != null)
                {
                    if (GameManager.instance.car != null && !GameManager.instance.car.listItems.Contains(this))
                    {
                        if (GameManager.instance.car.listItems != null)
                        {
                            GameManager.instance.car.listItems.Add(this);
                        }
                    }
                }
                if (wasInCar != 1)
                    onEnterCar.Invoke();
                wasInCar = 1;
            }
            if (_placementState == ItemState.notPlaced)
            {
                if (GameManager.instance != null)
                {
                    if (GameManager.instance.car != null && GameManager.instance.car.listItems != null)
                    {
                        GameManager.instance.car.listItems.Remove(this);
                    }

                }
                if (!doubledSize)
                {
                    gameObject.transform.localScale *= scaleFacteur;
                    doubledSize = true;
                }

                if (GetComponent<GazRefill>() != null && OilDisplay.IsMouseIn())
                {
                    GameManager.instance.car.ChangeGas(GetComponent<GazRefill>().refillValue);
                    MusicManager.instance.DoFuelSound();
                    Kill();
                    return;
                }
                GlobalAnimator.AddFloatingItem(gameObject);
                if (wasInCar != 0)
                    onExitCar.Invoke();
                wasInCar = 0;
            }
            if (_placementState != ItemState.notPlaced)
            {
                if (doubledSize)
                {
                    gameObject.transform.localScale /= scaleFacteur;
                    doubledSize = false;
                }
                GlobalAnimator.RemoveFloatingItem(gameObject);
            }

        }
    }


    virtual public bool canOccupeCase(Case central)
    {
        if (central != null)
        {
            List<Case> tryToOccupe = new List<Case>();
            bool canOccupe = true;
            for (int y = 0; y < cases.rows.Length; y++)
            {
                for (int x = 0; x < cases.rows[y].row.Length; x++)
                {
                    if (cases.rows[y].row[x] == true)
                    {
                        Case temp = central.getCaseWithOffset(x - offsetX, y - offsetY);
                        if (temp != null)
                        {
                            tryToOccupe.Add(temp);
                        }
                        else
                        {
                            canOccupe = false;
                        }

                    }
                }
            }


            foreach (Case c in tryToOccupe)
            {
                if (valideCase(c) != true)
                {
                    canOccupe = false;
                }
            }

            return canOccupe;
        }
        return false;
    }

    virtual public void occupeCase()
    {
       
            if (canOccupeCase(centralCase) == true)
            {
                foreach (Case c in occupedByCentral(centralCase))
                {
                    occupedCase.Add(c);
                    c.caseOccupe = true;
                }
            }
            else
            {
                centralCase = null;
            }
        
    }

    public void colorCase()
    {
        Case tempCentralCase = calculCentralCase();
        if (tempCentralCase != null)
        {

        }
    }

    public List<Case> occupedByCentral(Case centralCaseIn)
    {
        if (centralCaseIn != null)
        {
            List<Case> tryToOccupe = new List<Case>();
            bool canOccupe = true;
            for (int y = 0; y < cases.rows.Length; y++)
            {
                for (int x = 0; x < cases.rows[y].row.Length; x++)
                {
                    if (cases.rows[y].row[x] == true)
                    {
                        Case temp = centralCaseIn.getCaseWithOffset(x - offsetX, y - offsetY);
                        if (temp != null)
                        {
                            tryToOccupe.Add(temp);
                        }
                        else
                        {
                            canOccupe = false;
                        }

                    }
                }
            }
            canOccupe = canOccupeCase(centralCaseIn);

           
            if (canOccupe == true)
            {
                return tryToOccupe;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        colIn.Add(other);
        colInChange = true;
    }

    void OnTriggerExit(Collider other)
    {
        colIn.Remove(other);
        colInChange = true;
    }

    //public classCaseItem myCaseItem; 
    public virtual void Start()
    {
        if (allItem == null)
        {
            allItem = new List<Item>();
        }

        if (scaleFacteur <= 0)
        {
            scaleFacteur = 1;
        }
        allItem.Add(this);
        checkOffset();
        colIn = new List<Collider>();
        collidedCase = new List<Case>();
        occupedCase = new List<Case>();
        tempoHovered = new List<Case>();
        rend = new List<SpriteRenderer>();
        rend.AddRange(GetComponents<SpriteRenderer>());
        rend.AddRange(GetComponentsInChildren<SpriteRenderer>());





        if (centralCase != null)
        {
            transform.position = centralCase.transform.position + new Vector3(0, 0.1f, 0);
            CalculCollidedCase();
            collidedCase.Add(centralCase);
            occupeCase();
            if (centralCase != null)
            {
                placementState = ItemState.placed;
            }
            else
            {
                placementState = ItemState.notPlaced;
            }

        }
        else
        {
            placementState = ItemState.notPlaced;
        }

        if (placementState == ItemState.notPlaced)
        {
            if (doubledSize != true)
            {
                gameObject.transform.localScale *= scaleFacteur;
                doubledSize = true;
            }

        }


    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }


    protected void ajustPos()
    {
        if (collidedCase.Count > 0)
        {
            Case tempCentralCase = calculCentralCase();
            if (occupedByCentral(tempCentralCase) != null)
            {
                transform.position = tempCentralCase.transform.position + new Vector3(0, 0.1f, 0);
                collidedCase.Clear();
                fillCollidedCase();
                centralCase = tempCentralCase;
            }
        }
    }

    protected void fillCollidedCase()
    {
        foreach (Collider col in colIn)
        {
            Case tempCase = col.GetComponent<Case>();
            if (tempCase != null)
            {
                collidedCase.Add(tempCase);
            }
        }
    }

    protected void clearCase()
    {
        centralCase = null;
        foreach (Case c in occupedCase)
        {
            c.caseOccupe = false;
        }
        occupedCase.Clear();
        foreach (Case c in tempoHovered)
        {
            c.caseHovered = false;
        }
        tempoHovered.Clear();
    }

    protected Case calculCentralCase()
    {
        if (collidedCase.Count > 0)
        {
            Case minDistCase = collidedCase[0];
            float minDist = (transform.position - minDistCase.transform.position).magnitude;
            foreach (Case c in collidedCase)
            {
                float tempDist = (transform.position - c.transform.position).magnitude;

                if (tempDist < minDist)
                {
                    minDistCase = c;
                    minDist = tempDist;
                }
            }
            return minDistCase;
        }
        return null;
    }

    protected void checkOffset()
    {
        //We assume that the matrix is at least a rectangle. This don't support row with variable sizes
        if (offsetX >= cases.rows[0].row.Length || offsetX < 0)
        {
            Debug.LogError("OffsetX is not good");
            offsetX = 0;
        }
        if (offsetY >= cases.rows.Length || offsetY < 0)
        {
            Debug.LogError("OffsetX is not good");
            offsetY = 0;
        }
    }

    protected void CalculCollidedCase()
    {
        if (colInChange == true)
        {
            collidedCase.Clear();
            fillCollidedCase();
            colInChange = false;
        }
    }


    //Drag and drop methodes
    public void StartDrag()
    {
        beforePlacement = null;
        //posBeforePlacement = null;

        if (placementState == ItemState.placed)
        {
            beforePlacement = centralCase;
        }
        if (placementState == ItemState.notPlaced)
        {
            posBeforePlacement = transform.position;
        }
        Grille.Show(true, GetComponent<Personne>() == null || GetComponent<Personne>().nom == "Illegal");
        clearCase();
        collidedCase.Clear();
        onBeginDrag.Invoke();
    }

    virtual public void EndDrag()
    {
        CalculCollidedCase();
        if (collidedCase.Count != 0)
        {
            ajustPos();
            occupeCase();
            if (centralCase == null && beforePlacement != null)
            {
                transform.position = beforePlacement.transform.position;
                CalculCollidedCase();
                collidedCase.Add(beforePlacement);
                ajustPos();
                occupeCase();
                onFailPlacement.Invoke();
            }
            if (centralCase != null)
            {
                placementState = ItemState.placed;
            }
            else
            {
                placementState = ItemState.notPlaced;
                transform.position = posBeforePlacement;
            }
        }
        else
        {
            placementState = ItemState.notPlaced;
        }

        if (placementState == ItemState.notPlaced)
        {
            centralCase = null;
        }
        Grille.Show(false, true);
        onEndDrag.Invoke();
    }

    public void OnDrag()
    {
        foreach (Case c in tempoHovered)
        {
            c.caseHovered = false;
        }
        tempoHovered.Clear();
        CalculCollidedCase();
        
        List<Case> ret = occupedByCentral(calculCentralCase());
        if (ret != null && ret.Count > 0)
        {
            foreach (Case c in ret)
            {
                tempoHovered.Add(c);
                c.caseHovered = true;
                placementState = ItemState.onDragPlacable;
            }
        }
        else
        {
            placementState = ItemState.onDragUnplacable;
        }
    }

    virtual protected bool valideCase(Case c)
    {
        return !c._caseOccupe;
    }


    public void Kill()
    {
        onDeath.Invoke();
        clearCase();
        Tooltip ins = Tooltip.instance;
        if (ins != null)
        {
            Tooltip.instance.HideToolTip();
        }
        allItem.Remove(this);
        gameObject.SetActive(false);

        GlobalAnimator.RemoveFloatingItem(gameObject);
        //DestroyImmediate(gameObject,true);
    }


    public virtual string description()
    {
        return descriptionTxt;
    }

    public void OnMouseOver()
    {
        Tooltip ins = Tooltip.instance;
        if (ins != null)
        {
            Tooltip.instance.PrintToolTip(description());
        }
       
    }

    public void OnMouseExit()
    {
        Tooltip ins = Tooltip.instance;
        if (ins != null)
        {
            Tooltip.instance.HideToolTip();
        }
       
    }

    protected List<Case> caseUsedFromCentral(Case c)
    {
        List<Case> ret = new List<Case>();

        if (c != null)
        {
            
            for (int y = 0; y < cases.rows.Length; y++)
            {
                for (int x = 0; x < cases.rows[y].row.Length; x++)
                {
                    if (cases.rows[y].row[x] == true)
                    {
                        Case temp = c.getCaseWithOffset(x - offsetX, y - offsetY);
                        if (temp != null)
                        {
                            ret.Add(temp);
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }
        }

        return ret;
    }


}


