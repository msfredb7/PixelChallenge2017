using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour {

    public enum conditionType
    {
        Destination = 0,
        Item = 1
    }

    public conditionType currentCondition;
    public Ville currentVille;
    public Item currentItem;

    public Condition(Ville destination)
    {
        currentCondition = conditionType.Destination;

    }
}
