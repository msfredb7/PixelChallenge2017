using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CharacterSpriteSwaper : MonoBehaviour {


	void Start () {
        Item item = GetComponent<Item>();
        item.onBeginDrag.AddListener(delegate () { print("begin drag"); });
        item.onEndDrag.AddListener(delegate () { print("end drag"); });
        item.onEnterCar.AddListener(delegate () { print("enter car"); });
        item.onExitCar.AddListener(delegate () { print("exit car"); });
        item.onFailPlacement.AddListener(delegate () { print("failed placement"); });
    }

    void OnEnterCar()
    {

    }

    void OnExitCar()
    {

    }
}
