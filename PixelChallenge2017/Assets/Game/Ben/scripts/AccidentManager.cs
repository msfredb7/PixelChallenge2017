using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AccidentManager : PublicSingleton<AccidentManager>
{
    public Button utilisePneuSecoursButton;
    public Button UtiliseOutilButton;
    public Button DépanneuseButton;
    public Text textPanneMoteur;

    public void Start()
    {
        utilisePneuSecoursButton.onClick.AddListener(utiliserPneu);
        UtiliseOutilButton.onClick.AddListener(utiliserOutil);
        DépanneuseButton.onClick.AddListener(appelDépanneuse);

    }


    public void flatEvent()
    {
        GlobalAnimator.StopAt(LieuType.nullePart, null, delegate ()
        {
            textPanneMoteur.gameObject.SetActive(true);

            if (GameManager.instance.car.getItemOfType("Roue de secours") != null)
            {
                utilisePneuSecoursButton.gameObject.SetActive(true);
                if (GameManager.instance.car.cash >= GameManager.instance.TowingCost)
                {
                    textPanneMoteur.text = "Crevaison! Utilise un pneu de secours ou paye 35$ a la depanneuse le remplacement.";
                    DépanneuseButton.gameObject.SetActive(true);
                }
                else textPanneMoteur.text = "Crevaison! Utilise un pneu de secours.";
            }           
            else
            {
                textPanneMoteur.text = "Crevaison! Paye 35$ a la depanneuse le remplacement.";
                DépanneuseButton.gameObject.SetActive(true);
            }
        });

    }

    public void PanneMoteur()
    {
        GlobalAnimator.StopAt(LieuType.nullePart, null, delegate ()
        {
            textPanneMoteur.gameObject.SetActive(true);

            if (GameManager.instance.car.getItemOfType("Outils") != null)
            {
                UtiliseOutilButton.gameObject.SetActive(true);
                if (GameManager.instance.car.cash >= GameManager.instance.TowingCost)
                {
                    textPanneMoteur.text = "Panne mecanique! Utilise un outil ou Paye 35$ a la depanneuse pour la reperation.";
                    DépanneuseButton.gameObject.SetActive(true);
                }
                else textPanneMoteur.text = "Panne mecanique! Utilise un outil.";
            }       
            else
            {
                textPanneMoteur.text = "Panne mecanique! Paye 35$ a la depanneuse pour la reperation.";
                DépanneuseButton.gameObject.SetActive(true);
            }      
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

        if (GameManager.instance.car.cash < GameManager.instance.TowingCost)
        {
            utilisePneuSecoursButton.gameObject.SetActive(false);
            UtiliseOutilButton.gameObject.SetActive(false);
            DépanneuseButton.gameObject.SetActive(false);
            textPanneMoteur.gameObject.SetActive(false);
            PewDiePieUI.instance.manqueDeFond();
            return;
        }

        GameManager.instance.car.ChangeCash(-GameManager.instance.TowingCost);
        GameManager.instance.car.Repair();

        ResetContext();
    }

    public void ResetContext()
    {
        GlobalAnimator.Restart();

        utilisePneuSecoursButton.gameObject.SetActive(false);
        UtiliseOutilButton.gameObject.SetActive(false);
        DépanneuseButton.gameObject.SetActive(false);
        textPanneMoteur.gameObject.SetActive(false);
    }


}

