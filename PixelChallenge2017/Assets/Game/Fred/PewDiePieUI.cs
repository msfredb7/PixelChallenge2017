using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PewDiePieUI : PublicSingleton<PewDiePieUI> {
    public Button repairButton;
    public Button continueButton;

    void Start()
    {
        repairButton.onClick.AddListener(OnRepairClick);
        continueButton.onClick.AddListener(OnContinueClick);
        GameManager.instance.car.onDie.AddListener(OnCarDie);
    }

    void OnRepairClick()
    {
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
            float price = GameManager.instance.TowingCost;
            if (GameManager.instance.car.cash < price)
            {
                GameOver();
                return;
            }
            repairButton.gameObject.SetActive(true);
        });

    }

    void GameOver()
    {

    }
}
