using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoitureShake : MonoBehaviour {

    Vector3 startPos;
    public float shake;

    void Start()
    {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = startPos + new Vector3(Random.Range(-shake * 0.1f, shake * 0.1f), 0, Random.Range(-shake * 0.1f, shake * 0.1f));
    }
}
