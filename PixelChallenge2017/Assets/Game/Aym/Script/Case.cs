using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Case : MonoBehaviour
{



    #region position
    public int _posX;
    public int _posY;

    public int posX
    {
        get
        {
            return _posX;
        }
        set
        {
            _posX = value;
        }
    }

    public int posY
    {
        get
        {
            return _posY;
        }
        set
        {
            _posY = value;
        }
    }
    #endregion

    #region voisins
    public Case Haut;
    public Case Droite;
    public Case Gauche;
    public Case Bas;
    #endregion

    public bool _caseOccupe;

    public bool caseOccupe
    {
        get
        {
            return _caseOccupe;
        }
        set
        {
            _caseOccupe = value;
            if (_caseOccupe == true)
            {
                Available(false);
                GetComponent<Renderer>().material = occupedMat;
            }
            else
            {
                Available(true);
                GetComponent<Renderer>().material = standardMat;
            }
        }
    }

    public bool _caseHovered;

    public bool caseHovered
    {
        get
        {
            return _caseHovered;
        }
        set
        {
            _caseHovered = value;
            if (_caseHovered == true)
            {
                spr.sprite = fullSqr;
                GetComponent<Renderer>().material.SetFloat("_Metallic", 1);

            }
            else
            {
                spr.sprite = emptySqr;
                GetComponent<Renderer>().material.SetFloat("_Metallic", 0);
            }
        }
    }

    void Available(bool state)
    {
        if (state)
            spr.color = new Color(1, 1, 1, alpha);
        else
            spr.color = new Color(1, 0.2f, 0.2f, alpha);
    }

    public void SetColor(float alpha, bool available)
    {
        this.alpha = alpha;
        if (available)
            spr.color = new Color(1, 1, 1, alpha);
        else
            spr.color = new Color(1, 0.2f, 0.2f, alpha);
    }

    public CaseType caseType;


    private float alpha = 0.1f;
    private SpriteRenderer spr;
    public Sprite fullSqr;
    public Sprite emptySqr;

    public Material occupedMat;
    public Material standardMat;

    public void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        standardMat = GetComponent<Renderer>().material;

        if (caseType == CaseType.desactive)
        {
            Available(false);
            spr.sprite = fullSqr;
        }
    }

    public Case getCaseWithOffset(int xOffSet, int yOffSet)
    {
        Case ret;

        Grille gr = GetComponentInParent<Grille>();
        try
        {

            if (gr != null)
            {
                return gr.grille[posX + xOffSet][posY - yOffSet];
            }

        }
        catch (IndexOutOfRangeException e)
        {
            return null;
        }
        catch (NullReferenceException e)
        {
            return null;
        }
        Case actCase = this;

        if (xOffSet < 0)
        {
            for (int i = xOffSet; i != 0; i++)
            {
                if (actCase != null)
                {
                    actCase = actCase.Gauche;
                }
            }
        }
        else
        {
            for (int i = xOffSet; i != 0; i--)
            {
                if (actCase != null)
                {
                    actCase = actCase.Droite;
                }
            }
        }

        if (yOffSet < 0)
        {
            for (int i = yOffSet; i != 0; i++)
            {
                if (actCase != null)
                {
                    actCase = actCase.Bas;
                }
            }
        }
        else
        {
            for (int i = yOffSet; i != 0; i--)
            {
                if (actCase != null)
                {
                    actCase = actCase.Haut;
                }
            }
        }
        ret = actCase;
        return ret;
    }

}
