using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_Fred : MonoBehaviour {

    public BuildingAnimation anim;

	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            GlobalAnimator.StopAt(LieuType.stationEssence);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GlobalAnimator.StopAt(LieuType.arretBus);
        }

    }
}
