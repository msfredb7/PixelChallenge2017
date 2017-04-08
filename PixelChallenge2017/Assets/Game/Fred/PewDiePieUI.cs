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
    public Shop shop;

    public Image blackFG;

    void Start()
    {
        repairButton.onClick.AddListener(OnRepairClick);
        continueButton.onClick.AddListener(OnContinueClick);
        GlobalAnimator.OnRestart.AddListener(OnGlobalAnimatorRestart);
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
    }

    void OnContinueClick()
    {
        GlobalAnimator.Restart();
    }

    void OnGlobalAnimatorRestart()
    {
        repairButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    void OnCarDie()
    {
        GlobalAnimator.StopAt(LieuType.nullePart,null, delegate()
        {
            repairButton.gameObject.SetActive(true);
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
