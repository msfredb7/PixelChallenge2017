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

    //public classCaseItem myCaseItem; 

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
