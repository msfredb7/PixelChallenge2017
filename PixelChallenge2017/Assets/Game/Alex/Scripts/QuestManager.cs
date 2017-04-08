using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : PublicSingleton<QuestManager>
{
    public GameObject container;
    public GameObject textObject;

    public GameObject containerEcran;
    public Text personneNom;
    public Text description;
    public Text reward;
    public Text quantityObject;

    public GameObject questNotification;

    public List<Quest> questList = new List<Quest>();

    public List<Quest> reportQuest = new List<Quest>();

    public int nbMax;
    public int currentNbQuest = 0;

    private float timer;

    public void AddQuest(Quest quest, bool notify = false)
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
        if (notify)
            Notify();

        currentNbQuest++;

        quest.onFail.AddListener(OnQuestFail);
        quest.onWin.AddListener(OnQuestWin);

        // Debut de l'event
        quest.OnBegin();
    }

    void Start()
    {
        timer = Time.time;
    }

    void Update()
    {
        print("timer : " + timer + "| Time : " + Time.time);
        if ((reportQuest.Count >= 1) && (timer + 5) < Time.time)
        {
            timer = Time.time;
            ShowReport(reportQuest[0]);
            reportQuest.Remove(reportQuest[0]);
        } else if((timer + 5) < Time.time)
            containerEcran.SetActive(false);
    }

    void OnQuestFail(Quest quest)
    {
        DeleteQuest(quest);
    }
    void OnQuestWin(Quest quest)
    {
        DeleteQuest(quest);
        reportQuest.Add(quest);
        Debug.Log(reportQuest.Count);
    }

    // Supprimer la quete du UI
    public void DeleteQuest(Quest quest)
    {
        questList.Remove(quest);
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

    public void Notify()
    {
        questNotification.SetActive(true);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(false);
        }, 1);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(true);
        }, 2);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(false);
        }, 3);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(true);
        }, 4);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(false);
        }, 5);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(true);
        }, 6);
        DelayManager.CallTo(delegate ()
        {
            questNotification.SetActive(false);
        }, 7);
    }

    public void ShowReport(Quest quest)
    {
        if (quest == null)
            return;
        
        // Affichage de l'ecran de completion
        containerEcran.SetActive(true);
        personneNom.text = "Nom du personnage : " + quest.personne.nom;
        description.text = "Description : " + quest.questDescription;
        reward.text = "Recompense : " + quest.totalReward + "$";
        quantityObject.text = "Transport de bagages : " + quest.totalItems + " objets";
    }
}

