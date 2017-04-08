using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AccidentManager : PublicSingleton<QuestManager>
{
    public Button utilisePneuSecoursButton;
    public Button UtiliseOutilButton;
    public Button DépanneuseButton;
    public Text textFlat;
    public Text textPanneMoteur;

    public void Start()
    {
        utilisePneuSecoursButton.onClick.AddListener(utiliserPneu);
        UtiliseOutilButton.onClick.AddListener(utiliserOutil);
        DépanneuseButton.onClick.AddListener(appelDépanneuse);

    }

    public void Update()
    {
        if (Input.GetKeyDown("n"))
            flatEvent();
        if (Input.GetKeyDown("m"))
            PanneMoteur();
    }

    public void flatEvent()
    {
        GlobalAnimator.StopAt(LieuType.nullePart, null, delegate ()
        {
            if (GameManager.instance.car.getItemOfType("Roue de secours") != null)
                utilisePneuSecoursButton.gameObject.SetActive(true);
            else
            {
                float price = GameManager.instance.TowingCost;
                if (GameManager.instance.car.cash < price)
                {
                    Debug.Log("You Lose!!!!!!!!");
                    return;
                }
            }
            textFlat.gameObject.SetActive(true);
            DépanneuseButton.gameObject.SetActive(true);
        });

    }



    public void PanneMoteur()
    {
        GlobalAnimator.StopAt(LieuType.nullePart, null, delegate ()
        {
            if (GameManager.instance.car.getItemOfType("Outils") != null)
                UtiliseOutilButton.gameObject.SetActive(true);
            else
            {
                float price = GameManager.instance.TowingCost;
                if (GameManager.instance.car.cash < price)
                {
                    Debug.Log("You Lose!!!!!!!!");
                    return;
                }  
            }
            textPanneMoteur.gameObject.SetActive(true);
            DépanneuseButton.gameObject.SetActive(true);
        });

    }


    public void utiliserPneu()
    {
        Item it = GameManager.instance.car.getItemOfType("Roue de secours");
        GameManager.instance.car.listItems.Remove(it);
        it.Kill();

        ResetContext();
    }


    public void utiliserOutil()
    {
        Item it = GameManager.instance.car.getItemOfType("Outils");
        GameManager.instance.car.listItems.Remove(it);
        it.Kill();

        ResetContext();
    }

    public void appelDépanneuse()
    {
        float price = GameManager.instance.TowingCost;
        GameManager.instance.car.ChangeCash(-price);
        GameManager.instance.car.Repair();

        ResetContext();
    }

    public void ResetContext()
    {
        GlobalAnimator.Restart();

        utilisePneuSecoursButton.gameObject.SetActive(false);
        UtiliseOutilButton.gameObject.SetActive(false);
        DépanneuseButton.gameObject.SetActive(false);
        textFlat.gameObject.SetActive(false);
        textPanneMoteur.gameObject.SetActive(false);
    }


}

