using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class CharacterSpriteSwaper : MonoBehaviour {

    public Sprite insideCarSprite;
    public Sprite outsideCarSprite;

	void Start () {
        Item item = GetComponent<Item>();
        item.onEnterCar.AddListener(OnEnterCar);
        item.onExitCar.AddListener(OnExitCar);
    }

    void OnEnterCar()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = insideCarSprite;
    }

    void OnExitCar()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = outsideCarSprite;
    }
}
