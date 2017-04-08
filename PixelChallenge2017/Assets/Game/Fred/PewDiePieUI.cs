using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PewDiePieUI : PublicSingleton<PewDiePieUI> {
    public Button repairButton;
    public Button continueButton;
    public Text textPanneSeche;
    public Shop shop;

    public Image blackFG;

        void Start()
    {
        repairButton.onClick.AddListener(OnRepairClick);
        continueButton.onClick.AddListener(OnContinueClick);
        GlobalAnimator.OnRestart.AddListener(OnGlobalAnimatorRestart);
        GameManager.instance.car.onDie.AddListener(OnCarDie);
    }


    public void manqueDeFond()
    {
        textPanneSeche.gameObject.SetActive(true);
        textPanneSeche.text = "Tu n'as pas les fonds requis. Il t'est impossible de reprendre la route! Ton periple s'acheve ici.";

        DelayManager.CallTo(delegate ()
        {
            textPanneSeche.gameObject.SetActive(false);
            GameOver();
        }, 5);
        
    }


    void OnRepairClick()
    {
        if (GameManager.instance.car.cash < GameManager.instance.TowingCost)
        {
            repairButton.gameObject.SetActive(false);
            manqueDeFond();
            return;
        }
        float price = GameManager.instance.TowingCost;
        GameManager.instance.car.ChangeCash(-price);
        GameManager.instance.car.Repair();
        GlobalAnimator.Restart();
    }

    void OnContinueClick()
    {
        GlobalAnimator.Restart();
    }

    void OnGlobalAnimatorRestart()
    {
        repairButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        textPanneSeche.gameObject.SetActive(false);
    }

    void OnCarDie()
    {
        GlobalAnimator.StopAt(LieuType.nullePart,null, delegate()
        {

            textPanneSeche.gameObject.SetActive(true);

            if (GameManager.instance.car.getItemOfType("Gros bidon d'essence") != null ||
            GameManager.instance.car.getItemOfType("Moyen bidon d'essence") != null ||
            GameManager.instance.car.getItemOfType("Petit bidon d'essence") != null)
            {
                if (GameManager.instance.car.cash < GameManager.instance.TowingCost)
                {
                    textPanneSeche.text = "Panne seche! Utilise tes bidons d'essence.";
                }
                textPanneSeche.text = "Panne seche! Utilise tes bidons d'essence ou paye 35$ a la depaneuse pour un plein.";
                repairButton.gameObject.SetActive(true);

            }
            else
            {
                textPanneSeche.text = "Panne seche! Paye 35$ a la depaneuse pour un plein.";
                repairButton.gameObject.SetActive(true);
            }
        });

    }

    public void GameOver()
    {
        blackFG.color = new Color(0, 0, 0, 0);
        blackFG.gameObject.SetActive(true);
        blackFG.DOFade(1,2).OnComplete(delegate()
        {
            DelayManager.ClearAll();
            SceneManager.LoadScene("TransitionToGameOver");
        });
    }
}
