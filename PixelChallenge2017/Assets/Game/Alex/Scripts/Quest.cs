using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    public string questDescription;

    public Quest(string questDescription)
    {
        this.questDescription = questDescription;
    }
}
