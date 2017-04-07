using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Case : MonoBehaviour {

    

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
            if(_caseOccupe == true)
            {
                GetComponent<Renderer>().material = occupedMat;
            }
            else
            {
                GetComponent<Renderer>().material = standardMat;
            }
        }
    }

    public CaseType caseType;



    public Material occupedMat;
    public Material standardMat;

    public void Start()
    {
        standardMat = GetComponent<Renderer>().material;
    }

    public Case getCaseWithOffset(int xOffSet, int yOffSet)
    {
        Case ret;

        Grille gr = GetComponentInParent<Grille>();
        try
        {

        if(gr != null)
        {
            return gr.grille[posX + xOffSet][posY - yOffSet];
        }

        }
        catch (IndexOutOfRangeException e)
        {
            return null;
        }

        Case actCase = this;

        if(xOffSet<0)
        {
            for(int i = xOffSet; i!=0;i++)
            {
                if(actCase!=null)
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
