using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest {

    public float distance;

    public string questDescription;

    public List<Condition> listCondition = new List<Condition>();

    public Quest(string questDescription, float distance, List<Condition> listCondition = null)
    {
        this.questDescription = questDescription;
    }

    public void AddCondition(Condition condition)
    {
        listCondition.Add(condition);
    }

    public bool CheckQuestState()
    {
        bool result = true;
        for(int i = 0; i < listCondition.Count; i++)
        {
            if (!listCondition[i].ConditionDone())
                result = false;
        }
        return result;
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
