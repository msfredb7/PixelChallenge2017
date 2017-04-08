using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vomi : Item {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if(_placementState == ItemState.notPlaced)
        {
            Kill();
      
        }
	}
}
