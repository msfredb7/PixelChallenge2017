using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PewDiePieUI : PublicSingleton<PewDiePieUI> {
    public Button repairButton;
    public Button continueButton;
    public Shop shop;

    void Start()
    {
        repairButton.onClick.AddListener(OnRepairClick);
        continueButton.onClick.AddListener(OnContinueClick);
        GameManager.instance.car.onDie.AddListener(OnCarDie);
    }

    void OnRepairClick()
    {
        if (GameManager.instance.car.cash < GameManager.instance.TowingCost)
        {
            GameOver();
            return;
        }
        float price = GameManager.instance.TowingCost;
        GameManager.instance.car.ChangeCash(-price);
        GameManager.instance.car.Repair();
        GlobalAnimator.Restart();
        repairButton.gameObject.SetActive(false);
    }

    void OnContinueClick()
    {
        continueButton.gameObject.SetActive(false);
        GlobalAnimator.Restart();
    }

    void OnCarDie()
    {
        GlobalAnimator.StopAt(LieuType.nullePart,null, delegate()
        {
            repairButton.gameObject.SetActive(true);
        });

    }

    void GameOver()
    {
        // TODO
    }
}
