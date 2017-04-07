using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : PublicSingleton<QuestManager> {

    public GameObject container;
    public GameObject textObject;

    public int nbMax;
    public int currentNbQuest = 0;

    public void AddQuest(Quest quest)
    {
        if (currentNbQuest >= nbMax)
            return;
        GameObject newQuest = Instantiate(textObject, container.transform);
        newQuest.GetComponent<Text>().text = quest.questDescription;
        currentNbQuest++;
    }

    public void DeleteQuest(Quest quest)
    {
        foreach (Transform child in container.transform)
        {
            if (child.GetComponent<Text>().text == quest.questDescription)
                Destroy(child.gameObject);
        }
    }

}
