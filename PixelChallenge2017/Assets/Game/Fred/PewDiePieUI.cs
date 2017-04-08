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

    bool gasStopped = false;

    void Start()
    {
        repairButton.onClick.AddListener(OnRepairClick);
        continueButton.onClick.AddListener(OnContinueClick);
        GlobalAnimator.OnRestart.AddListener(OnGlobalAnimatorRestart);
        GameManager.instance.car.onDie.AddListener(OnCarDie);
        GameManager.instance.car.onGasChange.AddListener(OnGasChange);
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
        gasStopped = false;
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
            if (GameManager.instance.car.gas > 0)
            {
                GlobalAnimator.Restart();
                gasStopped = false;
            }
            else
            {
                gasStopped = true;
                repairButton.gameObject.SetActive(true);
            }
        });

    }

    void OnGasChange()
    {
        if (gasStopped)
        {
            if(GameManager.instance.car.gas > 0)
            {
                GlobalAnimator.Restart();
                gasStopped = false;
            }
        }
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
