using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : PublicSingleton<QuestManager>
{

    public GameObject container;
    public GameObject textObject;

    public List<Quest> questList = new List<Quest>();

    public int nbMax;
    public int currentNbQuest = 0;

    public void AddQuest(Quest quest)
    {
        // Si on a deja trop de quete
        if (currentNbQuest >= nbMax)
            return;

        // Ajout a la liste de traitement
        questList.Add(quest);

        // UI Quete
        GameObject newQuest = Instantiate(textObject, container.transform);
        newQuest.transform.localScale = Vector3.one;
        newQuest.GetComponent<Text>().text = quest.questDescription;

        currentNbQuest++;

        quest.onFail.AddListener(OnQuestFail);
        quest.onWin.AddListener(OnQuestWin);

        // Debut de l'event
        quest.OnBegin();
    }

    void OnQuestFail(Quest quest)
    {
        DeleteQuest(quest);
    }
    void OnQuestWin(Quest quest)
    {
        DeleteQuest(quest);
    }

    // Supprimer la quete du UI
    public void DeleteQuest(Quest quest)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Transform child = container.transform.GetChild(i);
            if (child.GetComponent<Text>().text == quest.questDescription)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }

    // Supprimer toute les quetes de la liste
    public void ClearQuest()
    {
        questList.Clear();
    }
}

