using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    public string questDescription;

    public List<Condition> listCondition = new List<Condition>();

    public Quest(string questDescription, List<Condition> listCondition = null)
    {
        this.questDescription = questDescription;
    }

    public void AddCondition(Condition condition)
    {
        listCondition.Add(condition);
    }

    public bool CheckQuestState()
    {
        return false;
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
