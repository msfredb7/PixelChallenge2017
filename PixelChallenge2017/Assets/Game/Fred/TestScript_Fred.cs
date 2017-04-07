using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_Fred : MonoBehaviour {

    public BuildingAnimation anim;

	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            OilDisplay.UpdateOil(0.5f);
        }

    }
}
