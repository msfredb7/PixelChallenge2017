using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grille : MonoBehaviour {

    public Case[][] grille;


    #region GrilleMaker
    public GameObject prefabCase;
    public float distanceX;
    public float distanceY;

    public int nbCaseX;
    public int nbCaseY;

    public bool produceGrille;

    private void initGrille()
    {

        allocateGrillTab();
        //Instantiate the grille
        for(int x = 0;x<nbCaseX;x++)
        {
            for(int y = 0; y<nbCaseY;y++)
            {
                GameObject temp = Instantiate(prefabCase,transform);
                temp.transform.Translate(new Vector3(x*distanceX, y * distanceY,0));
                grille[x][y] = temp.GetComponent<Case>();
                grille[x][y].posX = x;
                grille[x][y].posY = y;
            }
        }

        //Lie les voisins
        for (int x = 0; x < nbCaseX; x++)
        {
            for (int y = 0; y < nbCaseY; y++)
            {
                if(x>0)
                {
                    grille[x][y].Gauche = grille[x - 1][y];
                }
                if(y>0)
                {
                    grille[x][y].Bas = grille[x][y - 1];
                }
                if(x<nbCaseX-1)
                {
                    grille[x][y].Droite = grille[x + 1][y];
                }
                if(y<nbCaseY-1)
                {
                    grille[x][y].Haut = grille[x][y + 1];
                }
            }
        }


    }

    #endregion

    private void allocateGrillTab()
    {
        grille = new Case[nbCaseX][];
        for (int x = 0; x < nbCaseX; x++)
        {
            grille[x] = new Case[nbCaseY];
        }
    }

    private void GetChildsCases()
    {
        Case[] listCases = GetComponentsInChildren<Case>();
        foreach (Case c in listCases)
        {
            if(c.posX>nbCaseX)
            {
                nbCaseX = c.posX;
            }
            if (c.posX > nbCaseX)
            {
                nbCaseY = c.posY;
            }
        }

        allocateGrillTab();

        foreach (Case c in listCases)
        {
            grille[c.posX][c.posY] = c;
        }

    }

    // Use this for initialization
    void Start()
    {
        if(produceGrille)
        {
            initGrille();
        }
        else
        {
            GetChildsCases();
        }

        applyDesactivation();
        //gameObject.SetActive(false);

    }


    private void applyDesactivation()
    {
        for (int x = 0; x < nbCaseX; x++)
        {
            for (int y = 0; y < nbCaseY; y++)
            {
               if(grille[x][y].caseType == CaseType.desactive)
                {
                    grille[x][y]._caseOccupe = true;
                }
            }
        }
    }
}
