using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour {
    // Use this for initialization
    [System.Serializable]
    public class shape
    { 
        public bool[] caseItem;
    }
    public ArrayLayout cases;
    public shape[] itemShape;

    public int offsetX;
    public int offsetY;

    List<Collider> colIn;
    bool colInChange = true;
    List<Case> collidedCase;

    Case centralCase;
    List<Case> occupedCase;

    List<Case> tempoHovered;

    Case beforePlacement;

    public void occupeCase()
    {
        if(centralCase != null)
        {
            List<Case> tryToOccupe = new List<Case>();
            bool canOccupe = true;
            for (int y = 0; y < cases.rows.Length;y++)
            {
                 for(int x = 0; x<cases.rows[y].row.Length;x++)
                {
                    if(cases.rows[y].row[x] == true)
                    {
                        Case temp = centralCase.getCaseWithOffset(x - offsetX, y - offsetY);
                        if(temp != null)
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
   

            foreach(Case c in tryToOccupe)
            {
                if(c.caseOccupe)
                {
                    canOccupe = false;
                }
            }

            if(canOccupe == true)
            {
                foreach (Case c in tryToOccupe)
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
    }

    public void colorCase()
    {
        Case tempCentralCase = calculCentralCase();
        if(tempCentralCase != null)
        {

        }
    }

    public List<Case> occupedByCentral(Case centralCaseIn)
    {
        if(centralCaseIn != null)
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
            foreach (Case c in tryToOccupe)
            {
                if (c.caseOccupe)
                {
                    canOccupe = false;
                }
            }
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

    void Start () {
        checkOffset();
        colIn = new List<Collider>();
        collidedCase = new List<Case>();
        occupedCase = new List<Case>();
        tempoHovered = new List<Case>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void ajustPos()
    {
        if (collidedCase.Count > 0)
        {
            Case tempCentralCase = calculCentralCase();
            transform.position = tempCentralCase.transform.position + new Vector3(0, 0.1f, 0);
            collidedCase.Clear();
            fillCollidedCase();
            centralCase = tempCentralCase;
        }
    }

    private void fillCollidedCase()
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

    private void clearCase()
    {
        centralCase = null;
        foreach (Case c in occupedCase)
        {
            c.caseOccupe = false;
        }
    }

    private Case calculCentralCase()
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

    private void checkOffset()
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

    private void CalculCollidedCase()
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
        beforePlacement = centralCase;
        clearCase();
        collidedCase.Clear();
    }

    public void EndDrag()
    {
        CalculCollidedCase();
        ajustPos();
        occupeCase();
        if(centralCase == null && beforePlacement != null)
        {
            transform.position = beforePlacement.transform.position;
            CalculCollidedCase();
            collidedCase.Add(beforePlacement);
            ajustPos();
            occupeCase();
        }
    }

    public void OnDrag()
    {
        foreach(Case c in tempoHovered)
        {
            c.caseHovered = false;
        }
        tempoHovered.Clear();
        CalculCollidedCase();
        List<Case> ret = occupedByCentral(calculCentralCase());
        if(ret != null && ret.Count>0)
        {
            foreach(Case c in ret)
            {
                tempoHovered.Add(c);
                c.caseHovered = true;
            }
        }
    }
}
