using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest : MonoBehaviour {

    public string questDescription;

    public Quest(string questDescription, Condition condition)
    {
        this.questDescription = questDescription;
    }

    public bool CheckQuestState()
    {

    }

    public void OnBegin()
    {

    }

    public void OnFail()
    {

    }

    public void OnComplete()
    {

    }
}
