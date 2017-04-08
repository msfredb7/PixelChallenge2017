using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript_Fred : MonoBehaviour {

    public BuildingAnimation anim;
    public List<Item> items;

	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            //PewDiePieUI.instance.shop.Init(items, new List<int>() { 1, 5, 1, 2 }, new List<int> { 2, 24, 2, 2 });
        }

    }
}
