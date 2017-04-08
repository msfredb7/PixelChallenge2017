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

    void OnQuestFail(Quest quest)
    {
        DeleteQuest(quest);
    }
    void OnQuestWin(Quest quest)
    {
        DeleteQuest(quest);
        reportQuest.Add(quest);

        if (questList.Count < 1)
        {
            for (int i = 0; 1 < questList.Count; i++)
            {
                DelayManager.CallTo(delegate ()
                {
                    float totalReward = quest.recompense;
                    int totalItems = 0;
                    for (int j = 0; j < GameManager.instance.car.listSpecialItems.Count; j++)
                    {
                        if (quest.items.Contains(GameManager.instance.car.listSpecialItems[j].item)) // si il est dans la liste
                        {
                            totalItems++;
                            totalReward += GameManager.instance.car.listSpecialItems[j].reward;
                        }
                    }
                    // Affichage de l'ecran de completion
                    containerEcran.SetActive(true);
                    personneNom.text = "Nom du personnage : " + quest.personne.nom;
                    description.text = "Description : " + quest.questDescription;
                    reward.text = "Recompense : " + totalReward + "$";
                    quantityObject.text = "Transport de bagages : " + totalItems + " objets";

                    DelayManager.CallTo(delegate ()
                    {
                        containerEcran.SetActive(false);
                    }, (i * 5) + 5);
                }, i * 5);
            }
        }
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
}

